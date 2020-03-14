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
    public partial class FormSkins : Form
    {
        public FormSkins()
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

        private static int Selected = FormMain.SkinSelected;

        #region 棋盘渲染
        public static Bitmap GetChartlet(int value)
        {
            if (Selected == 0)
            {
                if (value == -1) return Properties.Resources._2;
                else if (value == 11) return Properties.Resources._2048;
                else if (value == 10) return Properties.Resources._1024;
                else if (value == 9) return Properties.Resources._512;
                else if (value == 8) return Properties.Resources._256;
                else if (value == 7) return Properties.Resources._128;
                else if (value == 6) return Properties.Resources._64;
                else if (value == 5) return Properties.Resources._32;
                else if (value == 4) return Properties.Resources._16;
                else if (value == 3) return Properties.Resources._8;
                else if (value == 2) return Properties.Resources._4;
                else if (value == 1) return Properties.Resources._2;
                else if (value == 0) return Properties.Resources.blank;
                else return Properties.Resources.unknown;
            }
            else if (Selected == 1)
            {
                if (value == -1) return Properties.Resources._2048_Classic;
                else if (value == 11) return Properties.Resources._2048_Classic;
                else if (value == 10) return Properties.Resources._1024_Classic;
                else if (value == 9) return Properties.Resources._512_Classic;
                else if (value == 8) return Properties.Resources._256_Classic;
                else if (value == 7) return Properties.Resources._128_Classic;
                else if (value == 6) return Properties.Resources._64_Classic;
                else if (value == 5) return Properties.Resources._32_Classic;
                else if (value == 4) return Properties.Resources._16_Classic;
                else if (value == 3) return Properties.Resources._8_Classic;
                else if (value == 2) return Properties.Resources._4_Classic;
                else if (value == 1) return Properties.Resources._2_Classic;
                else if (value == 0) return Properties.Resources.blank_Classic;
                else return Properties.Resources.unknown;
            }
            else if (Selected == 2)
            {
                if (value == -1) return Properties.Resources._2_Greek;
                else if (value == 11) return Properties.Resources._2048_Greek;
                else if (value == 10) return Properties.Resources._1024_Greek;
                else if (value == 9) return Properties.Resources._512_Greek;
                else if (value == 8) return Properties.Resources._256_Greek;
                else if (value == 7) return Properties.Resources._128_Greek;
                else if (value == 6) return Properties.Resources._64_Greek;
                else if (value == 5) return Properties.Resources._32_Greek;
                else if (value == 4) return Properties.Resources._16_Greek;
                else if (value == 3) return Properties.Resources._8_Greek;
                else if (value == 2) return Properties.Resources._4_Greek;
                else if (value == 1) return Properties.Resources._2_Greek;
                else if (value == 0) return Properties.Resources.blank_Greek;
                else return Properties.Resources.unknown;
            }
            else return Properties.Resources.unknown;
        }
        #endregion
        
        private void FormSkins_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 37)
            {
                Selected = ((Selected - 1) % FormMain.SkinCount + FormMain.SkinCount) % FormMain.SkinCount;
                pictureBoxView.BackgroundImage = GetChartlet(-1);
            }
            else if (e.KeyValue == 39)
            {
                Selected = ((Selected + 1) % FormMain.SkinCount + FormMain.SkinCount) % FormMain.SkinCount;
                pictureBoxView.BackgroundImage = GetChartlet(-1);
            }
            else if (e.KeyValue == 27)
            {
                Close();
            }
            else if (e.KeyValue == 13)
            {
                FormMain.SkinSelected = Selected;
                Close();
            }
        }

        private void FormSkins_Load(object sender, EventArgs e)
        {
            pictureBoxView.BackgroundImage = GetChartlet(-1);
        }

        private void labelPrev_MouseUp(object sender, MouseEventArgs e)
        {
            Selected = ((Selected - 1) % FormMain.SkinCount + FormMain.SkinCount) % FormMain.SkinCount;
            pictureBoxView.BackgroundImage = GetChartlet(-1);
        }

        private void labelNext_MouseUp(object sender, MouseEventArgs e)
        {
            Selected = ((Selected + 1) % FormMain.SkinCount + FormMain.SkinCount) % FormMain.SkinCount;
            pictureBoxView.BackgroundImage = GetChartlet(-1);
        }

        private void labelCancel_MouseUp(object sender, MouseEventArgs e) => Close();

        private void labelOk_MouseUp(object sender, MouseEventArgs e)
        {
            FormMain.SkinSelected = Selected;
            Close();
        }
    }
}
