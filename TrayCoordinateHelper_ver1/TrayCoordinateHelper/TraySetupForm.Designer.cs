namespace TrayCoordinateHelper
{
    partial class TraySetupForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.heading = new System.Windows.Forms.Label();
            this.subtitle = new System.Windows.Forms.Label();
            this.xOptions = new System.Windows.Forms.Panel();
            this.xTitle = new System.Windows.Forms.Label();
            this.xPitchLabel = new System.Windows.Forms.Label();
            this.xCountLabel = new System.Windows.Forms.Label();
            this.xPitch = new System.Windows.Forms.NumericUpDown();
            this.xCount = new System.Windows.Forms.NumericUpDown();
            this.yOptions = new System.Windows.Forms.Panel();
            this.yTitle = new System.Windows.Forms.Label();
            this.yPitchLabel = new System.Windows.Forms.Label();
            this.yCountLabel = new System.Windows.Forms.Label();
            this.yPitch = new System.Windows.Forms.NumericUpDown();
            this.yCount = new System.Windows.Forms.NumericUpDown();
            this.modes = new System.Windows.Forms.GroupBox();
            this.pitchMode = new System.Windows.Forms.RadioButton();
            this.defaultMode = new System.Windows.Forms.RadioButton();
            this.userMode = new System.Windows.Forms.RadioButton();
            this.path = new System.Windows.Forms.TextBox();
            this.browse = new System.Windows.Forms.Button();
            this.apply = new System.Windows.Forms.Button();
            this.modeHint = new System.Windows.Forms.Label();
            this.leftTray = new TrayCoordinateHelper.TrayPanel();
            this.middleTray = new TrayCoordinateHelper.TrayPanel();
            this.rightTray = new TrayCoordinateHelper.TrayPanel();
            this.status = new System.Windows.Forms.Label();
            this.reload = new System.Windows.Forms.Button();
            this.save = new System.Windows.Forms.Button();
            this.footnote = new System.Windows.Forms.Label();
            this.xOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xPitch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xCount)).BeginInit();
            this.yOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.yPitch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.yCount)).BeginInit();
            this.modes.SuspendLayout();
            this.SuspendLayout();
            // 
            // heading
            // 
            this.heading.Font = new System.Drawing.Font("맑은 고딕", 17F, System.Drawing.FontStyle.Bold);
            this.heading.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(110)))), ((int)(((byte)(186)))));
            this.heading.Location = new System.Drawing.Point(18, 12);
            this.heading.Name = "heading";
            this.heading.Size = new System.Drawing.Size(200, 29);
            this.heading.TabIndex = 0;
            this.heading.Text = "TRAY SETUP";
            this.heading.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // subtitle
            // 
            this.subtitle.Location = new System.Drawing.Point(223, 19);
            this.subtitle.Name = "subtitle";
            this.subtitle.Size = new System.Drawing.Size(440, 22);
            this.subtitle.TabIndex = 1;
            this.subtitle.Text = "트레이 좌표 설정  /  화면 구성 예제";
            this.subtitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // xOptions
            // 
            this.xOptions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(110)))), ((int)(((byte)(186)))));
            this.xOptions.Controls.Add(this.xTitle);
            this.xOptions.Controls.Add(this.xPitchLabel);
            this.xOptions.Controls.Add(this.xCountLabel);
            this.xOptions.Controls.Add(this.xPitch);
            this.xOptions.Controls.Add(this.xCount);
            this.xOptions.Location = new System.Drawing.Point(16, 53);
            this.xOptions.Name = "xOptions";
            this.xOptions.Size = new System.Drawing.Size(205, 133);
            this.xOptions.TabIndex = 2;
            // 
            // xTitle
            // 
            this.xTitle.ForeColor = System.Drawing.Color.White;
            this.xTitle.Location = new System.Drawing.Point(13, 8);
            this.xTitle.Name = "xTitle";
            this.xTitle.Size = new System.Drawing.Size(180, 24);
            this.xTitle.TabIndex = 0;
            this.xTitle.Text = "X AXIS";
            this.xTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // xPitchLabel
            // 
            this.xPitchLabel.ForeColor = System.Drawing.Color.White;
            this.xPitchLabel.Location = new System.Drawing.Point(13, 47);
            this.xPitchLabel.Name = "xPitchLabel";
            this.xPitchLabel.Size = new System.Drawing.Size(88, 22);
            this.xPitchLabel.TabIndex = 1;
            this.xPitchLabel.Text = "Pitch (mm)";
            this.xPitchLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // xCountLabel
            // 
            this.xCountLabel.ForeColor = System.Drawing.Color.White;
            this.xCountLabel.Location = new System.Drawing.Point(13, 85);
            this.xCountLabel.Name = "xCountLabel";
            this.xCountLabel.Size = new System.Drawing.Size(88, 22);
            this.xCountLabel.TabIndex = 2;
            this.xCountLabel.Text = "Count (칸)";
            this.xCountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // xPitch
            // 
            this.xPitch.AccessibleName = "X Pitch";
            this.xPitch.DecimalPlaces = 3;
            this.xPitch.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.xPitch.Location = new System.Drawing.Point(101, 44);
            this.xPitch.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.xPitch.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.xPitch.Name = "xPitch";
            this.xPitch.Size = new System.Drawing.Size(91, 27);
            this.xPitch.TabIndex = 3;
            this.xPitch.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // xCount
            // 
            this.xCount.AccessibleName = "X Count";
            this.xCount.Location = new System.Drawing.Point(101, 82);
            this.xCount.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.xCount.Name = "xCount";
            this.xCount.Size = new System.Drawing.Size(91, 27);
            this.xCount.TabIndex = 4;
            this.xCount.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // yOptions
            // 
            this.yOptions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(110)))), ((int)(((byte)(186)))));
            this.yOptions.Controls.Add(this.yTitle);
            this.yOptions.Controls.Add(this.yPitchLabel);
            this.yOptions.Controls.Add(this.yCountLabel);
            this.yOptions.Controls.Add(this.yPitch);
            this.yOptions.Controls.Add(this.yCount);
            this.yOptions.Location = new System.Drawing.Point(233, 53);
            this.yOptions.Name = "yOptions";
            this.yOptions.Size = new System.Drawing.Size(205, 133);
            this.yOptions.TabIndex = 3;
            // 
            // yTitle
            // 
            this.yTitle.ForeColor = System.Drawing.Color.White;
            this.yTitle.Location = new System.Drawing.Point(13, 8);
            this.yTitle.Name = "yTitle";
            this.yTitle.Size = new System.Drawing.Size(180, 24);
            this.yTitle.TabIndex = 0;
            this.yTitle.Text = "Y AXIS";
            this.yTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // yPitchLabel
            // 
            this.yPitchLabel.ForeColor = System.Drawing.Color.White;
            this.yPitchLabel.Location = new System.Drawing.Point(13, 47);
            this.yPitchLabel.Name = "yPitchLabel";
            this.yPitchLabel.Size = new System.Drawing.Size(88, 22);
            this.yPitchLabel.TabIndex = 1;
            this.yPitchLabel.Text = "Pitch (mm)";
            this.yPitchLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // yCountLabel
            // 
            this.yCountLabel.ForeColor = System.Drawing.Color.White;
            this.yCountLabel.Location = new System.Drawing.Point(13, 85);
            this.yCountLabel.Name = "yCountLabel";
            this.yCountLabel.Size = new System.Drawing.Size(88, 22);
            this.yCountLabel.TabIndex = 2;
            this.yCountLabel.Text = "Count (칸)";
            this.yCountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // yPitch
            // 
            this.yPitch.AccessibleName = "Y Pitch";
            this.yPitch.DecimalPlaces = 3;
            this.yPitch.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.yPitch.Location = new System.Drawing.Point(101, 44);
            this.yPitch.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.yPitch.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.yPitch.Name = "yPitch";
            this.yPitch.Size = new System.Drawing.Size(91, 27);
            this.yPitch.TabIndex = 3;
            this.yPitch.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // yCount
            // 
            this.yCount.AccessibleName = "Y Count";
            this.yCount.Location = new System.Drawing.Point(101, 82);
            this.yCount.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.yCount.Name = "yCount";
            this.yCount.Size = new System.Drawing.Size(91, 27);
            this.yCount.TabIndex = 4;
            this.yCount.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // modes
            // 
            this.modes.BackColor = System.Drawing.Color.White;
            this.modes.Controls.Add(this.pitchMode);
            this.modes.Controls.Add(this.defaultMode);
            this.modes.Controls.Add(this.userMode);
            this.modes.Controls.Add(this.path);
            this.modes.Controls.Add(this.browse);
            this.modes.Controls.Add(this.apply);
            this.modes.Controls.Add(this.modeHint);
            this.modes.Location = new System.Drawing.Point(452, 53);
            this.modes.Name = "modes";
            this.modes.Size = new System.Drawing.Size(652, 133);
            this.modes.TabIndex = 4;
            this.modes.TabStop = false;
            this.modes.Text = "좌표 입력 방식";
            // 
            // pitchMode
            // 
            this.pitchMode.Checked = true;
            this.pitchMode.Location = new System.Drawing.Point(16, 26);
            this.pitchMode.Name = "pitchMode";
            this.pitchMode.Size = new System.Drawing.Size(90, 25);
            this.pitchMode.TabIndex = 0;
            this.pitchMode.TabStop = true;
            this.pitchMode.Text = "피치값";
            this.pitchMode.CheckedChanged += new System.EventHandler(this.ModeChanged);
            // 
            // defaultMode
            // 
            this.defaultMode.Location = new System.Drawing.Point(114, 26);
            this.defaultMode.Name = "defaultMode";
            this.defaultMode.Size = new System.Drawing.Size(130, 25);
            this.defaultMode.TabIndex = 1;
            this.defaultMode.Text = "디폴트 경로";
            this.defaultMode.CheckedChanged += new System.EventHandler(this.ModeChanged);
            // 
            // userMode
            // 
            this.userMode.Location = new System.Drawing.Point(262, 26);
            this.userMode.Name = "userMode";
            this.userMode.Size = new System.Drawing.Size(180, 25);
            this.userMode.TabIndex = 2;
            this.userMode.Text = "사용자 설정 경로";
            this.userMode.CheckedChanged += new System.EventHandler(this.ModeChanged);
            // 
            // path
            // 
            this.path.Location = new System.Drawing.Point(16, 62);
            this.path.Name = "path";
            this.path.ReadOnly = true;
            this.path.Size = new System.Drawing.Size(412, 27);
            this.path.TabIndex = 3;
            this.path.Text = "설비 파라미터 파일 형식 확인 후 연결";
            // 
            // browse
            // 
            this.browse.Enabled = false;
            this.browse.Location = new System.Drawing.Point(437, 59);
            this.browse.Name = "browse";
            this.browse.Size = new System.Drawing.Size(90, 30);
            this.browse.TabIndex = 4;
            this.browse.Text = "찾아보기";
            this.browse.Click += new System.EventHandler(this.BrowseClick);
            // 
            // apply
            // 
            this.apply.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(110)))), ((int)(((byte)(186)))));
            this.apply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.apply.ForeColor = System.Drawing.Color.White;
            this.apply.Location = new System.Drawing.Point(535, 59);
            this.apply.Name = "apply";
            this.apply.Size = new System.Drawing.Size(100, 30);
            this.apply.TabIndex = 5;
            this.apply.Text = "전체 적용";
            this.apply.UseVisualStyleBackColor = false;
            this.apply.Click += new System.EventHandler(this.ApplyPitch);
            // 
            // modeHint
            // 
            this.modeHint.Location = new System.Drawing.Point(16, 101);
            this.modeHint.Name = "modeHint";
            this.modeHint.Size = new System.Drawing.Size(620, 22);
            this.modeHint.TabIndex = 6;
            this.modeHint.Text = "위 값은 전체 적용용입니다. 각 트레이 안에서는 입력 즉시 갱신됩니다.";
            this.modeHint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // leftTray
            // 
            this.leftTray.BackColor = System.Drawing.Color.White;
            this.leftTray.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.leftTray.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.leftTray.Location = new System.Drawing.Point(16, 199);
            this.leftTray.Name = "leftTray";
            this.leftTray.Size = new System.Drawing.Size(352, 835);
            this.leftTray.TabIndex = 5;
            this.leftTray.Load += new System.EventHandler(this.leftTray_Load);
            // 
            // middleTray
            // 
            this.middleTray.BackColor = System.Drawing.Color.White;
            this.middleTray.BandColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(170)))), ((int)(((byte)(93)))));
            this.middleTray.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.middleTray.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.middleTray.Location = new System.Drawing.Point(384, 199);
            this.middleTray.Name = "middleTray";
            this.middleTray.SampleTray = TrayCoordinateHelper.TrayKind.Middle;
            this.middleTray.Size = new System.Drawing.Size(352, 835);
            this.middleTray.TabIndex = 6;
            this.middleTray.TrayTitle = "MIDDLE TRAY (GOOD)";
            this.middleTray.Load += new System.EventHandler(this.middleTray_Load);
            // 
            // rightTray
            // 
            this.rightTray.BackColor = System.Drawing.Color.White;
            this.rightTray.BandColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(170)))), ((int)(((byte)(93)))));
            this.rightTray.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rightTray.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.rightTray.Location = new System.Drawing.Point(752, 199);
            this.rightTray.Name = "rightTray";
            this.rightTray.SampleTray = TrayCoordinateHelper.TrayKind.Right;
            this.rightTray.Size = new System.Drawing.Size(352, 835);
            this.rightTray.TabIndex = 7;
            this.rightTray.TrayTitle = "RIGHT TRAY (GOOD)";
            // 
            // status
            // 
            this.status.Location = new System.Drawing.Point(21, 1050);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(855, 27);
            this.status.TabIndex = 8;
            this.status.Text = "준비 | LR 기준 좌표와 피치/카운트를 입력한 뒤 적용하세요.";
            this.status.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // reload
            // 
            this.reload.Location = new System.Drawing.Point(900, 1050);
            this.reload.Name = "reload";
            this.reload.Size = new System.Drawing.Size(98, 30);
            this.reload.TabIndex = 9;
            this.reload.Text = "RELOAD";
            this.reload.Click += new System.EventHandler(this.ReloadClick);
            // 
            // save
            // 
            this.save.Enabled = false;
            this.save.Location = new System.Drawing.Point(1006, 1050);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(98, 30);
            this.save.TabIndex = 10;
            this.save.Text = "SAVE";
            // 
            // footnote
            // 
            this.footnote.Location = new System.Drawing.Point(21, 1090);
            this.footnote.Name = "footnote";
            this.footnote.Size = new System.Drawing.Size(1080, 22);
            this.footnote.TabIndex = 11;
            this.footnote.Text = "연습용 · 피치 계산 / 샘플 복원 동작   |   개별 APPLY · 파일 불러오기 · SAVE는 연결 전";
            this.footnote.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TraySetupForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(244)))), ((int)(((byte)(248)))));
            this.ClientSize = new System.Drawing.Size(1120, 973);
            this.Controls.Add(this.heading);
            this.Controls.Add(this.subtitle);
            this.Controls.Add(this.xOptions);
            this.Controls.Add(this.yOptions);
            this.Controls.Add(this.modes);
            this.Controls.Add(this.leftTray);
            this.Controls.Add(this.middleTray);
            this.Controls.Add(this.rightTray);
            this.Controls.Add(this.status);
            this.Controls.Add(this.reload);
            this.Controls.Add(this.save);
            this.Controls.Add(this.footnote);
            this.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.MinimumSize = new System.Drawing.Size(1136, 763);
            this.Name = "TraySetupForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TRAY SETUP | 트레이 좌표 설정";
            this.xOptions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xPitch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xCount)).EndInit();
            this.yOptions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.yPitch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.yCount)).EndInit();
            this.modes.ResumeLayout(false);
            this.modes.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.Label heading;
        private System.Windows.Forms.Label subtitle;
        private System.Windows.Forms.Panel xOptions;
        private System.Windows.Forms.Label xTitle;
        private System.Windows.Forms.Label xPitchLabel;
        private System.Windows.Forms.Label xCountLabel;
        private System.Windows.Forms.NumericUpDown xPitch;
        private System.Windows.Forms.NumericUpDown xCount;
        private System.Windows.Forms.Panel yOptions;
        private System.Windows.Forms.Label yTitle;
        private System.Windows.Forms.Label yPitchLabel;
        private System.Windows.Forms.Label yCountLabel;
        private System.Windows.Forms.NumericUpDown yPitch;
        private System.Windows.Forms.NumericUpDown yCount;
        private System.Windows.Forms.GroupBox modes;
        private System.Windows.Forms.RadioButton pitchMode;
        private System.Windows.Forms.RadioButton defaultMode;
        private System.Windows.Forms.RadioButton userMode;
        private System.Windows.Forms.TextBox path;
        private System.Windows.Forms.Button browse;
        private System.Windows.Forms.Button apply;
        private System.Windows.Forms.Label modeHint;
        private TrayCoordinateHelper.TrayPanel leftTray;
        private TrayCoordinateHelper.TrayPanel middleTray;
        private TrayCoordinateHelper.TrayPanel rightTray;
        private System.Windows.Forms.Label status;
        private System.Windows.Forms.Button reload;
        private System.Windows.Forms.Button save;
        private System.Windows.Forms.Label footnote;
    }
}
