using System;
using System.Drawing;
using System.Windows.Forms;

namespace TrayCoordinateHelper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // 숫자 입력칸의 범위와 처음 표시할 값입니다.
            numericUpDown1.DecimalPlaces = 3;
            numericUpDown1.Maximum = 10000m;
            numericUpDown1.Increment = 0.5m;
            numericUpDown1.Value = 30m;
            numericUpDown1.Location = new Point(40, 65);
            numericUpDown1.TabIndex = 0;

            numericUpDown2.DecimalPlaces = 0;
            numericUpDown2.Minimum = 1m;
            numericUpDown2.Maximum = 10000m;
            numericUpDown2.Value = 3m;
            numericUpDown2.Location = new Point(210, 65);
            numericUpDown2.TabIndex = 1;

            Label pitchLabel = new Label();
            pitchLabel.Text = "피치 (mm)";
            pitchLabel.AutoSize = true;
            pitchLabel.Location = new Point(40, 35);
            Controls.Add(pitchLabel);

            Label countLabel = new Label();
            countLabel.Text = "카운트 (간격 수)";
            countLabel.AutoSize = true;
            countLabel.Location = new Point(210, 35);
            Controls.Add(countLabel);

            button1.Location = new Point(40, 110);
            button1.Size = new Size(120, 35);
            button1.TabIndex = 2;
            Text = "피치 × 카운트 계산 연습";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 버튼을 누를 때마다 현재 입력값을 읽습니다.
            decimal pitch = numericUpDown1.Value;
            int count = decimal.ToInt32(numericUpDown2.Value);
            decimal distance = pitch * count;
            MessageBox.Show("이동 거리: " + distance + " mm", "계산 결과");
        }
    }
}
