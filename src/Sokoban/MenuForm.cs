using Sokoban.Core;
using System;
using System.Windows.Forms;

namespace Sokoban
{
    public partial class MenuForm : Form
    {
        public MenuForm()
        {
            InitializeComponent();
            UpdateElapsedTime();
        }

        private void continueButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
            Close();
        }

        private void goButton_Click(object sender, EventArgs e)
        {
            Tag = selectLevelComboBox.SelectedValue;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void quitButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void MenuForm_Load(object sender, EventArgs e)
        {
            if (G.I.Logic != null)
                Text = string.IsNullOrEmpty(G.I.Logic.Map?.Name.Trim()) ?
                        G.APP_NAME :
                        G.I.Logic.Map.Name + " — " + G.APP_NAME;

            if (G.I.Logic != null)
            {
                stepsToolStripStatusLabel.Text =
                    string.Format( "{0}/{1}",
                    G.I.Logic.Steps,
                    G.I.Logic.Movements);
                doneToolStripStatusLabel.Text =
                    string.Format( "{0} ({1})",
                        G.I.Logic.InPlace,
                        G.I.Logic.Map.Plates);
            }

            helpLabel.Text = @"CURSOR, WASD to move. D-Pad
ESCAPE to select another level. START
CTRL+, CTRL- resizes game board. RB, LB
BACKSPACE to undo last movement. B
F5 restarts level. BACK";

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

        private void clockTimer_Tick(object sender, EventArgs e)
        {
            UpdateElapsedTime();
        }

        private class ComboboxItem
        {
            public ComboboxItem(Level level)
            {
                Text = level.Name;
                Value = level;
            }
            public string Text { get; set; }
            public Level Value { get; set; }
            public override string ToString() { return Text; }
        }

        private void MenuForm_Shown(object sender, EventArgs e)
        {
            if (G.I.IsSplashLevel)
                selectLevelComboBox.Focus();
        }

        private void selectLevelComboBox_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
                if (selectLevelComboBox.SelectedValue != null)
                    goButton_Click(sender, EventArgs.Empty);
        }
    }
}
