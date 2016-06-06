using Sokoban.Core;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Sokoban
{
    public partial class GameForm : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        int cellSizePx = 25;
        bool avoidSplashLevel = false;

        public GameForm(bool showSelectLevelMenu = false)
        {
            avoidSplashLevel = showSelectLevelMenu;

            SetStyle(
                ControlStyles.OptimizedDoubleBuffer |
                    ControlStyles.AllPaintingInWmPaint,
                true);

            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (!avoidSplashLevel)
                Show_SplashLevel();
            else
            {
                G.I.Start();
                Show_SelectLevelForm();
            }
        }

        private void Show_SplashLevel()
        {
            closeLabel.Visible = true;
            G.I.Start();
            Update_GameField();
        }

        private void Update_GameField()
        {
            Text = string.IsNullOrEmpty(G.I.Logic.Map.Name.Trim()) ?
                    G.APP_NAME :
                    G.I.Logic.Map.Name + " — " + G.APP_NAME;

            G.I.View.Resize(cellSizePx);
            G.I.View.DrawField();

            Size = new Size(G.I.View.Width, G.I.View.Height);

            Invalidate();
        }

        private void Show_SelectLevelForm()
        {
            MenuForm menuForm = new MenuForm();
            DialogResult result = menuForm.ShowDialog(this);
            switch (result)
            {
                //case DialogResult.No:     // continue current level
                //    break;

                case DialogResult.Cancel:   // close app
                    Close();
                    break;

                case DialogResult.OK:       // start thinking over new level
                    Level level = menuForm.Tag as Level;
                    if (level == null)
                        Close();
                    else
                        RestartLevel(level);
                    break;
            }
        }

        private void RestartLevel(Level level)
        {
            closeLabel.Visible = false;
            G.I.Start(level);
            Update_GameField();
        }

        private void Do_Keys(KeyEventArgs e)
        {
            Point dir = Point.Empty;

            switch (e.KeyCode)
            {
                case Keys.Escape:
                    if (!G.I.IsSplashLevel)
                        Show_SelectLevelForm();
                    break;

                case Keys.Up:
                case Keys.W:
                    dir.Y = -1;
                    break;
                case Keys.Down:
                case Keys.S:
                    dir.Y = 1;
                    break;
                case Keys.Left:
                case Keys.A:
                    dir.X = -1;
                    break;
                case Keys.Right:
                case Keys.D:
                    dir.X = 1;
                    break;
                case Keys.Oemplus:
                    if (e.Control)
                    {
                        cellSizePx++;
                        Update_GameField();
                    }
                    break;
                case Keys.OemMinus:
                    if (e.Control && cellSizePx > 10)
                    {
                        cellSizePx--;
                        Update_GameField();
                    }
                    break;
                case Keys.Add:
                    cellSizePx++;
                    Update_GameField();
                    break;
                case Keys.Subtract:
                    if (cellSizePx > 10)
                    {
                        cellSizePx--;
                        Update_GameField();
                    }
                    break;
                case Keys.Back:
                    G.I.Logic.Undo();
                    G.I.View.Update();
                    Invalidate();
                    break;
                case Keys.F5:
                    RestartLevel(G.I.Logic.Map);
                    break;
            } // switch (e.KeyCode)

            if (dir.X != 0 || dir.Y != 0)
            {
                WhatsUp whatsup = G.I.Logic.MovePlayer(dir);

                G.I.View.Update();
                Invalidate();

                switch(whatsup)
                {
                    case WhatsUp.Win:
                        if (G.I.IsSplashLevel)
                            Show_SelectLevelForm();
                        else
                            Show_LevelDone();
                        break;

                    case WhatsUp.Nothing:
                        if (G.I.Logic.PlayerHx > 38)
                            Close();
                        break;
                } // switch(whatsup)
            } // if (dir.X != 0 || dir.Y != 0)
        } // Do_Keys()

        private void Show_LevelDone()
        {
            MessageBox.Show(
                string.Format(
                    "Amazing! You win!\nIn {0} steps\nAnd {1} movements of boxes\nBy the time: {2}",
                    G.I.Logic.Steps,
                    G.I.Logic.Movements,
                    G.I.ElapsedTimeLongString), 
                "Level Done!");

            closeLabel.Visible = false;
            G.I.StartNextLevel();
            Update_GameField();
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            if (G.I.View?.Canvas != null)
                e.Graphics.DrawImageUnscaled(G.I.View.Canvas, 0, 0);
        }

        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            Do_Keys(e);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
