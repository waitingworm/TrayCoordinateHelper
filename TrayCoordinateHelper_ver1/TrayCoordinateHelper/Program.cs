using System;
using System.Drawing;
using System.Windows.Forms;

namespace TrayCoordinateHelper
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var window = new Form
            {
                Text = "TrayCoordinateHelper · 프로토콜 분석 / 트레이 설정",
                ClientSize = new Size(1440, 900),
                MinimumSize = new Size(1000, 700),
                StartPosition = FormStartPosition.CenterScreen,
                WindowState = FormWindowState.Maximized
            };
            var tabs = new TabControl { Dock = DockStyle.Fill, Font = new Font("맑은 고딕", 11F) };
            var protocolPage = new TabPage("프로토콜 분석");
            var trayPage = new TabPage("트레이 설정") { AutoScroll = true };
            protocolPage.Controls.Add(new ProtocolPanel { Dock = DockStyle.Fill });
            var tray = new TraySetupForm { TopLevel = false, FormBorderStyle = FormBorderStyle.None };
            trayPage.Controls.Add(tray);
            tabs.TabPages.Add(protocolPage);
            tabs.TabPages.Add(trayPage);
            window.Controls.Add(tabs);
            tray.Show();
            Application.Run(window);
        }
    }
}
