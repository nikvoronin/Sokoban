using System;
using System.Windows.Forms;

namespace Sokoban
{
    public partial class MenuForm : Form
    {
        Logic logic;
        public MenuForm(Logic gameLogic)
        {
            logic = gameLogic;
            InitializeComponent();
        }

        private void continueButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
            Close();
        }

        private void goButton_Click(object sender, EventArgs e)
        {
            G.I.LevelNo = selectLevelComboBox.SelectedIndex;
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
            Text = G.I.LevelNo == -1 ?
                    G.APP_NAME :
                    G.I.Level.Name + " — " + G.APP_NAME;

            if (null != logic)
            {
                stepsToolStripStatusLabel.Text = logic.Steps.ToString();
                doneToolStripStatusLabel.Text = string.Format("{0} ({1})", logic.InPlace, logic.Plates);
                TimeSpan span = TimeSpan.FromTicks(DateTime.Now.Ticks - logic.StartTime.Ticks);
                timeToolStripStatusLabel.Text =
                    string.Format("{0}{1}:{2}:{3}",
                        span.Days > 0 ? span.Days.ToString() + " " : "",
                        span.Hours,
                        span.Minutes.ToString("00"),
                        span.Seconds.ToString("00")
                        );
            }

            helpLabel.Text = @"Cursor keys or WASD to move.
ESCAPE to select another level.
Ctrl+, Ctrl- resize window.";

            foreach (Level level in G.I.Levels)
            {
                selectLevelComboBox.Items.Add(level.Name);
            }

            continueButton.Enabled = G.I.LevelNo > -1;
            if (G.I.LevelNo > -1)
            {
                selectLevelComboBox.SelectedIndex = G.I.LevelNo;
            }
            else
            {
                if (selectLevelComboBox.Items.Count > 0)
                {
                    selectLevelComboBox.SelectedIndex = 0;
                    G.I.LevelNo = 0;
                }
            }
        }
    }
}
