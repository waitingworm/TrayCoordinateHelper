using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TrayCoordinateHelper
{
    public enum TrayKind { Left, Middle, Right }

    // 이 부품은 TrayPanel.cs의 '디자이너 보기'에서 내부 버튼을 편집합니다.
    public partial class TrayPanel : UserControl
    {
        private readonly NumericUpDown[,] coordinates;
        private TrayKind sampleTray;
        private bool updating;
        private static readonly decimal[][,] Samples = new decimal[][,] {
                new decimal[,] { {430.296m,394.678m}, {447.697m,295.881m}, {545.612m,394.647m}, {563.013m,295.850m}, {430.304m,688.817m}, {447.705m,590.020m} },
                new decimal[,] { {586.362m,395.483m}, {603.763m,296.686m}, {701.678m,395.452m}, {719.079m,296.655m}, {586.370m,689.622m}, {603.771m,590.825m} },
                new decimal[,] { {742.319m,399.054m}, {759.720m,300.257m}, {857.635m,399.023m}, {875.036m,300.226m}, {742.327m,693.193m}, {759.728m,594.396m} }
            };

        public TrayPanel()
        {
            InitializeComponent();
            coordinates = new NumericUpDown[,] {
                { numLrX3X, numLrX3Y },
                { numLrX4X, numLrX4Y },
                { numRrX3X, numRrX3Y },
                { numRrX4X, numRrX4Y },
                { numLfX3X, numLfX3Y },
                { numLfX4X, numLfX4Y }
            };
            RestoreSample();
            threePointMode.Checked = true;
            previewSet.SelectedIndex = 0;
            localXPitch.ValueChanged += InputsChanged;
            localYPitch.ValueChanged += InputsChanged;
            localXCount.ValueChanged += InputsChanged;
            localYCount.ValueChanged += InputsChanged;
            threePointMode.CheckedChanged += InputsChanged;
            previewSet.SelectedIndexChanged += InputsChanged;
            foreach (NumericUpDown coordinate in coordinates) coordinate.ValueChanged += InputsChanged;
            preview.Paint += PreviewPaint;
            RefreshPreview();
        }

        [Category("트레이"), DefaultValue("LEFT TRAY (REWORK)")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string TrayTitle
        {
            get { return footer.Text; }
            set { footer.Text = value; }
        }

        [Category("트레이"), DefaultValue(typeof(Color), "215, 162, 29")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color BandColor
        {
            get { return footer.BackColor; }
            set { footer.BackColor = value; }
        }

        [Category("트레이"), DefaultValue(TrayKind.Left)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public TrayKind SampleTray
        {
            get { return sampleTray; }
            set { sampleTray = value; RestoreSample(); }
        }

        public void RestoreSample()
        {
            if (coordinates == null) return;
            threePointMode.Checked = true;
            ShowCoordinates(Samples[(int)sampleTray]);
            RefreshPreview();
        }

        // Count는 칸 개수입니다. 첫 칸 중심부터 마지막 칸 중심까지는 Count - 1 간격입니다.
        public void SetPitchSettings(decimal xp, int xc, decimal yp, int yc)
        {
            if (xp < 0.001m || xp > 10000m || yp < 0.001m || yp > 10000m ||
                xc < 2 || xc > 100 || yc < 2 || yc > 100)
                throw new InvalidOperationException("피치는 0.001~10000, 칸 개수는 2~100을 입력하세요.");
            Calculate(xp * (xc - 1), yp * (yc - 1));
            updating = true;
            try
            {
                localXPitch.Value = xp; localYPitch.Value = yp;
                localXCount.Value = xc; localYCount.Value = yc;
                threePointMode.Checked = false;
            }
            finally { updating = false; }
            RefreshPreview();
        }

        private void InputsChanged(object sender, EventArgs e)
        {
            if (!updating) RefreshPreview();
        }

        private void RefreshPreview()
        {
            if (coordinates == null || updating) return;
            bool measured = threePointMode.Checked;
            localXPitch.Enabled = localYPitch.Enabled = !measured;
            for (int row = 2; row < 6; row++)
                for (int axis = 0; axis < 2; axis++) coordinates[row, axis].Enabled = measured;
            try
            {
                if (!measured)
                    ShowCoordinates(Calculate(localXPitch.Value * (localXCount.Value - 1),
                        localYPitch.Value * (localYCount.Value - 1)));
                previewInfo.Text = (localXCount.Value * localYCount.Value) + "칸 · 자동 축척\n파랑: 기준점 연결\n주황: 칸 중심";
                preview.Tag = null;
                GetGridPoint(previewSet.SelectedIndex == 1 ? 1 : 0, 0, 0);
                GetGridPoint(previewSet.SelectedIndex == 1 ? 1 : 0, (int)localXCount.Value - 1, (int)localYCount.Value - 1);
            }
            catch (InvalidOperationException error)
            {
                previewInfo.Text = error.Message;
                preview.Tag = error.Message;
            }
            preview.Invalidate();
        }

        // 3점 보간 예제: LR/RR/LF를 첫/마지막 칸의 중심으로 가정합니다.
        // X3와 X4는 서로 독립적으로 계산합니다. 실제 설비의 보정 알고리즘과 연결되지 않습니다.
        public decimal[] GetGridPoint(int set, int column, int row)
        {
            int nx = (int)localXCount.Value, ny = (int)localYCount.Value;
            if (set < 0 || set > 1 || column < 0 || column >= nx || row < 0 || row >= ny)
                throw new ArgumentOutOfRangeException();
            decimal ax = coordinates[set, 0].Value, ay = coordinates[set, 1].Value;
            decimal ux = coordinates[set + 2, 0].Value - ax, uy = coordinates[set + 2, 1].Value - ay;
            decimal vx = coordinates[set + 4, 0].Value - ax, vy = coordinates[set + 4, 1].Value - ay;
            if (Math.Abs(ux * vy - uy * vx) < 0.000001m)
                throw new InvalidOperationException("LR·RR·LF가 겹치거나 일직선입니다.");
            decimal x = ax + ux * column / (nx - 1) + vx * row / (ny - 1);
            decimal y = ay + uy * column / (nx - 1) + vy * row / (ny - 1);
            if (Math.Abs(x) > 1000000m || Math.Abs(y) > 1000000m)
                throw new InvalidOperationException("미리보기 좌표 범위 초과");
            return new decimal[] { x, y };
        }

        private void PreviewPaint(object sender, PaintEventArgs e)
        {
            if (coordinates == null || preview.Tag != null) return;
            int nx = (int)localXCount.Value, ny = (int)localYCount.Value;
            int set = previewSet.SelectedIndex == 1 ? 1 : 0;
            try
            {
                decimal[][] corners = { GetGridPoint(set, 0, 0), GetGridPoint(set, nx-1, 0),
                    GetGridPoint(set, nx-1, ny-1), GetGridPoint(set, 0, ny-1) };
                double minX = (double)corners[0][0], maxX = minX, minY = (double)corners[0][1], maxY = minY;
                foreach (decimal[] point in corners)
                {
                    minX = Math.Min(minX, (double)point[0]); maxX = Math.Max(maxX, (double)point[0]);
                    minY = Math.Min(minY, (double)point[1]); maxY = Math.Max(maxY, (double)point[1]);
                }
                double scale = Math.Min((preview.Width - 40) / (maxX - minX), (preview.Height - 40) / (maxY - minY));
                double ox = (preview.Width - (maxX-minX)*scale)/2, oy = (preview.Height - (maxY-minY)*scale)/2;
                Func<decimal[], PointF> screen = point => new PointF((float)(ox+((double)point[0]-minX)*scale), (float)(oy+((double)point[1]-minY)*scale));
                PointF[] outline = new PointF[4];
                for (int i=0; i<4; i++) outline[i] = screen(corners[i]);
                e.Graphics.DrawPolygon(Pens.SteelBlue, outline);
                float size = Math.Max(1, Math.Min(8, Math.Min(preview.Width/(float)(nx+2), preview.Height/(float)(ny+2)) * 0.6f));
                for (int r=0; r<ny; r++) for (int c=0; c<nx; c++)
                {
                    PointF point = screen(GetGridPoint(set,c,r));
                    e.Graphics.FillRectangle(Brushes.DarkOrange, point.X-size/2, point.Y-size/2, size, size);
                }
                e.Graphics.DrawString("LR", Font, Brushes.Navy, outline[0].X-12, outline[0].Y-18);
                e.Graphics.DrawString("RR", Font, Brushes.Navy, outline[1].X-8, outline[1].Y-18);
                e.Graphics.DrawString("LF", Font, Brushes.Navy, outline[3].X-12, outline[3].Y+4);
            }
            catch (InvalidOperationException error) { e.Graphics.DrawString(error.Message, Font, Brushes.Firebrick, preview.ClientRectangle); }
        }

        public decimal[,] Calculate(decimal dx, decimal dy)
        {
            decimal[,] result = new decimal[6, 2];
            for (int set = 0; set < 2; set++)
            {
                decimal x = coordinates[set, 0].Value;
                decimal y = coordinates[set, 1].Value;
                result[set, 0] = x;
                result[set, 1] = y;
                result[set + 2, 0] = x + dx;
                result[set + 2, 1] = y;
                result[set + 4, 0] = x;
                result[set + 4, 1] = y + dy;
            }
            foreach (decimal value in result)
                if (value < -1000000m || value > 1000000m)
                    throw new InvalidOperationException("계산 결과가 예제 좌표 범위를 벗어났습니다.");
            return result;
        }

        public void ShowCoordinates(decimal[,] values)
        {
            bool wasUpdating = updating;
            updating = true;
            try {
            for (int row = 0; row < 6; row++)
                for (int axis = 0; axis < 2; axis++)
                    coordinates[row, axis].Value = values[row, axis];
            }
            finally { updating = wasUpdating; }
            preview.Invalidate();
        }

        private void labelRrX3Y_Click(object sender, EventArgs e)
        {

        }

        private void numRrX3Y_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
