using Sokoban.Core;
using System.Drawing;
using System.Runtime.InteropServices;
using XInput.Wrapper;

namespace Sokoban;

public partial class GameForm : Form
{
    public const int WM_NCLBUTTONDOWN = 0xA1;
    public const int HT_CAPTION = 0x2;
    const long KEYDOWN_TICKS_BEFORE_REPEAT = 4000000;

    [DllImport("user32.dll")]
    public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
    [DllImport("user32.dll")]
    public static extern bool ReleaseCapture();

    int cellSizePx = 25;
    bool avoidSplashLevel = false;
    bool isShowSelectLevel = false;
    MenuForm? currentMenuForm = null;

    public GameForm(bool showSelectLevelMenu = false)
    {
        avoidSplashLevel = showSelectLevelMenu;

        SetStyle(
            ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.AllPaintingInWmPaint,
            true);

        InitializeComponent();
    }

    private void GameForm_Load(object sender, EventArgs e)
    {
        if (GameContext.I.Gamepad != null)
        {
            GameContext.I.Gamepad.KeyDown += Gamepad_KeyDown;
            GameContext.I.Gamepad.StateChanged += Gamepad_StateChanged;
            X.StartPolling(GameContext.I.Gamepad);
        }

        if (!avoidSplashLevel)
            Show_SplashLevel();
        else
        {
            GameContext.I.Start();
            Show_SelectLevelForm();
        }
    }

    long startDelta = 0;
    private void Gamepad_StateChanged(object? sender, EventArgs e)
    {
        Keys k = GamepadToKeys();
        if (k != Keys.None)
        {
            startDelta = DateTime.UtcNow.Ticks;
            if (isShowSelectLevel)
                currentMenuForm?.HandleGamepadKey(k);
            else
                Do_Keys(new KeyEventArgs(k));
        }
    }

    private void Gamepad_KeyDown(object? sender, EventArgs e)
    {
        if (isShowSelectLevel)
            return;

        Keys k = GamepadToKeys();
        if ((k != Keys.None) && (DateTime.UtcNow.Ticks - startDelta > KEYDOWN_TICKS_BEFORE_REPEAT))
        {
            Do_Keys(new KeyEventArgs(k));
            startDelta = 0;
        }
    }

    private Keys GamepadToKeys()
    {
        Keys keys = Keys.None;
        X.Gamepad gpad = GameContext.I.Gamepad!; // only called while Gamepad != null (see GameForm_Load)

        if (gpad.Dpad_Down_down)
            keys = Keys.Down;

        if (gpad.Dpad_Up_down)
            keys = Keys.Up;

        if (gpad.Dpad_Left_down)
            keys = Keys.Left;

        if (gpad.Dpad_Right_down)
            keys = Keys.Right;

        if (gpad.Back_up)
            keys = Keys.F5;

        if (gpad.B_down)
            keys = Keys.Back;

        if (gpad.A_up)
            keys = Keys.Enter;

        if (gpad.Start_up)
            keys = Keys.Escape;

        if (gpad.LBumper_down)
            keys = Keys.Subtract;

        if (gpad.RBumper_down)
            keys = Keys.Add;

        return keys;
    }

    private void Show_SplashLevel()
    {
        closeLabel.Visible = true;

        GameContext.I.Start();
        Update_GameField();
    }

    private void Update_GameField()
    {
        Text = string.IsNullOrEmpty(GameContext.I.Logic.Map.Name.Trim()) ?
                    GameContext.APP_NAME :
                    $"{GameContext.I.Logic.Map.Name} — {GameContext.APP_NAME}";

        GameContext.I.View.Resize(cellSizePx);
        GameContext.I.View.DrawField();

        Size =
            new Size(
                GameContext.I.View.Width,
                GameContext.I.View.Height);

        Invalidate();
    }

    private void Show_SelectLevelForm()
    {
        isShowSelectLevel = true;
        MenuForm menuForm = new();
        currentMenuForm = menuForm;
        DialogResult result = menuForm.ShowDialog(this);
        currentMenuForm = null;
        isShowSelectLevel = false;

        switch (result)
        {
            //case DialogResult.No:     // continue current level
            //    break;

            case DialogResult.Cancel:   // close app
                Close();
                break;

            case DialogResult.OK:       // start thinking over new level
                Level? level = menuForm.Tag as Level;
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
        GameContext.I.Start(level);
        Update_GameField();
    }

    private void Do_Keys(KeyEventArgs e)
    {
        Point dir = Point.Empty;

        switch (e.KeyCode)
        {
            case Keys.Escape:
                if (!GameContext.I.IsSplashLevel)
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
                    HandleZoom(1);
                break;
            case Keys.OemMinus:
                if (e.Control)
                    HandleZoom(-1);
                break;
            case Keys.Add:
                HandleZoom(1);
                break;
            case Keys.Subtract:
                HandleZoom(-1);
                break;
            case Keys.Back:
                HandleUndo();
                break;
            case Keys.F5:
                RestartLevel(GameContext.I.Logic.Map);
                break;
        } // switch (e.KeyCode)

        if (dir.X != 0 || dir.Y != 0)
            HandleMovement(dir);
    } // Do_Keys()

    private void HandleZoom(int delta)
    {
        if (delta > 0)
            cellSizePx++;
        else if (cellSizePx > 10)
            cellSizePx--;
        else
            return;

        Update_GameField();
    }

    private void HandleUndo()
    {
        GameContext.I.Logic.Undo();
        GameContext.I.View.Update();
        Invalidate();
    }

    private void HandleMovement(Point dir)
    {
        WhatsUp whatsup = GameContext.I.Logic.MovePlayer(dir);

        GameContext.I.View.Update();
        Invalidate();

        switch (whatsup)
        {
            case WhatsUp.Win:
                if (GameContext.I.IsSplashLevel)
                    Show_SelectLevelForm();
                else
                    Show_LevelDone();
                break;

            case WhatsUp.Nothing:
                if (GameContext.I.Logic.PlayerX > 38)
                    Close();
                break;
        } // switch(whatsup)
    } // HandleMovement()

    private void Show_LevelDone()
    {
        MessageBox.Show(
            $"Amazing! You win!\nIn {GameContext.I.Logic.Steps} steps\nAnd {GameContext.I.Logic.Movements} movements of boxes\nBy the time: {GameContext.I.ElapsedTimeLongString}", 
            "Level Done!");

        closeLabel.Visible = false;

        GameContext.I.StartNextLevel();
        Update_GameField();
    }

    private void GameForm_Paint(object sender, PaintEventArgs e)
    {
        if (GameContext.I.View?.Canvas != null)
            e.Graphics.DrawImageUnscaled(GameContext.I.View.Canvas, 0, 0);
    }

    private void GameForm_MouseDown(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
            DragMoveWindow();
    }

    private void DragMoveWindow()
    {
        if (ReleaseCapture())
            SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
    }

    private void GameForm_KeyDown(object sender, KeyEventArgs e)
    {
        Do_Keys(e);
    }

    private void closeLabel_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void GameForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        if(GameContext.I.Gamepad != null)
            X.StopPolling();
    }
}
