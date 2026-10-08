using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Drawing;
using System.Windows.Forms;
using TrayCoordinateHelper;

class ProtocolHistoryVerification
{
    static void Check(bool ok, string message) { if (!ok) throw new Exception(message); }
    static T Get<T>(ProtocolPanel p, string field) { return (T)typeof(ProtocolPanel).GetField(field, BindingFlags.NonPublic | BindingFlags.Instance).GetValue(p); }
    static object Call(ProtocolPanel p, string method) { return typeof(ProtocolPanel).GetMethod(method, BindingFlags.NonPublic | BindingFlags.Instance).Invoke(p, new object[] { null, EventArgs.Empty }); }
    [STAThread] static void Main(string[] args) { try { Run(args); } catch(Exception e) { Console.WriteLine(e.GetType().FullName); Console.WriteLine(e.Message); Console.WriteLine(e.StackTrace); if(e.InnerException != null) Console.WriteLine(e.InnerException.Message); Environment.ExitCode=1; } }
    static void Run(string[] args)
    {
        string folder = Path.Combine(Path.Combine(Environment.CurrentDirectory, "TrayCoordinateHelper_ver1", ".test-output"), "ProtocolHistoryTest-" + Guid.NewGuid().ToString("N"));
        var store = new ProtocolHistoryStore(folder);
        string raw = "0000 AA BB FF FF FF FF 30 01 00 00 FE FF FF FF 00 00\r\n0010 00 00 24 00 00 00 01 02";
        var unsigned = AnalysisRecord.Create(raw, false);
        Check(unsigned.PrefixOffset == 2 && unsigned.Rows.Count == 6 && unsigned.RemainingBytes == 2, "boundary/remainder");
        Check(unsigned.Rows[1].Value == "304" && unsigned.Rows[2].Value == "4294967294", "uint decode");
        var signed = AnalysisRecord.Create(raw, true);
        Check(signed.Rows[2].Value == "-2" && signed.Rows[5].Value == "—", "signed decode/remainder");
        signed.Rows[2].Memo = "음수 좌표 <테스트> & 보존";
        signed.Pinned = true;
        signed.Name = "검사 기록";
        store.Save(signed);
        signed.Name = "변경 이름";
        store.Save(signed);
        string warning;
        var loaded = store.Load(out warning).Single();
        Check(warning == "" && loaded.OriginalText == raw && loaded.Rows[2].Memo == signed.Rows[2].Memo && loaded.Name == "변경 이름" && loaded.Pinned && loaded.Signed, "round trip");
        File.WriteAllText(Path.Combine(folder, signed.Id + ".xml"), "broken");
        loaded = store.Load(out warning).Single();
        Check(warning.Contains("백업") && loaded.Name == "검사 기록", "backup recovery");
        var uiStore = new ProtocolHistoryStore(Path.Combine(folder, "ui"));
        using (var form = new Form { ClientSize = new Size(1400, 850) })
        using (var panel = new ProtocolPanel(uiStore) { Dock = DockStyle.Fill })
        {
            form.Controls.Add(panel); form.Show(); Application.DoEvents();
            var source = Get<TextBox>(panel, "sourceBox");
            var grid = Get<DataGridView>(panel, "resultsGrid");
            var list = Get<ListBox>(panel, "historyList");
            source.Text = raw; Call(panel, "AnalyzeClick");
            Check(grid.Columns.Count == 4 && grid.Columns[1].DataPropertyName == "Memo" && grid.Columns[2].DataPropertyName == "Hex", "column order");
            Check(grid.Rows.Count == 6 && !grid.Columns[1].ReadOnly && grid.Columns[2].ReadOnly, "grid editable memo");
            grid.CurrentCell = grid.Rows[2].Cells[1]; grid.BeginEdit(true);
            ((TextBox)grid.EditingControl).Text = "메모 저장 검증";
            grid.EndEdit();
            Get<TextBox>(panel, "nameBox").Text = "첫 번째 분석";
            Call(panel, "PinClick");
            source.Text = "0000 FF FF FF FF 06 00 00 00";
            Check(grid.Rows.Count == 0 && list.Items.Count == 1, "new input preserves history, clears stale results");
            Call(panel, "AnalyzeClick");
            Check(list.Items.Count == 2 && ((AnalysisRecord)list.Items[0]).Pinned, "pinned order");
            list.SelectedIndex = 0;
            Check(source.Text == raw && grid.Rows[2].Cells[1].Value.ToString() == "메모 저장 검증", "restore source and memo");
            source.Text = "invalid"; Call(panel, "AnalyzeClick");
            Check(list.Items.Count == 2 && grid.Rows.Count == 0, "invalid input no history");
            list.SelectedIndex = 0;
            form.Refresh(); Application.DoEvents();
            if(args.Length > 0) using(var bmp = new Bitmap(panel.Width, panel.Height)) { panel.DrawToBitmap(bmp, panel.ClientRectangle); bmp.Save(args[0]); }
            form.ClientSize = new Size(1000, 700); Application.DoEvents();
            Check(grid.Width > 500 && grid.Height > 100, "small layout");
            grid.CurrentCell = grid.Rows[1].Cells[1]; grid.BeginEdit(true);
            ((TextBox)grid.EditingControl).Text = "종료 직전 편집";
            form.Close();
        }
        using(var form = new Form()) using(var panel = new ProtocolPanel(uiStore) { Dock = DockStyle.Fill })
        {
            form.ClientSize = new Size(1200, 800); form.Controls.Add(panel); form.Show(); Application.DoEvents();
            var list = Get<ListBox>(panel, "historyList");
            Check(list.Items.Count == 2, "restart history");
            list.SelectedIndex = 0;
            Check(Get<DataGridView>(panel, "resultsGrid").Rows[1].Cells[1].Value.ToString() == "종료 직전 편집", "close commits edit");
            form.Close();
        }
        Console.WriteLine("PASS: decode, signed values, remainder, record roundtrip, backup recovery, grid edit, pin, rename, source restore, new/invalid input, close-save, restart, layout.");
        Console.WriteLine("Test-only records: " + folder);
    }
}


