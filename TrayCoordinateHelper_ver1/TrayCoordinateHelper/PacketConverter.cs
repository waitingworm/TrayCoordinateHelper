using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace TrayCoordinateHelper
{
    public sealed class PacketConversionResult
    {
        public string Text { get; internal set; }
        public string HexText { get; internal set; }
        public string DescriptionText { get; internal set; }
        public string DecimalText { get; internal set; }
        public int PrefixOffset { get; internal set; }
        public int GroupCount { get; internal set; }
        public int RemainingBytes { get; internal set; }
    }

    // 화면에 의존하지 않는 변환기입니다.
    // 입력: 주소 열 + 공백으로 구분된 바이트. 해석: little-endian UInt32.
    public static class PacketConverter
    {
        public static PacketConversionResult Convert(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new FormatException("원본 데이터를 먼저 붙여넣어 주세요.");
            if (input.Length > 1000000)
                throw new FormatException("한 번에 입력할 수 있는 텍스트는 100만 자까지입니다.");

            var bytes = new List<byte>();
            string[] lines = input.Replace("\r\n", "\n").Replace('\r', '\n').Split('\n');
            int? firstAddress = null;
            for (int line = 0; line < lines.Length; line++)
            {
                string[] words = lines[line].Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                if (words.Length == 0) continue;
                int address;
                if (words.Length < 2 || words[0].Length < 4 || words[0].Length > 8 ||
                    !int.TryParse(words[0], NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture, out address) || address < 0)
                    throw new FormatException((line + 1) + "행: 주소 열과 바이트를 확인해 주세요. 예: 0000 FF FF FF FF");
                if (!firstAddress.HasValue) firstAddress = address;
                if ((long)firstAddress.Value + bytes.Count != address)
                    throw new FormatException((line + 1) + "행: 주소가 이어지지 않습니다. 누락되거나 중복된 행을 확인해 주세요.");

                // 주소는 제외하고 실제 바이트만 연결합니다. 줄바꿈은 데이터 경계가 아닙니다.
                for (int i = 1; i < words.Length; i++)
                {
                    byte value;
                    if (words[i].Length != 2 || !byte.TryParse(words[i], NumberStyles.AllowHexSpecifier,
                        CultureInfo.InvariantCulture, out value))
                        throw new FormatException((line + 1) + "행: 잘못된 바이트 '" + words[i] + "'. 주소와 HEX 바이트만 붙여넣어 주세요.");
                    bytes.Add(value);
                }
            }

            int start = -1;
            for (int i = 0; i + 3 < bytes.Count; i++)
                if (bytes[i] == 255 && bytes[i + 1] == 255 && bytes[i + 2] == 255 && bytes[i + 3] == 255)
                { start = i; break; }
            if (start < 0) throw new FormatException("시작 표시 FF FF FF FF를 찾지 못했습니다.");

            var output = new StringBuilder();
            var hexOutput = new StringBuilder();
            var descriptions = new StringBuilder();
            var decimals = new StringBuilder();
            output.AppendLine("해석: Little-endian / UInt32 (부호 없는 32비트 정수)");
            output.AppendLine("번호   원본 HEX      DEC");
            output.AppendLine("-------------------------------------------");
            int position = start, count = 0;
            for (; position + 3 < bytes.Count; position += 4, count++)
            {
                uint value = (uint)bytes[position] | ((uint)bytes[position + 1] << 8) |
                    ((uint)bytes[position + 2] << 16) | ((uint)bytes[position + 3] << 24);
                hexOutput.AppendFormat("{0:X2} {1:X2} {2:X2} {3:X2}\r\n",
                    bytes[position], bytes[position + 1], bytes[position + 2], bytes[position + 3]);
                descriptions.AppendLine(); // 항목의 의미는 표의 메모에 자유롭게 입력합니다.
                decimals.AppendLine(value.ToString(CultureInfo.InvariantCulture));
                output.AppendFormat(CultureInfo.InvariantCulture, "{0,4}   {1:X2} {2:X2} {3:X2} {4:X2}   {5}{6}\r\n",
                    count, bytes[position], bytes[position + 1], bytes[position + 2], bytes[position + 3],
                    value, count == 0 ? "  (Prefix)" : "");
            }
            int remaining = bytes.Count - position;
            if (remaining > 0)
            {
                output.Append("미변환 HEX: ");
                for (int i = position; i < bytes.Count; i++) output.Append(bytes[i].ToString("X2") + " ");
                output.AppendLine("— 4바이트에 부족하므로 보존합니다.");
                for (int i = position; i < bytes.Count; i++) hexOutput.Append(bytes[i].ToString("X2") + " ");
                hexOutput.AppendLine();
                descriptions.AppendLine("미변환: 4바이트 부족");
                decimals.AppendLine("—");
            }
            return new PacketConversionResult { Text = output.ToString(), PrefixOffset = firstAddress.Value + start,
                GroupCount = count, RemainingBytes = remaining, HexText = hexOutput.ToString(),
                DescriptionText = descriptions.ToString(), DecimalText = decimals.ToString() };
        }
    }
}


