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
        if (G.I.Logic != null)
        {
            Text = string.IsNullOrEmpty(G.I.Logic.Map?.Name.Trim()) ?
                    G.APP_NAME :
                    G.I.Logic.Map.Name + " — " + G.APP_NAME;

            stepsToolStripStatusLabel.Text = $"{G.I.Logic.Steps}:{G.I.Logic.Movements}";
            doneToolStripStatusLabel.Text = $"{G.I.Logic.InPlace} ({G.I.Logic.Map?.Plates})";
        }

        selectLevelComboBox.DataSource = G.I.Levels;

        continueButton.Enabled = !G.I.IsSplashLevel;

        if (G.I.Logic?.Map != null)
            selectLevelComboBox.SelectedItem = G.I.Logic.Map;
        else
            if (selectLevelComboBox.Items.Count > 0)
                selectLevelComboBox.SelectedIndex = 0;
    }

    private void UpdateElapsedTime()
    {
        timeToolStripStatusLabel.Text = G.I.ElapsedTimeLongString ?? "";
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
        if (G.I.IsSplashLevel)
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
} // class
