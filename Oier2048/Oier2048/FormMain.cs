using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Oier2048
{
    public partial class FormMain : Form
    {
        private static int skinSelected = 0;
        private static int skinCount = 3;
        private static bool isUnlocked512;
        private static bool isUnlocked1024;
        private static bool isUnlocked2048;

        public int[] Blocks = new int[36];
        public int Score = 0, BestScore = 0, TimeUsedBySceonds = 0;
        public bool RecordHasSaved = true;

        public FormMain()
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
        
        #region 窗口置顶函数
        [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "GetForegroundWindow", CharSet = System.Runtime.InteropServices.CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetFormHandle();
        [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "SetForegroundWindow")]
        public static extern bool SetFrontForm(IntPtr formHandle);
        #endregion

        public static bool IsUnlocked512 { get => isUnlocked512; set => isUnlocked512 = value; }
        public static bool IsUnlocked1024 { get => isUnlocked1024; set => isUnlocked1024 = value; }
        public static bool IsUnlocked2048 { get => isUnlocked2048; set => isUnlocked2048 = value; }
        public static int SkinSelected { get => skinSelected; set => skinSelected = value; }
        public static int SkinCount { get => skinCount; set => skinCount = value; }

        #region 棋盘渲染
        public static Bitmap GetChartlet(int value)
        {
            if (SkinSelected == 0)
            {
                if (value == 11) return Properties.Resources._2048;
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
            else if (SkinSelected == 1)
            {
                if (value == 11) return Properties.Resources._2048_Classic;
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
            else if (SkinSelected == 2)
            {
                if (value == 11) return Properties.Resources._2048_Greek;
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
        private Bitmap GetChartlet(int i, int j) => GetChartlet(Blocks[i * 6 + j]);
        private void CanvasRefresh()
        {
            gameOver_1.Visible = false;
            gameOver_2.Visible = false;
            gameOver_3.Visible = false;
            gameOver_4.Visible = false;
            gameOver_5.Visible = false;
            gameOver_6.Visible = false;
            gameOver_7.Visible = false;
            gameOver_8.Visible = false;
            gameOver_Background.Visible = false;
            gameOver_Background_Top_1.Visible = false;
            gameOver_Background_Top_2.Visible = false;
            gameOver_Background_Top_3.Visible = false;
            gameOver_Background_Top_4.Visible = false;
            gameOver_Background_Top_5.Visible = false;
            gameOver_Background_Bottom_1.Visible = false;
            gameOver_Background_Bottom_2.Visible = false;
            gameOver_Background_Bottom_3.Visible = false;
            gameOver_Background_Bottom_4.Visible = false;
            gameOver_Background_Bottom_5.Visible = false;
            BestScore = Math.Max(Score, BestScore);
            labelScore.Text = Score.ToString();
            labelBestScore.Text = BestScore.ToString();
            block_1_1.BackgroundImage = GetChartlet(1, 1);
            block_1_2.BackgroundImage = GetChartlet(1, 2);
            block_1_3.BackgroundImage = GetChartlet(1, 3);
            block_1_4.BackgroundImage = GetChartlet(1, 4);
            block_2_1.BackgroundImage = GetChartlet(2, 1);
            block_2_2.BackgroundImage = GetChartlet(2, 2);
            block_2_3.BackgroundImage = GetChartlet(2, 3);
            block_2_4.BackgroundImage = GetChartlet(2, 4);
            block_3_1.BackgroundImage = GetChartlet(3, 1);
            block_3_2.BackgroundImage = GetChartlet(3, 2);
            block_3_3.BackgroundImage = GetChartlet(3, 3);
            block_3_4.BackgroundImage = GetChartlet(3, 4);
            block_4_1.BackgroundImage = GetChartlet(4, 1);
            block_4_2.BackgroundImage = GetChartlet(4, 2);
            block_4_3.BackgroundImage = GetChartlet(4, 3);
            block_4_4.BackgroundImage = GetChartlet(4, 4);
            bool isFull = true;
            for (int i = 1; i <= 4; i++)
            {
                for (int j = 1; j <= 4; j++)
                {
                    isFull &= Blocks[i * 6 + j] > 0;
                }
            }
            if (isFull)
            {
                bool canMove = false;
                for (int i = 1; i <= 4; i++)
                {
                    for (int j = 1; j <= 3; j++)
                    {
                        canMove |= Blocks[i * 6 + j] == Blocks[i * 6 + j + 1] || Blocks[j * 6 + i] == Blocks[(j + 1) * 6 + i];
                    }
                }
                if (!canMove)
                {
                    RecordHasSaved = true;
                    gameOver_1.Visible = true;
                    gameOver_2.Visible = true;
                    gameOver_3.Visible = true;
                    gameOver_4.Visible = true;
                    gameOver_5.Visible = true;
                    gameOver_6.Visible = true;
                    gameOver_7.Visible = true;
                    gameOver_8.Visible = true;
                    gameOver_Background.Visible = true;
                    gameOver_Background_Top_1.Visible = true;
                    gameOver_Background_Top_2.Visible = true;
                    gameOver_Background_Top_3.Visible = true;
                    gameOver_Background_Top_4.Visible = true;
                    gameOver_Background_Top_5.Visible = true;
                    gameOver_Background_Bottom_1.Visible = true;
                    gameOver_Background_Bottom_2.Visible = true;
                    gameOver_Background_Bottom_3.Visible = true;
                    gameOver_Background_Bottom_4.Visible = true;
                    gameOver_Background_Bottom_5.Visible = true;
                    if (backgroundWorkerTimer.WorkerSupportsCancellation == true) backgroundWorkerTimer.CancelAsync();
                }
            }
        }
        #endregion

        private void FileDragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Link;
            else e.Effect = DragDropEffects.None;
        }
        private void FileDragDrop(object sender, DragEventArgs e)
        {
            char[] index =
            {
                '0','1','2','3','4','5','6','7','8','9','A','B',
                'C','D','E','F','G','H','I','J','K','L','M','N',
                'O','P','Q','R','S','T','U','V','W','X','Y','Z'
            };
            string[] filePath = (string[])e.Data.GetData(DataFormats.FileDrop);
            DialogResult msgDialog = DialogResult.OK;
            if (!RecordHasSaved)
            {
                SetFrontForm(Handle);
                msgDialog = MessageBox.Show("已选择存档：" + filePath[0] + "，该存档将会覆盖当前游戏。", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
            if (msgDialog == DialogResult.OK)
            {
                string record = System.IO.File.ReadAllText(filePath[0]);
                bool isLegal = true;
                if (record.IndexOf(" ") < 0) isLegal = false;
                string recordBlocks = "";
                int recordScore = 0;
                if (isLegal) recordBlocks = record.Substring(0, record.IndexOf(" "));
                if (isLegal) isLegal = recordBlocks.Length == 36 && record.IndexOf(" ") == record.LastIndexOf(" ");
                if (isLegal)
                {
                    isLegal = Regex.IsMatch(record.Substring(record.LastIndexOf(" ") + 1), @"\d*");
                }
                if (isLegal)
                {
                    for (int i = 0; i < 6; i++)
                    {
                        for (int j = 0; j < 6; j++)
                        {
                            if (1 <= i && i <= 4 && 1 <= j && j <= 4)
                            {
                                isLegal &=
                                recordBlocks[i * 6 + j] >= '0' && recordBlocks[i * 6 + j] <= '9' ||
                                recordBlocks[i * 6 + j] >= 'A' && recordBlocks[i * 6 + j] <= 'B';
                            }
                            else isLegal &= isLegal &= recordBlocks[i * 6 + j] == index[j * 6 + i];
                        }
                    }
                }
                if (isLegal)
                {
                    recordScore = int.Parse(record.Substring(record.LastIndexOf(" ") + 1));
                }
                if (isLegal)
                {
                    Score = recordScore;
                    for (int i = 1; i <= 4; i++)
                    {
                        for (int j = 1; j <= 4; j++)
                        {
                            if (recordBlocks[i * 6 + j] >= '0' && recordBlocks[i * 6 + j] <= '9') Blocks[i * 6 + j] = recordBlocks[i * 6 + j] - 48;
                            else Blocks[i * 6 + j] = recordBlocks[i * 6 + j] - 55;
                        }
                    }
                    RecordHasSaved = true;
                    CanvasRefresh();
                }
                else
                {
                    MessageBox.Show("存档“" + filePath[0] + "”不合法，请选择其他存档。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void ActUp()
        {
            int[] NewBlocks = new int[36];
            bool[] isLocked = new bool[36];
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    NewBlocks[i * 6 + j] = Blocks[i * 6 + j];
                    isLocked[i * 6 + j] = false;
                }
            }
            for (int i = 1; i <= 4; i++)
            {
                for (int j = 1; j <= 3; j++)
                {
                    for (int k = 1; k <= (4 - j); k++)
                    {
                        if (NewBlocks[k * 6 + i] == NewBlocks[(k + 1) * 6 + i] && NewBlocks[k * 6 + i] > 0 && !isLocked[k * 6 + i] && !isLocked[(k + 1) * 6 + i])
                        {
                            NewBlocks[k * 6 + i]++;
                            Score += (int)Math.Pow(2.0, (double)NewBlocks[k * 6 + i]);
                            if (NewBlocks[k * 6 + i] == 9) isUnlocked512 = true;
                            if (NewBlocks[k * 6 + i] == 10) isUnlocked1024 = true;
                            if (NewBlocks[k * 6 + i] == 11) isUnlocked2048 = true;
                            isLocked[k * 6 + i] = true;
                            NewBlocks[(k + 1) * 6 + i] = 0;
                            isLocked[(k + 1) * 6 + i] = false;
                            break;
                        }
                    }
                    for (int k = 1; k <= 4; k++)
                    {
                        int l = k;
                        while (NewBlocks[--l * 6 + i] == 0 && l >= 1)
                        {
                            NewBlocks[l * 6 + i] = NewBlocks[(l + 1) * 6 + i];
                            isLocked[l * 6 + i] = isLocked[(l + 1) * 6 + i];
                            NewBlocks[(l + 1) * 6 + i] = 0;
                            isLocked[(l + 1) * 6 + i] = false;
                        }
                    }
                }
                for (int k = 1; k <= 4; k++)
                {
                    int l = k;
                    while (NewBlocks[--l * 6 + i] == 0 && l >= 1)
                    {
                        NewBlocks[l * 6 + i] = NewBlocks[(l + 1) * 6 + i];
                        isLocked[l * 6 + i] = isLocked[(l + 1) * 6 + i];
                        NewBlocks[(l + 1) * 6 + i] = 0;
                        isLocked[(l + 1) * 6 + i] = false;
                    }
                }
            }
            bool isSameBlocks = true;
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    isSameBlocks &= NewBlocks[i * 6 + j] == Blocks[i * 6 + j];
                }
            }
            if (!isSameBlocks)
            {
                RecordHasSaved = false;
                for (int i = 0; i < 6; i++)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        Blocks[i * 6 + j] = NewBlocks[i * 6 + j];
                    }
                }
                int[] BlankX = new int[36];
                int[] BlankY = new int[36];
                int BlankCount = 0;
                for (int i = 1; i <= 4; i++)
                {
                    for (int j = 1; j <= 4; j++)
                    {
                        if (Blocks[i * 6 + j] == 0)
                        {
                            BlankX[BlankCount] = i;
                            BlankY[BlankCount] = j;
                            BlankCount++;
                        }
                    }
                }
                if (BlankCount > 0)
                {
                    Random random = new Random();
                    int NewBlockIndex = random.Next() % BlankCount;
                    int NewBlockValue = (random.Next() % 5 == 0 ? 1 : 0) + 1;
                    Blocks[BlankX[NewBlockIndex] * 6 + BlankY[NewBlockIndex]] = NewBlockValue;
                }
                CanvasRefresh();
            }
        }

        private void ActDown()
        {
            int[] NewBlocks = new int[36];
            bool[] isLocked = new bool[36];
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    NewBlocks[i * 6 + j] = Blocks[i * 6 + j];
                    isLocked[i * 6 + j] = false;
                }
            }
            for (int i = 1; i <= 4; i++)
            {
                for (int j = 4; j >= 2; j--)
                {
                    for (int k = 4; k >= (4 - j + 1); k--)
                    {
                        if (NewBlocks[k * 6 + i] == NewBlocks[(k - 1) * 6 + i] && NewBlocks[k * 6 + i] > 0 && !isLocked[k * 6 + i] && !isLocked[(k - 1) * 6 + i])
                        {
                            NewBlocks[k * 6 + i]++;
                            Score += (int)Math.Pow(2.0, (double)NewBlocks[k * 6 + i]);
                            if (NewBlocks[k * 6 + i] == 9) isUnlocked512 = true;
                            if (NewBlocks[k * 6 + i] == 10) isUnlocked1024 = true;
                            if (NewBlocks[k * 6 + i] == 11) isUnlocked2048 = true;
                            isLocked[k * 6 + i] = true;
                            NewBlocks[(k - 1) * 6 + i] = 0;
                            isLocked[(k - 1) * 6 + i] = false;
                            break;
                        }
                    }
                    for (int k = 4; k >= 1; k--)
                    {
                        int l = k;
                        while (NewBlocks[++l * 6 + i] == 0 && l <= 4)
                        {
                            NewBlocks[l * 6 + i] = NewBlocks[(l - 1) * 6 + i];
                            isLocked[l * 6 + i] = isLocked[(l - 1) * 6 + i];
                            NewBlocks[(l - 1) * 6 + i] = 0;
                            isLocked[(l - 1) * 6 + i] = false;
                        }
                    }
                }
                for (int k = 4; k >= 1; k--)
                {
                    int l = k;
                    while (NewBlocks[++l * 6 + i] == 0 && l <= 4)
                    {
                        NewBlocks[l * 6 + i] = NewBlocks[(l - 1) * 6 + i];
                        isLocked[l * 6 + i] = isLocked[(l - 1) * 6 + i];
                        NewBlocks[(l - 1) * 6 + i] = 0;
                        isLocked[(l - 1) * 6 + i] = false;
                    }
                }
            }
            bool isSameBlocks = true;
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    isSameBlocks &= NewBlocks[i * 6 + j] == Blocks[i * 6 + j];
                }
            }
            if (!isSameBlocks)
            {
                RecordHasSaved = false;
                for (int i = 0; i < 6; i++)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        Blocks[i * 6 + j] = NewBlocks[i * 6 + j];
                    }
                }
                int[] BlankX = new int[36];
                int[] BlankY = new int[36];
                int BlankCount = 0;
                for (int i = 1; i <= 4; i++)
                {
                    for (int j = 1; j <= 4; j++)
                    {
                        if (Blocks[i * 6 + j] == 0)
                        {
                            BlankX[BlankCount] = i;
                            BlankY[BlankCount] = j;
                            BlankCount++;
                        }
                    }
                }
                if (BlankCount > 0)
                {
                    Random random = new Random();
                    int NewBlockIndex = random.Next() % BlankCount;
                    int NewBlockValue = (random.Next() % 5 == 0 ? 1 : 0) + 1;
                    Blocks[BlankX[NewBlockIndex] * 6 + BlankY[NewBlockIndex]] = NewBlockValue;
                }
                CanvasRefresh();
            }
        }

        private void ActLeft()
        {
            int[] NewBlocks = new int[36];
            bool[] isLocked = new bool[36];
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    NewBlocks[i * 6 + j] = Blocks[i * 6 + j];
                    isLocked[i * 6 + j] = false;
                }
            }
            for (int i = 1; i <= 4; i++)
            {
                for (int j = 1; j <= 3; j++)
                {
                    for (int k = 1; k <= (4 - j); k++)
                    {
                        if (NewBlocks[i * 6 + k] == NewBlocks[i * 6 + k + 1] && NewBlocks[i * 6 + k] > 0 && !isLocked[i * 6 + k] && !isLocked[i * 6 + k + 1])
                        {
                            NewBlocks[i * 6 + k]++;
                            Score += (int)Math.Pow(2.0, (double)NewBlocks[i * 6 + k]);
                            if (NewBlocks[i * 6 + k] == 9) isUnlocked512 = true;
                            if (NewBlocks[i * 6 + k] == 10) isUnlocked1024 = true;
                            if (NewBlocks[i * 6 + k] == 11) isUnlocked2048 = true;
                            isLocked[i * 6 + k] = true;
                            NewBlocks[i * 6 + k + 1] = 0;
                            isLocked[i * 6 + k - 1] = false;
                            break;
                        }
                    }
                    for (int k = 1; k <= 4; k++)
                    {
                        int l = k;
                        while (NewBlocks[i * 6 + (--l)] == 0 && l >= 1)
                        {
                            NewBlocks[i * 6 + l] = NewBlocks[i * 6 + l + 1];
                            isLocked[i * 6 + l] = isLocked[i * 6 + l + 1];
                            NewBlocks[i * 6 + l + 1] = 0;
                            isLocked[i * 6 + l + 1] = false;
                        }
                    }
                }
                for (int k = 1; k <= 4; k++)
                {
                    int l = k;
                    while (NewBlocks[i * 6 + (--l)] == 0 && l >= 1)
                    {
                        NewBlocks[i * 6 + l] = NewBlocks[i * 6 + l + 1];
                        isLocked[i * 6 + l] = isLocked[i * 6 + l + 1];
                        NewBlocks[i * 6 + l + 1] = 0;
                        isLocked[i * 6 + l + 1] = false;
                    }
                }
            }
            bool isSameBlocks = true;
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    isSameBlocks &= NewBlocks[i * 6 + j] == Blocks[i * 6 + j];
                }
            }
            if (!isSameBlocks)
            {
                RecordHasSaved = false;
                for (int i = 0; i < 6; i++)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        Blocks[i * 6 + j] = NewBlocks[i * 6 + j];
                    }
                }
                int[] BlankX = new int[36];
                int[] BlankY = new int[36];
                int BlankCount = 0;
                for (int i = 1; i <= 4; i++)
                {
                    for (int j = 1; j <= 4; j++)
                    {
                        if (Blocks[i * 6 + j] == 0)
                        {
                            BlankX[BlankCount] = i;
                            BlankY[BlankCount] = j;
                            BlankCount++;
                        }
                    }
                }
                if (BlankCount > 0)
                {
                    Random random = new Random();
                    int NewBlockIndex = random.Next() % BlankCount;
                    int NewBlockValue = (random.Next() % 5 == 0 ? 1 : 0) + 1;
                    Blocks[BlankX[NewBlockIndex] * 6 + BlankY[NewBlockIndex]] = NewBlockValue;
                }
                CanvasRefresh();
            }
        }

        private void ActRight()
        {
            int[] NewBlocks = new int[36];
            bool[] isLocked = new bool[36];
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    NewBlocks[i * 6 + j] = Blocks[i * 6 + j];
                    isLocked[i * 6 + j] = false;
                }
            }
            for (int i = 1; i <= 4; i++)
            {
                for (int j = 4; j >= 2; j--)
                {
                    for (int k = 4; k >= (4 - j + 1); k--)
                    {
                        if (NewBlocks[i * 6 + k] == NewBlocks[i * 6 + k - 1] && NewBlocks[i * 6 + k] > 0 && !isLocked[i * 6 + k] && !isLocked[i * 6 + k - 1])
                        {
                            NewBlocks[i * 6 + k]++;
                            Score += (int)Math.Pow(2.0, (double)NewBlocks[i * 6 + k]);
                            if (NewBlocks[i * 6 + k] == 9) isUnlocked512 = true;
                            if (NewBlocks[i * 6 + k] == 10) isUnlocked1024 = true;
                            if (NewBlocks[i * 6 + k] == 11) isUnlocked2048 = true;
                            isLocked[i * 6 + k] = true;
                            NewBlocks[i * 6 + k - 1] = 0;
                            isLocked[i * 6 + k - 1] = false;
                            break;
                        }
                    }
                    for (int k = 4; k >= 1; k--)
                    {
                        int l = k;
                        while (NewBlocks[i * 6 + (++l)] == 0 && l <= 4)
                        {
                            NewBlocks[i * 6 + l] = NewBlocks[i * 6 + l - 1];
                            isLocked[i * 6 + l] = isLocked[i * 6 + l - 1];
                            NewBlocks[i * 6 + l - 1] = 0;
                            isLocked[i * 6 + l - 1] = false;
                        }
                    }
                }
                for (int k = 4; k >= 1; k--)
                {
                    int l = k;
                    while (NewBlocks[i * 6 + (++l)] == 0 && l <= 4)
                    {
                        NewBlocks[i * 6 + l] = NewBlocks[i * 6 + l - 1];
                        isLocked[i * 6 + l] = isLocked[i * 6 + l - 1];
                        NewBlocks[i * 6 + l - 1] = 0;
                        isLocked[i * 6 + l - 1] = false;
                    }
                }
            }
            bool isSameBlocks = true;
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    isSameBlocks &= NewBlocks[i * 6 + j] == Blocks[i * 6 + j];
                }
            }
            if (!isSameBlocks)
            {
                RecordHasSaved = false;
                for (int i = 0; i < 6; i++)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        Blocks[i * 6 + j] = NewBlocks[i * 6 + j];
                    }
                }
                int[] BlankX = new int[36];
                int[] BlankY = new int[36];
                int BlankCount = 0;
                for (int i = 1; i <= 4; i++)
                {
                    for (int j = 1; j <= 4; j++)
                    {
                        if (Blocks[i * 6 + j] == 0)
                        {
                            BlankX[BlankCount] = i;
                            BlankY[BlankCount] = j;
                            BlankCount++;
                        }
                    }
                }
                if (BlankCount > 0)
                {
                    Random random = new Random();
                    int NewBlockIndex = random.Next() % BlankCount;
                    int NewBlockValue = (random.Next() % 5 == 0 ? 1 : 0) + 1;
                    Blocks[BlankX[NewBlockIndex] * 6 + BlankY[NewBlockIndex]] = NewBlockValue;
                }
                CanvasRefresh();
            }
        }

        private void ActNewGame()
        {
            if (backgroundWorkerTimer.IsBusy == true) backgroundWorkerTimer.CancelAsync();
            SetFrontForm(Handle);
            DialogResult msgDialog = DialogResult.OK;
            if (!RecordHasSaved)
            {
                SetFrontForm(Handle);
                msgDialog = MessageBox.Show("当前游戏尚未保存，您确定要开始新的游戏吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
            if (msgDialog != DialogResult.OK)
            {
                if (backgroundWorkerTimer.IsBusy != true) backgroundWorkerTimer.RunWorkerAsync();
                return;
            }
            Score = 0;
            RecordHasSaved = false;
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    Blocks[i * 6 + j] = 0;
                }
            }
            Random random = new Random();
            int newBlockMode = random.Next() % 4;
            if (newBlockMode == 0)
            {
                int newBlockY1 = random.Next() % 4 + 1;
                int newBlockY2 = random.Next() % 3 + 1;
                Blocks[1 * 6 + newBlockY1] = 1;
                if (newBlockY1 > newBlockY2)
                {
                    Blocks[1 * 6 + newBlockY2] = 1;
                }
                else
                {
                    Blocks[1 * 6 + newBlockY2 + 1] = 1;
                }
            }
            else if (newBlockMode == 1)
            {
                int newBlockX1 = random.Next() % 4 + 1;
                int newBlockX2 = random.Next() % 3 + 1;
                Blocks[newBlockX1 * 6 + 1] = 1;
                if (newBlockX1 > newBlockX2)
                {
                    Blocks[newBlockX2 * 6 + 1] = 1;
                }
                else
                {
                    Blocks[(newBlockX2 + 1) * 6 + 1] = 1;
                }
            }
            else if (newBlockMode == 2)
            {
                int newBlockX1 = random.Next() % 4 + 1;
                int newBlockX2 = random.Next() % 3 + 1;
                Blocks[newBlockX1 * 6 + 4] = 1;
                if (newBlockX1 > newBlockX2)
                {
                    Blocks[newBlockX2 * 6 + 4] = 1;
                }
                else
                {
                    Blocks[(newBlockX2 + 1) * 6 + 4] = 1;
                }
            }
            else
            {
                int newBlockY1 = random.Next() % 4 + 1;
                int newBlockY2 = random.Next() % 3 + 1;
                Blocks[4 * 6 + newBlockY1] = 1;
                if (newBlockY1 > newBlockY2)
                {
                    Blocks[4 * 6 + newBlockY2] = 1;
                }
                else
                {
                    Blocks[4 * 6 + newBlockY2 + 1] = 1;
                }
            }
            CanvasRefresh();
            TimeUsedBySceonds = 0;
            labelTimeUsed.Text =
                 ((TimeUsedBySceonds / 3600) >= 1 ? ((TimeUsedBySceonds / 3600).ToString() + ":") : "") +
                 ((TimeUsedBySceonds / 60 % 60) < 10 ? "0" : "") + (TimeUsedBySceonds / 60 % 60).ToString() + ":" +
                 ((TimeUsedBySceonds % 60) < 10 ? "0" : "") + (TimeUsedBySceonds % 60).ToString();
            backgroundWorkerTimer.WorkerReportsProgress = true;
            backgroundWorkerTimer.WorkerSupportsCancellation = true;
            if (backgroundWorkerTimer.IsBusy != true) backgroundWorkerTimer.RunWorkerAsync();
        }

        private void ActNewGame(object sender, EventArgs e) => ActNewGame();

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult msgDialog = DialogResult.OK;
            if (!RecordHasSaved)
            {
                SetFrontForm(Handle);
                msgDialog = MessageBox.Show("当前游戏尚未保存，您确定要退出吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
            if (msgDialog == DialogResult.OK)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void SaveRecord()
        {
            bool TimerIsStoped = false;
            if (backgroundWorkerTimer.IsBusy == true) backgroundWorkerTimer.CancelAsync();
            else TimerIsStoped = true;
            char[] index =
            {
                '0','1','2','3','4','5','6','7','8','9','A','B',
                'C','D','E','F','G','H','I','J','K','L','M','N',
                'O','P','Q','R','S','T','U','V','W','X','Y','Z'
            };
            SaveFileDialog fileDialog = new SaveFileDialog
            {
                RestoreDirectory = true,
                Title = "保存存档",
                Filter = "Oier2048存档(3.0.0及以上版本)(*.oier2048x)|*.oier2048x|Oier2048存档(3.0.0以下版本)(*.oier2048)|*.oier2048"
            };
            SetFrontForm(Handle);
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                string localFilePath = fileDialog.FileName.ToString();
                bool isNewVersion = localFilePath.Substring(localFilePath.LastIndexOf(".") + 1, localFilePath.Length - localFilePath.LastIndexOf(".") - 1).ToLower() == "oier2048x";
                System.IO.FileStream fileStream = (System.IO.FileStream)fileDialog.OpenFile();
                string record = "";
                if (isNewVersion)
                {
                    int password = (Score + (TimeUsedBySceonds % 13 + 7)) % (TimeUsedBySceonds % 36 + 1);
                    for (int i = 0; i < 6; i++)
                    {
                        for (int j = 0; j < 6; j++)
                        {
                            int unencode = (1 <= i && i <= 4 && 1 <= j && j <= 4 ? Blocks[i * 6 + j] : j * 6 + i);
                            if ((i * 6 + j) % 5 == 0 || (i * 6 + j) % 5 == 2 || (i * 6 + j) % 5 == 3)
                            {
                                record += index[(unencode + password) % 36];
                            }
                            else
                            {
                                record += index[((unencode + password) % 36) % 6 * 6 + ((unencode + password) % 36) / 6];
                            }
                        }
                    }
                    record += '-';
                    record += Score.ToString();
                    record += '/';
                    record += TimeUsedBySceonds.ToString();
                }
                else
                {
                    for (int i = 0; i < 6; i++)
                    {
                        for (int j = 0; j < 6; j++)
                        {
                            record += index[(1 <= i && i <= 4 && 1 <= j && j <= 4 ? Blocks[i * 6 + j] : j * 6 + i)];
                        }
                    }
                    record += ' ';
                    record += Score.ToString();
                    record += '~';
                    record += TimeUsedBySceonds.ToString();
                }
                byte[] bytes = new ASCIIEncoding().GetBytes(record);
                fileStream.Write(bytes, 0, bytes.Length);
                fileStream.Close();
                RecordHasSaved = true;
            }
            if (backgroundWorkerTimer.IsBusy != true && !TimerIsStoped) backgroundWorkerTimer.RunWorkerAsync();
        }

        private void OpenRecord()
        {
            bool TimerIsStoped = false;
            if (backgroundWorkerTimer.IsBusy == true) backgroundWorkerTimer.CancelAsync();
            else TimerIsStoped = true;
            char[] index =
            {
                '0','1','2','3','4','5','6','7','8','9','A','B',
                'C','D','E','F','G','H','I','J','K','L','M','N',
                'O','P','Q','R','S','T','U','V','W','X','Y','Z'
            };
            OpenFileDialog fileDialog = new OpenFileDialog
            {
                Multiselect = false,
                RestoreDirectory = true,
                Title = "载入存档",
                Filter = "Oier2048存档(3.0.0及以上版本)(*.oier2048x)|*.oier2048x|Oier2048存档(3.0.0以下版本)(*.oier2048)|*.oier2048"
            };
            SetFrontForm(Handle);
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                string file = fileDialog.FileName;
                bool isNewVersion = file.Substring(file.LastIndexOf(".") + 1, file.Length - file.LastIndexOf(".") - 1).ToLower() == "oier2048x";
                string record = System.IO.File.ReadAllText(file);
                bool isLegal = true;
                if (record.IndexOf(" ") < 0) isLegal = false;
                string recordBlocks = "", tempRecordBlocks = "";
                int recordScore = 0, recordTimeUsed = 0;
                if (isNewVersion)
                {
                    if (isLegal) recordBlocks = record.Substring(0, record.IndexOf("-"));
                    if (isLegal) isLegal = recordBlocks.Length == 36 && record.IndexOf("-") == record.LastIndexOf("-") && record.IndexOf("/") == record.LastIndexOf("/");
                    if (isLegal)
                    {
                        if (record.LastIndexOf("/") > record.LastIndexOf("-"))
                        {
                            isLegal = Regex.IsMatch(record.Substring(record.LastIndexOf("-") + 1, record.LastIndexOf("/") - record.LastIndexOf("-") - 1), @"\d*");
                        }
                        else isLegal = false;
                    }
                    if (isLegal)
                    {
                        recordScore = int.Parse(record.Substring(record.LastIndexOf(" ") + 1, record.LastIndexOf("~") - record.LastIndexOf(" ") - 1));
                        recordTimeUsed = int.Parse(record.Substring((record.LastIndexOf("~") + 1)));
                        int password = (recordScore + (recordTimeUsed % 13 + 7)) % (recordTimeUsed % 36 + 1);
                        for (int i = 0; i < 6; i++)
                        {
                            for (int j = 0; j < 6; j++)
                            {
                                int unencode;
                                if (recordBlocks[i * 6 + j] >= 'A' && recordBlocks[i * 6 + j] <= 'Z')
                                {
                                    unencode = recordBlocks[i * 6 + j] - 'A' + 10;
                                }
                                else
                                {
                                    unencode = recordBlocks[i * 6 + j] - '0';
                                }
                                if ((i * 6 + j) % 5 == 0 || (i * 6 + j) % 5 == 2 || (i * 6 + j) % 5 == 3)
                                {
                                    unencode = (unencode + 36 - password) % 36;
                                }
                                else
                                {
                                    unencode = ((unencode + 36 - password) % 36) % 6 * 6 + ((unencode + 36 - password) % 36) / 6;
                                }
                                if (1 <= i && i <= 4 && 1 <= j && j <= 4)
                                {
                                    isLegal &= unencode >= '0' && unencode <= '9' || unencode >= 'A' && unencode <= 'B';
                                }
                                else
                                {
                                    isLegal &= unencode == (j * 6 + i);
                                    unencode = (j * 6 + i);
                                    tempRecordBlocks += index[unencode];
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (isLegal) recordBlocks = record.Substring(0, record.IndexOf(" "));
                    if (isLegal) isLegal = recordBlocks.Length == 36 && record.IndexOf(" ") == record.LastIndexOf(" ") && record.IndexOf("~") == record.LastIndexOf("~");
                    if (isLegal)
                    {
                        if (record.LastIndexOf("~") > record.LastIndexOf(" "))
                        {
                            isLegal = Regex.IsMatch(record.Substring(record.LastIndexOf(" ") + 1, record.LastIndexOf("~") - record.LastIndexOf(" ") - 1), @"\d*");
                        }
                        else isLegal = false;
                    }
                    if (isLegal)
                    {
                        for (int i = 0; i < 6; i++)
                        {
                            for (int j = 0; j < 6; j++)
                            {
                                if (1 <= i && i <= 4 && 1 <= j && j <= 4)
                                {
                                    isLegal &=
                                    recordBlocks[i * 6 + j] >= '0' && recordBlocks[i * 6 + j] <= '9' ||
                                    recordBlocks[i * 6 + j] >= 'A' && recordBlocks[i * 6 + j] <= 'B';
                                }
                                else isLegal &= isLegal &= recordBlocks[i * 6 + j] == index[j * 6 + i];
                            }
                        }
                    }
                    if (isLegal)
                    {
                        recordScore = int.Parse(record.Substring(record.LastIndexOf(" ") + 1, record.LastIndexOf("~") - record.LastIndexOf(" ") - 1));
                        recordTimeUsed = int.Parse(record.Substring((record.LastIndexOf("~") + 1)));
                    }
                }
                if (isLegal)
                {
                    DialogResult msgDialog = DialogResult.Cancel;
                    if (!RecordHasSaved)
                    {
                        SetFrontForm(Handle);
                        msgDialog = MessageBox.Show("已选择存档：" + file + "，该存档将会覆盖当前游戏。", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    }
                    if (msgDialog != DialogResult.OK)
                    {
                        goto cancelPoint;
                    }
                    Score = recordScore;
                    TimeUsedBySceonds = recordTimeUsed;
                    if (isNewVersion)
                    {
                        for (int i = 1; i <= 4; i++)
                        {
                            for (int j = 1; j <= 4; j++)
                            {
                                if (tempRecordBlocks[i * 6 + j] >= '0' && tempRecordBlocks[i * 6 + j] <= '9') Blocks[i * 6 + j] = tempRecordBlocks[i * 6 + j] - 48;
                                else Blocks[i * 6 + j] = tempRecordBlocks[i * 6 + j] - 55;
                                if (tempRecordBlocks[i * 6 + j] >= 9) IsUnlocked512 = true;
                                if (tempRecordBlocks[i * 6 + j] >= 10) IsUnlocked1024 = true;
                                if (tempRecordBlocks[i * 6 + j] >= 11) IsUnlocked2048 = true;
                            }
                        }
                    }
                    else
                    {
                        for (int i = 1; i <= 4; i++)
                        {
                            for (int j = 1; j <= 4; j++)
                            {
                                if (recordBlocks[i * 6 + j] >= '0' && recordBlocks[i * 6 + j] <= '9') Blocks[i * 6 + j] = recordBlocks[i * 6 + j] - 48;
                                else Blocks[i * 6 + j] = recordBlocks[i * 6 + j] - 55;
                                if (recordBlocks[i * 6 + j] >= 9) IsUnlocked512 = true;
                                if (recordBlocks[i * 6 + j] >= 10) IsUnlocked1024 = true;
                                if (recordBlocks[i * 6 + j] >= 11) IsUnlocked2048 = true;
                            }
                        }
                    }
                    RecordHasSaved = true;
                    CanvasRefresh();
                    labelTimeUsed.Text =
                        ((TimeUsedBySceonds / 3600) >= 1 ? ((TimeUsedBySceonds / 3600).ToString() + ":") : "") +
                        ((TimeUsedBySceonds / 60 % 60) < 10 ? "0" : "") + (TimeUsedBySceonds / 60 % 60).ToString() + ":" +
                        ((TimeUsedBySceonds % 60) < 10 ? "0" : "") + (TimeUsedBySceonds % 60).ToString();
                }
                else
                {
                    MessageBox.Show("存档“" + file + "”不合法，请选择其他存档。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            bool isFull = true;
            for (int i = 1; i <= 4; i++)
            {
                for (int j = 1; j <= 4; j++)
                {
                    isFull &= Blocks[i * 6 + j] > 0;
                }
            }
            if (isFull)
            {
                bool canMove = false;
                for (int i = 1; i <= 4; i++)
                {
                    for (int j = 1; j <= 3; j++)
                    {
                        canMove |= Blocks[i * 6 + j] == Blocks[i * 6 + j + 1] || Blocks[j * 6 + i] == Blocks[(j + 1) * 6 + i];
                    }
                }
                if (!canMove)
                {
                    backgroundWorkerTimer.WorkerReportsProgress = true;
                    backgroundWorkerTimer.WorkerSupportsCancellation = true;
                    return;
                }
            }
            cancelPoint:
            backgroundWorkerTimer.WorkerReportsProgress = true;
            backgroundWorkerTimer.WorkerSupportsCancellation = true;
            if (backgroundWorkerTimer.IsBusy != true && !TimerIsStoped) backgroundWorkerTimer.RunWorkerAsync();
        }

        private void ToolStripMenuItemSave_Click(object sender, EventArgs e) => SaveRecord();

        private void ToolStripMenuItemOpen_Click(object sender, EventArgs e) => OpenRecord();

        private void ToolStripMenuItemNewGame_Click(object sender, EventArgs e) => ActNewGame();

        private void buttonHowToPlay_MouseUp(object sender, MouseEventArgs e) => labelHowToPlay.Visible = !labelHowToPlay.Visible;

        private void backgroundWorkerTimer_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            labelTimeUsed.Text =
                ((TimeUsedBySceonds / 3600) >= 1 ? ((TimeUsedBySceonds / 3600).ToString() + ":") : "") +
                ((TimeUsedBySceonds / 60 % 60) < 10 ? "0" : "") + (TimeUsedBySceonds / 60 % 60).ToString() + ":" +
                ((TimeUsedBySceonds % 60) < 10 ? "0" : "") + (TimeUsedBySceonds % 60).ToString();
        }

        private void backgroundWorkerTimer_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                labelTimeUsed.Text = "Error!";
            }
        }

        private void backgroundWorkerTimer_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            while (true)
            {
                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    Thread.Sleep(985);
                    worker.ReportProgress(TimeUsedBySceonds);
                    TimeUsedBySceonds++;
                }
            }
        }

        private void FormMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 37 || e.KeyValue == 65)
            {
                ActLeft();
            }
            else if (e.KeyValue == 38 || e.KeyValue == 87)
            {
                ActUp();
            }
            else if (e.KeyValue == 39 || e.KeyValue == 68)
            {
                ActRight();
            }
            else if (e.KeyValue == 40 || e.KeyValue == 83)
            {
                ActDown();
            }
            else if (e.KeyValue == 27)
            {
                Close();
            }
            else if (e.KeyValue == 48 || e.KeyValue == 78)
            {
                ActNewGame();
            }
            else if (e.KeyValue == 49)
            {
                FormAbout formAbout = new FormAbout();
                formAbout.ShowDialog();
            }
            else if (e.KeyValue == 50)
            {
                FormHelp formHelp = new FormHelp();
                formHelp.ShowDialog();
            }
            else if (e.KeyValue == 9)
            {
                FormSkins formSkins = new FormSkins();
                formSkins.ShowDialog();
                CanvasRefresh();
            }
            else if (e.KeyValue == 16)
            {
                TopMost = !TopMost;
                ShowInTaskbar = !TopMost;
                labelHowToPlay.Text = @"- 上　字母键W / 方向键上
- 下　字母键S / 方向键下
- 左　字母键A / 方向键左
- 右　字母键D / 方向键右
- 载入存档　句号 / Ctrl+O
- 保存存档　逗号 / Ctrl+S
- 新的游戏　数字键0 / Ctrl+N
- 关于　数字键1　- 帮助　数字键2
- 换肤　Tab键　- 退出　Esc键
- 窗口置顶锁定　Shift键（" + (TopMost ? "已" : "未") + "锁定）";
            }
            else if (e.KeyValue == 188)
            {
                SaveRecord();
            }
            else if (e.KeyValue == 190 || e.KeyValue == 79)
            {
                OpenRecord();
            }
        }
    }
}
