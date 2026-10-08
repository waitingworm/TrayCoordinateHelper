using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace TrayCoordinateHelper
{
    public class AnalysisRow
    {
        public int Number { get; set; }
        public string Memo { get; set; }
        public string Hex { get; set; }
        public string Value { get; set; }
    }

    public class AnalysisRecord
    {
        public int Version { get; set; } = 1;
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedUtc { get; set; }
        public bool Pinned { get; set; }
        public string OriginalText { get; set; }
        public bool Signed { get; set; }
        public string ByteOrder { get; set; } = "Little-endian";
        public int PrefixOffset { get; set; }
        public int RemainingBytes { get; set; }
        public List<AnalysisRow> Rows { get; set; } = new List<AnalysisRow>();

        public override string ToString()
        {
            return (Pinned ? "★ " : "") + Name + "  · " + CreatedUtc.ToLocalTime().ToString("MM/dd HH:mm");
        }

        public static AnalysisRecord Create(string original, bool signed)
        {
            var converted = PacketConverter.Convert(original);
            string[] hex = converted.HexText.TrimEnd('\r', '\n').Split(new[] { "\r\n" }, StringSplitOptions.None);
            string[] values = converted.DecimalText.TrimEnd('\r', '\n').Split(new[] { "\r\n" }, StringSplitOptions.None);
            var record = new AnalysisRecord
            {
                Id = Guid.NewGuid().ToString("N"), CreatedUtc = DateTime.UtcNow,
                Name = "분석 " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                OriginalText = original, Signed = signed,
                PrefixOffset = converted.PrefixOffset, RemainingBytes = converted.RemainingBytes
            };
            for (int i = 0; i < hex.Length; i++)
            {
                uint value;
                string decoded = values[i];
                if (signed && uint.TryParse(decoded, NumberStyles.Integer, CultureInfo.InvariantCulture, out value))
                    decoded = unchecked((int)value).ToString(CultureInfo.InvariantCulture);
                record.Rows.Add(new AnalysisRow { Number = i + 1, Hex = hex[i].Trim(), Value = decoded, Memo = "" });
            }
            return record;
        }
    }

    // 화면과 독립적인 저장소. 임시 파일을 완성한 후 교체하고 직전 버전을 백업합니다.
    public sealed class ProtocolHistoryStore
    {
        public string Folder { get; private set; }
        private readonly XmlSerializer serializer = new XmlSerializer(typeof(AnalysisRecord));
        public ProtocolHistoryStore(string folder) { Folder = folder; }

        public void Save(AnalysisRecord record)
        {
            Guid id;
            if (!Guid.TryParseExact(record.Id, "N", out id)) throw new InvalidDataException("기록 ID가 잘못되었습니다.");
            Directory.CreateDirectory(Folder);
            string path = Path.Combine(Folder, record.Id + ".xml");
            string temp = path + ".tmp";
            using (var stream = new FileStream(temp, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                var settings = new XmlWriterSettings { Encoding = new System.Text.UTF8Encoding(false), Indent = true, NewLineHandling = NewLineHandling.Entitize };
                using (var writer = XmlWriter.Create(stream, settings)) serializer.Serialize(writer, record);
                stream.Flush(true);
            }
            if (File.Exists(path)) File.Replace(temp, path, path + ".bak");
            else File.Move(temp, path);
        }

        private AnalysisRecord Read(string path)
        {
            var settings = new XmlReaderSettings { DtdProcessing = DtdProcessing.Prohibit, XmlResolver = null, MaxCharactersInDocument = 512000000 };
            using (var reader = XmlReader.Create(path, settings))
            {
                var record = (AnalysisRecord)serializer.Deserialize(reader);
                Guid id;
                if (record.Version != 1 || !Guid.TryParseExact(record.Id, "N", out id) ||
                    Path.GetFileName(path).Split('.')[0] != record.Id || record.Rows == null ||
                    record.Rows.Any(r => r == null) || record.OriginalText == null)
                    throw new InvalidDataException("지원하지 않거나 손상된 기록입니다.");
                return record;
            }
        }

        public List<AnalysisRecord> Load(out string warning)
        {
            warning = "";
            var records = new List<AnalysisRecord>();
            if (!Directory.Exists(Folder)) return records;
            foreach (var path in Directory.GetFiles(Folder, "*.xml"))
            {
                try { records.Add(Read(path)); }
                catch (Exception error) when (error is IOException || error is InvalidOperationException || error is XmlException || error is UnauthorizedAccessException)
                {
                    try { records.Add(Read(path + ".bak")); warning += "백업 복구: " + Path.GetFileName(path) + "\r\n"; }
                    catch (Exception backupError) when (backupError is IOException || backupError is InvalidOperationException || backupError is XmlException || backupError is UnauthorizedAccessException)
                    { warning += "불러오지 못한 기록(원본 보존): " + Path.GetFileName(path) + "\r\n"; }
                }
            }
            return records;
        }
    }
}

