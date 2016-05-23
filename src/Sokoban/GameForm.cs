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
        Warehouse warehouse;
        bool avoidMainMenu = false;

        public GameForm(bool goSelectLevel = false)
        {
            avoidMainMenu = goSelectLevel;

            SetStyle(
                ControlStyles.OptimizedDoubleBuffer |
                    ControlStyles.AllPaintingInWmPaint,
                true);

            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (avoidMainMenu)
                ShowSelectLevelMenu();
            else
                ShowMainMenu();
        }

        private void ShowMainMenu()
        {
            warehouse = new Warehouse(G.I.StartMenu);
            view = new View(warehouse);

            UpdateGameField();
        }

        private void UpdateGameField()
        {
            view.Resize(G.I.Zoom);
            view.Update();

            Width = view.Width;
            Height = view.Height;

            Invalidate();
        }

        private void ShowSelectLevelMenu()
        {
            MenuForm menuForm = new MenuForm(warehouse);
            DialogResult result = menuForm.ShowDialog();
            switch (result)
            {
                case DialogResult.No:       // continue current level
                    UpdateGameField();
                    break;

                case DialogResult.Cancel:   // close app
                    Close();
                    break;

                case DialogResult.OK:       // start thinking over new level
                    warehouse = new Warehouse(G.I.Levels[G.I.CurrentLevelNo]);
                    view = new View(warehouse);

                    UpdateGameField();
                    break;
            }
        }

        private void DoKeys(KeyEventArgs e)
        {
            Point dir = Point.Empty;

            switch (e.KeyCode)
            {
                case Keys.Escape:
                    if (G.I.CurrentLevelNo > -1)
                        ShowSelectLevelMenu();
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
                        UpdateGameField();
                    }
                    break;
                case Keys.OemMinus:
                    if (e.Control)
                    {
                        if (G.I.Zoom > 10)
                            G.I.Zoom -= 1;
                        UpdateGameField();
                    }
                    break;
                case Keys.Add:
                    G.I.Zoom += 1;
                    UpdateGameField();
                    break;
                case Keys.Subtract:
                    if (G.I.Zoom > 10)
                        G.I.Zoom -= 1;
                    UpdateGameField();
                    break;
            }

            if (dir.X != 0 || dir.Y != 0)
            {
                Warehouse.WhatHappend whatsap = warehouse.MovePlayer(dir);

                view.Update();
                Invalidate();

                switch(whatsap)
                {
                    case Warehouse.WhatHappend.Win:
                        if (G.I.CurrentLevelNo == -1)
                            ShowSelectLevelMenu();
                        else
                            LevelDone();
                        break;

                    case Warehouse.WhatHappend.Nothing:
                        if (warehouse.PlayerX > 38)
                            Close();
                        break;
                }
            }
        }

        private void LevelDone()
        {
            if (G.I.CurrentLevelNo > -1)
                MessageBox.Show("Level done!");

            G.I.CurrentLevelNo = -1;
            ShowMainMenu();
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            if (view.Canvas != null)
            {
                e.Graphics.DrawImageUnscaled(view.Canvas, 0, 0);
            }
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
            DoKeys(e);
        }
    }
}
