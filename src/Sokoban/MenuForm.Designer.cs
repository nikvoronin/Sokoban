namespace Sokoban
{
    partial class MenuForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label label1;
            System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
            System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
            System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
            System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
            this.helpLabel = new System.Windows.Forms.Label();
            this.selectLevelComboBox = new System.Windows.Forms.ComboBox();
            this.goButton = new System.Windows.Forms.Button();
            this.continueButton = new System.Windows.Forms.Button();
            this.mainStatusStrip = new System.Windows.Forms.StatusStrip();
            this.stepsToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.doneToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.timeToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.quitButton = new System.Windows.Forms.Button();
            this.clockTimer = new System.Windows.Forms.Timer(this.components);
            label1 = new System.Windows.Forms.Label();
            toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
            this.mainStatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(12, 24);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(33, 13);
            label1.TabIndex = 2;
            label1.Text = "Level";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new System.Drawing.Size(48, 25);
            toolStripStatusLabel1.Text = "Steps:";
            // 
            // toolStripStatusLabel2
            // 
            toolStripStatusLabel2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            toolStripStatusLabel2.Size = new System.Drawing.Size(83, 25);
            toolStripStatusLabel2.Spring = true;
            toolStripStatusLabel2.Text = "Sokoban";
            toolStripStatusLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // toolStripStatusLabel4
            // 
            toolStripStatusLabel4.AutoSize = false;
            toolStripStatusLabel4.Margin = new System.Windows.Forms.Padding(10, 3, 0, 2);
            toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            toolStripStatusLabel4.Size = new System.Drawing.Size(48, 25);
            toolStripStatusLabel4.Text = "Done:";
            // 
            // toolStripStatusLabel5
            // 
            toolStripStatusLabel5.AutoSize = false;
            toolStripStatusLabel5.Margin = new System.Windows.Forms.Padding(10, 3, 0, 2);
            toolStripStatusLabel5.Name = "toolStripStatusLabel5";
            toolStripStatusLabel5.Size = new System.Drawing.Size(48, 25);
            toolStripStatusLabel5.Text = "Time:";
            // 
            // helpLabel
            // 
            this.helpLabel.AutoSize = true;
            this.helpLabel.BackColor = System.Drawing.SystemColors.Control;
            this.helpLabel.Location = new System.Drawing.Point(49, 53);
            this.helpLabel.Name = "helpLabel";
            this.helpLabel.Size = new System.Drawing.Size(128, 13);
            this.helpLabel.TabIndex = 8;
            this.helpLabel.Text = "Description appears here.";
            // 
            // selectLevelComboBox
            // 
            this.selectLevelComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.selectLevelComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.selectLevelComboBox.FormattingEnabled = true;
            this.selectLevelComboBox.Location = new System.Drawing.Point(52, 21);
            this.selectLevelComboBox.Name = "selectLevelComboBox";
            this.selectLevelComboBox.Size = new System.Drawing.Size(261, 21);
            this.selectLevelComboBox.TabIndex = 2;
            this.selectLevelComboBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.selectLevelComboBox_KeyUp);
            // 
            // goButton
            // 
            this.goButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.goButton.Location = new System.Drawing.Point(319, 19);
            this.goButton.Name = "goButton";
            this.goButton.Size = new System.Drawing.Size(75, 23);
            this.goButton.TabIndex = 3;
            this.goButton.Text = "re.Start";
            this.goButton.UseVisualStyleBackColor = true;
            this.goButton.Click += new System.EventHandler(this.goButton_Click);
            // 
            // continueButton
            // 
            this.continueButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.continueButton.Enabled = false;
            this.continueButton.Location = new System.Drawing.Point(319, 48);
            this.continueButton.Name = "continueButton";
            this.continueButton.Size = new System.Drawing.Size(75, 23);
            this.continueButton.TabIndex = 0;
            this.continueButton.Text = "Continue";
            this.continueButton.UseVisualStyleBackColor = true;
            this.continueButton.Click += new System.EventHandler(this.continueButton_Click);
            // 
            // mainStatusStrip
            // 
            this.mainStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            toolStripStatusLabel1,
            this.stepsToolStripStatusLabel,
            toolStripStatusLabel4,
            this.doneToolStripStatusLabel,
            toolStripStatusLabel5,
            this.timeToolStripStatusLabel,
            toolStripStatusLabel2});
            this.mainStatusStrip.Location = new System.Drawing.Point(0, 140);
            this.mainStatusStrip.Name = "mainStatusStrip";
            this.mainStatusStrip.Size = new System.Drawing.Size(408, 30);
            this.mainStatusStrip.TabIndex = 7;
            // 
            // stepsToolStripStatusLabel
            // 
            this.stepsToolStripStatusLabel.Name = "stepsToolStripStatusLabel";
            this.stepsToolStripStatusLabel.Size = new System.Drawing.Size(31, 25);
            this.stepsToolStripStatusLabel.Text = "0/0";
            this.stepsToolStripStatusLabel.ToolTipText = "Player\'s steps / Movements";
            // 
            // doneToolStripStatusLabel
            // 
            this.doneToolStripStatusLabel.Name = "doneToolStripStatusLabel";
            this.doneToolStripStatusLabel.Size = new System.Drawing.Size(39, 25);
            this.doneToolStripStatusLabel.Text = "0 (0)";
            // 
            // timeToolStripStatusLabel
            // 
            this.timeToolStripStatusLabel.Name = "timeToolStripStatusLabel";
            this.timeToolStripStatusLabel.Size = new System.Drawing.Size(76, 25);
            this.timeToolStripStatusLabel.Text = "0d 0:00:00";
            // 
            // quitButton
            // 
            this.quitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.quitButton.Location = new System.Drawing.Point(319, 105);
            this.quitButton.Name = "quitButton";
            this.quitButton.Size = new System.Drawing.Size(75, 23);
            this.quitButton.TabIndex = 1;
            this.quitButton.Text = "Quit";
            this.quitButton.UseVisualStyleBackColor = true;
            this.quitButton.Click += new System.EventHandler(this.quitButton_Click);
            // 
            // clockTimer
            // 
            this.clockTimer.Enabled = true;
            this.clockTimer.Interval = 500;
            this.clockTimer.Tick += new System.EventHandler(this.clockTimer_Tick);
            // 
            // MenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(408, 170);
            this.Controls.Add(this.helpLabel);
            this.Controls.Add(this.mainStatusStrip);
            this.Controls.Add(this.quitButton);
            this.Controls.Add(this.continueButton);
            this.Controls.Add(this.goButton);
            this.Controls.Add(label1);
            this.Controls.Add(this.selectLevelComboBox);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(424, 210);
            this.Name = "MenuForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Sokoban";
            this.Load += new System.EventHandler(this.MenuForm_Load);
            this.Shown += new System.EventHandler(this.MenuForm_Shown);
            this.mainStatusStrip.ResumeLayout(false);
            this.mainStatusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox selectLevelComboBox;
        private System.Windows.Forms.Button goButton;
        private System.Windows.Forms.Button continueButton;
        private System.Windows.Forms.StatusStrip mainStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel stepsToolStripStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel doneToolStripStatusLabel;
        private System.Windows.Forms.Label helpLabel;
        private System.Windows.Forms.Button quitButton;
        private System.Windows.Forms.ToolStripStatusLabel timeToolStripStatusLabel;
        private System.Windows.Forms.Timer clockTimer;
    }
}