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

        View view;
        Logic logic;
        bool avoidSplashLevel = false;

        public GameForm(bool goSelectLevel = false)
        {
            avoidSplashLevel = goSelectLevel;

            SetStyle(
                ControlStyles.OptimizedDoubleBuffer |
                    ControlStyles.AllPaintingInWmPaint,
                true);

            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (avoidSplashLevel)
                Show_SelectLevelForm();
            else
                Show_SplashLevel();
        }

        private void Show_SplashLevel()
        {
            logic = new Logic(G.I.SplashLevel);
            view = new View(logic);

            Resize_GameField();
        }

        private void Resize_GameField()
        {
            Text = G.I.LevelNo == -1 ?
                    G.APP_NAME :
                    G.I.Level.Name + " — " + G.APP_NAME;

            view.Resize(G.I.Zoom);
            view.DrawField();

            Width = view.Width;
            Height = view.Height;

            Invalidate();
        }

        private void Show_SelectLevelForm()
        {
            MenuForm menuForm = new MenuForm(logic);
            DialogResult result = menuForm.ShowDialog();
            switch (result)
            {
                //case DialogResult.No:     // continue current level
                //    break;

                case DialogResult.Cancel:   // close app
                    Close();
                    break;

                case DialogResult.OK:       // start thinking over new level
                    logic = new Logic(G.I.Level);
                    view = new View(logic);
                    break;
            }

            Resize_GameField();
        }

        private void Do_Keys(KeyEventArgs e)
        {
            Point dir = Point.Empty;

            switch (e.KeyCode)
            {
                case Keys.Escape:
                    if (G.I.LevelNo > -1)
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
                        G.I.Zoom += 1;
                        Resize_GameField();
                    }
                    break;
                case Keys.OemMinus:
                    if (e.Control)
                    {
                        if (G.I.Zoom > 10)
                            G.I.Zoom -= 1;
                        Resize_GameField();
                    }
                    break;
                case Keys.Add:
                    G.I.Zoom += 1;
                    Resize_GameField();
                    break;
                case Keys.Subtract:
                    if (G.I.Zoom > 10)
                        G.I.Zoom -= 1;
                    Resize_GameField();
                    break;
            }

            if (dir.X != 0 || dir.Y != 0)
            {
                WhatHappend whatsap = logic.MovePlayer(dir);

                view.UpdateCells();
                Invalidate();

                switch(whatsap)
                {
                    case WhatHappend.Win:
                        if (G.I.LevelNo == -1)
                            Show_SelectLevelForm();
                        else
                            Show_LevelDone();
                        break;

                    case WhatHappend.Nothing:
                        if (logic.PlayerHx > 38)
                            Close();
                        break;
                }
            }
        }

        private void Show_LevelDone()
        {
            if (G.I.LevelNo > -1)
                MessageBox.Show("Level done!");

            G.I.LevelNo = -1;
            Show_SplashLevel();
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            if (view.Canvas != null)
                e.Graphics.DrawImageUnscaled(view.Canvas, 0, 0);
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
    }
}
