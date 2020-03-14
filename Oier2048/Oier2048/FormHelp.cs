using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Oier2048
{
    public partial class FormHelp : Form
    {
        public FormHelp()
        {
            InitializeComponent();
        }

        #region 无边框拖动效果
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;

        private void Start_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }
        #endregion
        
        private void FormHelp_Load(object sender, EventArgs e)
        {
            pictureBox01.BackgroundImage = FormMain.GetChartlet(1);
            pictureBox02.BackgroundImage = FormMain.GetChartlet(2);
            pictureBox03.BackgroundImage = FormMain.GetChartlet(3);
            pictureBox04.BackgroundImage = FormMain.GetChartlet(4);
            pictureBox05.BackgroundImage = FormMain.GetChartlet(5);
            pictureBox06.BackgroundImage = FormMain.GetChartlet(6);
            pictureBox07.BackgroundImage = FormMain.GetChartlet(7);
            pictureBox08.BackgroundImage = FormMain.GetChartlet(8);
            pictureBox12.BackgroundImage = FormMain.GetChartlet(0);
            if (FormMain.IsUnlocked512)
            {
                pictureBox09.BackgroundImage = FormMain.GetChartlet(9);
            }
            if (FormMain.IsUnlocked1024)
            {
                pictureBox10.BackgroundImage = FormMain.GetChartlet(10);
            }
            if (FormMain.IsUnlocked2048)
            {
                pictureBox11.BackgroundImage = FormMain.GetChartlet(11);
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
