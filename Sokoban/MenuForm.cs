using Sokoban.Core;

namespace Sokoban;

public partial class MenuForm : Form
{
    public MenuForm()
    {
        InitializeComponent();
        UpdateElapsedTime();
    }

    private void ContinueButton_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.No;
        Close();
    }

    private void GoButton_Click(object sender, EventArgs e)
    {
        Tag = selectLevelComboBox.SelectedValue;
        DialogResult = DialogResult.OK;
        Close();
    }

    private void QuitButton_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }

    private void MenuForm_Load(object sender, EventArgs e)
    {
        if (GameContext.I.Logic != null)
        {
            Text = string.IsNullOrEmpty(GameContext.I.Logic.Map?.Name.Trim()) ?
                    GameContext.APP_NAME :
                    GameContext.I.Logic.Map.Name + " — " + GameContext.APP_NAME;

            stepsToolStripStatusLabel.Text = $"{GameContext.I.Logic.Steps}:{GameContext.I.Logic.Movements}";
            doneToolStripStatusLabel.Text = $"{GameContext.I.Logic.InPlace} ({GameContext.I.Logic.Map?.Plates})";
        }

        packComboBox.DataSource = GameContext.I.Packs;

        continueButton.Enabled = !GameContext.I.IsSplashLevel;

        Level? currentMap = GameContext.I.Logic?.Map;
        LevelPack? currentPack = currentMap != null
            ? GameContext.I.Packs.FirstOrDefault(p => p.Levels.Contains(currentMap))
            : null;

        if (currentPack != null)
            packComboBox.SelectedItem = currentPack;
        else
        {
            LevelPack? defaultPack = GameContext.I.Packs.FirstOrDefault(p => p.Name == "Rabbit");
            if (defaultPack != null)
                packComboBox.SelectedItem = defaultPack;
            else if (packComboBox.Items.Count > 0)
                packComboBox.SelectedIndex = 0;
        }

        if (currentMap != null)
            selectLevelComboBox.SelectedItem = currentMap;
    }

    private void PackComboBox_SelectedIndexChanged(object? sender, EventArgs e)
    {
        selectLevelComboBox.DataSource = (packComboBox.SelectedItem as LevelPack)?.Levels;
    }

    private void UpdateElapsedTime()
    {
        timeToolStripStatusLabel.Text = GameContext.I.ElapsedTimeLongString ?? "";
    }

    private void ClockTimer_Tick(object sender, EventArgs e)
    {
        UpdateElapsedTime();
    }

    private sealed class ComboboxItem(Level level)
    {
        public string Text { get; set; } = level.Name;
        public Level Value { get; set; } = level;
        public override string ToString() { return Text; }
    }

    private void MenuForm_Shown(object sender, EventArgs e)
    {
        if (GameContext.I.IsSplashLevel)
        { 
            selectLevelComboBox.Focus();
        }
    }

    private void SelectLevelComboBox_KeyUp(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            if (selectLevelComboBox.SelectedValue != null)
            {
                GoButton_Click(sender, EventArgs.Empty);
            }
        }
    }

    public void HandleGamepadKey(Keys k)
    {
        switch (k)
        {
            case Keys.Up:
                AdjustSelection(-1);
                break;

            case Keys.Down:
                AdjustSelection(1);
                break;

            case Keys.Back:
                SelectNextControl(ActiveControl, true, true, true, true);
                break;

            case Keys.Enter:
                if (selectLevelComboBox.SelectedValue != null)
                    GoButton_Click(this, EventArgs.Empty);
                break;
        }
    }

    private void AdjustSelection(int delta)
    {
        if (ActiveControl is ComboBox combo)
        {
            int next = combo.SelectedIndex + delta;
            if (next >= 0 && next < combo.Items.Count)
                combo.SelectedIndex = next;
        }
    }
}
