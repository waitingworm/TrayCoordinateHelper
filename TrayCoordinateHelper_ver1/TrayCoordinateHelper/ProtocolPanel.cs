using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace TrayCoordinateHelper
{
    public partial class ProtocolPanel : UserControl
    {
        private readonly List<AnalysisRecord> records = new List<AnalysisRecord>();
        private ProtocolHistoryStore store;
        private AnalysisRecord current;
        private bool loading;
        private bool dirty;
        private Form host;
        private bool initialized;

        public ProtocolPanel() { InitializeComponent(); }
        public ProtocolPanel(ProtocolHistoryStore historyStore) : this() { store = historyStore; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime || initialized) return;
            initialized = true;
            if (store == null) store = new ProtocolHistoryStore(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "TrayCoordinateHelper", "ProtocolHistory"));
            host = FindForm();
            if (host != null) host.FormClosing += HostClosing;
            try
            {
                string warning;
                records.AddRange(store.Load(out warning));
                RefreshHistory();
                if (warning.Length > 0) MessageBox.Show(this, warning, "기록 복구 안내");
                statusLabel.Text = records.Count + "개 기록 · 원본 입력 후 분석을 누르세요.";
            }
            catch (Exception error) when (IsStorageError(error)) { ShowSaveError(error); }
        }

        private static bool IsStorageError(Exception error)
        { return error is IOException || error is UnauthorizedAccessException || error is InvalidOperationException; }

        private void ShowSaveError(Exception error)
        { statusLabel.Text = "저장/불러오기 실패 · " + error.Message; }

        private bool SaveCurrent()
        {
            saveTimer.Stop();
            resultsGrid.EndEdit();
            if (!dirty || current == null || store == null) return true;
            try { store.Save(current); dirty = false; statusLabel.Text = "자동 저장 완료 · " + current.Name; return true; }
            catch (Exception error) when (IsStorageError(error)) { ShowSaveError(error); return false; }
        }

        private void HostClosing(object sender, FormClosingEventArgs e)
        {
            if (!SaveCurrent())
            {
                e.Cancel = true;
                MessageBox.Show(this, "기록을 저장하지 못해 창을 닫지 않았습니다. 저장 위치의 권한과 디스크 공간을 확인해 주세요.", "저장 실패");
            }
        }

        private void RefreshHistory()
        {
            loading = true;
            historyList.DataSource = records.OrderByDescending(r => r.Pinned).ThenByDescending(r => r.CreatedUtc).ToList();
            historyList.SelectedItem = current;
            loading = false;
        }

        private void AnalyzeClick(object sender, EventArgs e)
        {
            if (!SaveCurrent()) return;
            try
            {
                var record = AnalysisRecord.Create(sourceBox.Text, signedBox.Checked);
                store.Save(record);
                records.Add(record);
                OpenRecord(record);
                statusLabel.Text = "분석·저장 완료 · " + record.Rows.Count + "행 · Prefix 위치 0x" + record.PrefixOffset.ToString("X4") +
                    (record.RemainingBytes > 0 ? " · 마지막 " + record.RemainingBytes + "바이트는 미변환" : "");
            }
            catch (FormatException error) { statusLabel.Text = "입력 확인 · " + error.Message; }
            catch (Exception error) when (IsStorageError(error)) { ShowSaveError(error); }
        }

        private void OpenRecord(AnalysisRecord record)
        {
            loading = true;
            current = record;
            sourceBox.Text = record.OriginalText;
            nameBox.Text = record.Name;
            signedBox.Checked = record.Signed;
            pinButton.Text = record.Pinned ? "★ 고정 해제" : "☆ 상단 고정";
            resultsGrid.DataSource = new BindingList<AnalysisRow>(record.Rows);
            nameBox.Enabled = pinButton.Enabled = true;
            dirty = false;
            loading = false;
            RefreshHistory();
        }

        private void HistoryChanged(object sender, EventArgs e)
        {
            if (loading) return;
            var selected = historyList.SelectedItem as AnalysisRecord;
            if (selected == null || selected == current) return;
            if (!SaveCurrent()) { RefreshHistory(); return; }
            OpenRecord(selected);
            statusLabel.Text = "기록 불러옴 · " + selected.Name + " · " + (selected.Signed ? "Int32" : "UInt32") + " / Little-endian";
        }

        private void SourceChanged(object sender, EventArgs e)
        {
            if (loading) return;
            if (!SaveCurrent())
            {
                loading = true;
                sourceBox.Text = current.OriginalText;
                loading = false;
                return;
            }
            current = null;
            resultsGrid.DataSource = null;
            nameBox.Enabled = pinButton.Enabled = false;
            loading = true;
            nameBox.Text = "새 분석";
            loading = false;
            RefreshHistory();
            statusLabel.Text = "새 입력 · 분석하면 새 기록으로 저장됩니다. 이전 기록은 왼쪽 목록에 보관됩니다.";
        }

        private void NewClick(object sender, EventArgs e)
        {
            if (!SaveCurrent()) return;
            sourceBox.Clear();
            sourceBox.Focus();
        }

        private void MemoEdited(object sender, DataGridViewCellEventArgs e)
        {
            if (loading || current == null) return;
            dirty = true;
            saveTimer.Stop();
            saveTimer.Start();
        }

        private void NameChanged(object sender, EventArgs e)
        {
            if (loading || current == null) return;
            if (string.IsNullOrWhiteSpace(nameBox.Text)) { statusLabel.Text = "이름을 입력해 주세요. 기존 이름은 유지됩니다."; return; }
            current.Name = nameBox.Text.Trim();
            dirty = true;
            saveTimer.Stop();
            saveTimer.Start();
        }

        private void SaveTick(object sender, EventArgs e)
        { if (SaveCurrent()) RefreshHistory(); }

        private void PinClick(object sender, EventArgs e)
        {
            if (current == null) return;
            current.Pinned = !current.Pinned;
            dirty = true;
            pinButton.Text = current.Pinned ? "★ 고정 해제" : "☆ 상단 고정";
            SaveCurrent();
            RefreshHistory();
        }

        private void SignedChanged(object sender, EventArgs e)
        {
            if (!loading) statusLabel.Text = "해석 방식 변경 · 분석을 누르면 이 설정으로 새 기록이 생성됩니다.";
        }

        private void GridError(object sender, DataGridViewDataErrorEventArgs e)
        { e.ThrowException = false; statusLabel.Text = "셀 입력을 확인해 주세요."; }
    }
}
