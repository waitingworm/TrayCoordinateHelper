using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace TrayCoordinateHelper
{
    // 어느 결과 열에서 스크롤해도 같은 행끼리 비교하도록 세 TextBox를 동기화합니다.
    public class LinkedResultTextBox : TextBox
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr window, int message, IntPtr wParam, IntPtr lParam);
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public LinkedResultTextBox[] Peers { get; set; }
        private bool syncing;
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            // Scrollbar, mouse wheel, keyboard navigation and mouse selection.
            if (syncing || Peers == null || (m.Msg != 0x115 && m.Msg != 0x20A && m.Msg != 0x101 && m.Msg != 0x202 && m.Msg != 0x200)) return;
            int first = SendMessage(Handle, 0xCE, IntPtr.Zero, IntPtr.Zero).ToInt32();
            foreach (var peer in Peers)
            {
                if (peer == this || peer.IsDisposed || !peer.IsHandleCreated) continue;
                int other = SendMessage(peer.Handle, 0xCE, IntPtr.Zero, IntPtr.Zero).ToInt32();
                peer.syncing = true;
                try { SendMessage(peer.Handle, 0xB6, IntPtr.Zero, new IntPtr(first - other)); }
                finally { peer.syncing = false; }
            }
        }
    }
}
