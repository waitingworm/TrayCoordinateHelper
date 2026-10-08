using System;
using System.Windows.Forms;

namespace TrayCoordinateHelper
{
    // 배치/크기는 Designer.cs, 클릭/계산 동작은 이 파일에 있습니다.
    public partial class TraySetupForm : Form
    {
        private readonly TrayPanel[] trays;

        public TraySetupForm()
        {
            InitializeComponent();
            trays = new TrayPanel[] { leftTray, middleTray, rightTray };
            UpdateMode();
        }

        private void ModeChanged(object sender, EventArgs e) { UpdateMode(); }

        private void BrowseClick(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Title = "설비 파라미터 파일 선택 (현재는 경로 선택만 가능)";
                if (dialog.ShowDialog(this) == DialogResult.OK) path.Text = dialog.FileName;
            }
        }

        private void ReloadClick(object sender, EventArgs e)
        {
            foreach (TrayPanel tray in trays) tray.RestoreSample();
            status.Text = "샘플 좌표 복원 완료 (파일을 다시 읽는 기능은 연결 전)";
        }

        private void UpdateMode()
        {
            if (apply == null || status == null) return;
            xPitch.Enabled = xCount.Enabled = yPitch.Enabled = yCount.Enabled = pitchMode.Checked;
            browse.Enabled = userMode.Checked;
            apply.Enabled = pitchMode.Checked;
            status.Text = pitchMode.Checked ? "준비 | LR 기준 좌표와 피치/카운트를 입력한 뒤 적용하세요."
                : "경로 모드 화면 예제 | 실제 파일 형식 확인 후 불러오기를 연결합니다.";
        }

        // 4. 화면과 계산 연결: 기준점은 그대로 두고 다른 두 점만 계산합니다.
        private void ApplyPitch(object sender, EventArgs e)
        {
            try
            {
                decimal dx = xPitch.Value * (xCount.Value - 1);
                decimal dy = yPitch.Value * (yCount.Value - 1);
                decimal[][,] results = new decimal[3][,];
                for (int i = 0; i < trays.Length; i++) results[i] = trays[i].Calculate(dx, dy);
                for (int i = 0; i < trays.Length; i++) trays[i].SetPitchSettings(xPitch.Value, (int)xCount.Value, yPitch.Value, (int)yCount.Value);
                status.Text = "전체 적용 | X 길이 " + dx + " mm / Y 길이 " + dy + " mm · 이후 트레이별 입력 즉시 갱신";
            }
            catch (InvalidOperationException error)
            {
                MessageBox.Show(this, error.Message, "입력 확인", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void leftTray_Load(object sender, EventArgs e)
        {

        }

        private void middleTray_Load(object sender, EventArgs e)
        {

        }
    }
}
