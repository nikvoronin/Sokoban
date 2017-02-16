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
            System.Windows.Forms.Label helpLabel;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label3;
            System.Windows.Forms.Label label4;
            System.Windows.Forms.Label label5;
            System.Windows.Forms.Label label6;
            System.Windows.Forms.Label label7;
            System.Windows.Forms.Label label8;
            System.Windows.Forms.Label label9;
            System.Windows.Forms.Label label10;
            System.Windows.Forms.Label label11;
            System.Windows.Forms.Label label12;
            System.Windows.Forms.Label label13;
            System.Windows.Forms.Label label14;
            System.Windows.Forms.Label label15;
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
            helpLabel = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
            label9 = new System.Windows.Forms.Label();
            label10 = new System.Windows.Forms.Label();
            label11 = new System.Windows.Forms.Label();
            label12 = new System.Windows.Forms.Label();
            label13 = new System.Windows.Forms.Label();
            label14 = new System.Windows.Forms.Label();
            label15 = new System.Windows.Forms.Label();
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
            toolStripStatusLabel2.Size = new System.Drawing.Size(86, 25);
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
            helpLabel.AutoSize = true;
            helpLabel.BackColor = System.Drawing.SystemColors.Control;
            helpLabel.Location = new System.Drawing.Point(49, 53);
            helpLabel.Name = "helpLabel";
            helpLabel.Size = new System.Drawing.Size(34, 13);
            helpLabel.TabIndex = 8;
            helpLabel.Text = "Move";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = System.Drawing.SystemColors.Control;
            label2.Location = new System.Drawing.Point(49, 79);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(66, 13);
            label2.TabIndex = 8;
            label2.Text = "Select Level";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = System.Drawing.SystemColors.Control;
            label3.Location = new System.Drawing.Point(49, 105);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(81, 13);
            label3.TabIndex = 8;
            label3.Text = "Resize Window";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = System.Drawing.SystemColors.Control;
            label4.Location = new System.Drawing.Point(49, 66);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(86, 13);
            label4.TabIndex = 8;
            label4.Text = "Undo Last Move";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = System.Drawing.SystemColors.Control;
            label5.Location = new System.Drawing.Point(49, 92);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(70, 13);
            label5.TabIndex = 8;
            label5.Text = "Restart Level";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = System.Drawing.SystemColors.Control;
            label6.Location = new System.Drawing.Point(141, 92);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(19, 13);
            label6.TabIndex = 8;
            label6.Text = "F5";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.BackColor = System.Drawing.SystemColors.Control;
            label7.Location = new System.Drawing.Point(141, 66);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(70, 13);
            label7.TabIndex = 8;
            label7.Text = "BACKSPACE";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.BackColor = System.Drawing.SystemColors.Control;
            label8.Location = new System.Drawing.Point(141, 105);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(81, 13);
            label8.TabIndex = 8;
            label8.Text = "CTRL+   CTRL-";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.BackColor = System.Drawing.SystemColors.Control;
            label9.Location = new System.Drawing.Point(141, 79);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(49, 13);
            label9.TabIndex = 8;
            label9.Text = "ESCAPE";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.BackColor = System.Drawing.SystemColors.Control;
            label10.Location = new System.Drawing.Point(141, 53);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(95, 13);
            label10.TabIndex = 8;
            label10.Text = "CURSOR   WASD";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.BackColor = System.Drawing.SystemColors.Control;
            label11.Location = new System.Drawing.Point(239, 53);
            label11.Name = "label11";
            label11.Size = new System.Drawing.Size(46, 13);
            label11.TabIndex = 8;
            label11.Text = ".. D-Pad";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.BackColor = System.Drawing.SystemColors.Control;
            label12.Location = new System.Drawing.Point(239, 66);
            label12.Name = "label12";
            label12.Size = new System.Drawing.Size(23, 13);
            label12.TabIndex = 8;
            label12.Text = ".. B";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.BackColor = System.Drawing.SystemColors.Control;
            label13.Location = new System.Drawing.Point(239, 79);
            label13.Name = "label13";
            label13.Size = new System.Drawing.Size(52, 13);
            label13.TabIndex = 8;
            label13.Text = ".. START";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.BackColor = System.Drawing.SystemColors.Control;
            label14.Location = new System.Drawing.Point(239, 92);
            label14.Name = "label14";
            label14.Size = new System.Drawing.Size(44, 13);
            label14.TabIndex = 8;
            label14.Text = ".. BACK";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.BackColor = System.Drawing.SystemColors.Control;
            label15.Location = new System.Drawing.Point(239, 105);
            label15.Name = "label15";
            label15.Size = new System.Drawing.Size(53, 13);
            label15.TabIndex = 8;
            label15.Text = ".. RB   LB";
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
            this.mainStatusStrip.Location = new System.Drawing.Point(0, 141);
            this.mainStatusStrip.Name = "mainStatusStrip";
            this.mainStatusStrip.Size = new System.Drawing.Size(408, 30);
            this.mainStatusStrip.TabIndex = 7;
            // 
            // stepsToolStripStatusLabel
            // 
            this.stepsToolStripStatusLabel.Name = "stepsToolStripStatusLabel";
            this.stepsToolStripStatusLabel.Size = new System.Drawing.Size(28, 25);
            this.stepsToolStripStatusLabel.Text = "0:0";
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
            this.ClientSize = new System.Drawing.Size(408, 171);
            this.Controls.Add(label11);
            this.Controls.Add(label10);
            this.Controls.Add(label13);
            this.Controls.Add(label9);
            this.Controls.Add(label15);
            this.Controls.Add(label8);
            this.Controls.Add(label12);
            this.Controls.Add(label7);
            this.Controls.Add(label14);
            this.Controls.Add(label6);
            this.Controls.Add(label5);
            this.Controls.Add(label4);
            this.Controls.Add(label3);
            this.Controls.Add(label2);
            this.Controls.Add(helpLabel);
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
        private System.Windows.Forms.Button quitButton;
        private System.Windows.Forms.ToolStripStatusLabel timeToolStripStatusLabel;
        private System.Windows.Forms.Timer clockTimer;
    }
}