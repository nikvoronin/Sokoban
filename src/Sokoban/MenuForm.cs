using System;
using System.Windows.Forms;

namespace Sokoban
{
    public partial class MenuForm : Form
    {
        public MenuForm(Warehouse warehouse)
        {
            InitializeComponent();

            if (null != warehouse)
            {
                stepsToolStripStatusLabel.Text = warehouse.Steps.ToString();
                doneToolStripStatusLabel.Text = string.Format("{0} ({1})", warehouse.InPlace, warehouse.Plates);
            }
        }

        private void continueButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }

        private void goButton_Click(object sender, EventArgs e)
        {
            G.I.CurrentLevelNo = selectLevelComboBox.SelectedIndex;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void MenuForm_Load(object sender, EventArgs e)
        {
            helpLabel.Text = @"Cursor keys or WASD to move.
ESCAPE to select another level.
Ctrl+, Ctrl- zooming.";

            foreach (LevelData level in G.I.Levels)
            {
                selectLevelComboBox.Items.Add(level.Name);
            }

            continueButton.Enabled = G.I.CurrentLevelNo > -1;
            if (G.I.CurrentLevelNo > -1)
            {
                selectLevelComboBox.SelectedIndex = G.I.CurrentLevelNo;
            }
            else
            {
                if (selectLevelComboBox.Items.Count > 0)
                {
                    selectLevelComboBox.SelectedIndex = 0;
                    G.I.CurrentLevelNo = 0;
                }
            }
        }
    }
}
