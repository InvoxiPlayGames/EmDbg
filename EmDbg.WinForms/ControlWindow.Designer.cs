namespace EmDbg.WinForms
{
    partial class ControlWindow
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
            this.logList = new System.Windows.Forms.TextBox();
            this.disconnectButton = new System.Windows.Forms.Button();
            this.memoryViewCheck = new System.Windows.Forms.CheckBox();
            this.disasmCheck = new System.Windows.Forms.CheckBox();
            this.threadlistCheck = new System.Windows.Forms.CheckBox();
            this.modulelistCheck = new System.Windows.Forms.CheckBox();
            this.bpCheck = new System.Windows.Forms.CheckBox();
            this.settingsButton = new System.Windows.Forms.Button();
            this.stateChangeButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // logList
            // 
            this.logList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.logList.BackColor = System.Drawing.SystemColors.ControlText;
            this.logList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.logList.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.logList.Location = new System.Drawing.Point(12, 41);
            this.logList.Multiline = true;
            this.logList.Name = "logList";
            this.logList.ReadOnly = true;
            this.logList.Size = new System.Drawing.Size(570, 304);
            this.logList.TabIndex = 0;
            // 
            // disconnectButton
            // 
            this.disconnectButton.Location = new System.Drawing.Point(12, 12);
            this.disconnectButton.Name = "disconnectButton";
            this.disconnectButton.Size = new System.Drawing.Size(75, 23);
            this.disconnectButton.TabIndex = 5;
            this.disconnectButton.Text = "Disconnect";
            this.disconnectButton.UseVisualStyleBackColor = true;
            this.disconnectButton.Click += new System.EventHandler(this.disconnectButton_Click);
            // 
            // memoryViewCheck
            // 
            this.memoryViewCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.memoryViewCheck.AutoSize = true;
            this.memoryViewCheck.Enabled = false;
            this.memoryViewCheck.Location = new System.Drawing.Point(101, 15);
            this.memoryViewCheck.Name = "memoryViewCheck";
            this.memoryViewCheck.Size = new System.Drawing.Size(109, 19);
            this.memoryViewCheck.TabIndex = 6;
            this.memoryViewCheck.Text = "Memory Viewer";
            this.memoryViewCheck.UseVisualStyleBackColor = true;
            // 
            // disasmCheck
            // 
            this.disasmCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.disasmCheck.AutoSize = true;
            this.disasmCheck.Location = new System.Drawing.Point(216, 15);
            this.disasmCheck.Name = "disasmCheck";
            this.disasmCheck.Size = new System.Drawing.Size(91, 19);
            this.disasmCheck.TabIndex = 7;
            this.disasmCheck.Text = "Disassembly";
            this.disasmCheck.UseVisualStyleBackColor = true;
            this.disasmCheck.CheckedChanged += new System.EventHandler(this.disasmCheck_CheckedChanged);
            // 
            // threadlistCheck
            // 
            this.threadlistCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.threadlistCheck.AutoSize = true;
            this.threadlistCheck.Enabled = false;
            this.threadlistCheck.Location = new System.Drawing.Point(313, 15);
            this.threadlistCheck.Name = "threadlistCheck";
            this.threadlistCheck.Size = new System.Drawing.Size(83, 19);
            this.threadlistCheck.TabIndex = 8;
            this.threadlistCheck.Text = "Thread List";
            this.threadlistCheck.UseVisualStyleBackColor = true;
            // 
            // modulelistCheck
            // 
            this.modulelistCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.modulelistCheck.AutoSize = true;
            this.modulelistCheck.Enabled = false;
            this.modulelistCheck.Location = new System.Drawing.Point(402, 15);
            this.modulelistCheck.Name = "modulelistCheck";
            this.modulelistCheck.Size = new System.Drawing.Size(88, 19);
            this.modulelistCheck.TabIndex = 9;
            this.modulelistCheck.Text = "Module List";
            this.modulelistCheck.UseVisualStyleBackColor = true;
            // 
            // bpCheck
            // 
            this.bpCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bpCheck.AutoSize = true;
            this.bpCheck.Enabled = false;
            this.bpCheck.Location = new System.Drawing.Point(496, 15);
            this.bpCheck.Name = "bpCheck";
            this.bpCheck.Size = new System.Drawing.Size(88, 19);
            this.bpCheck.TabIndex = 10;
            this.bpCheck.Text = "Breakpoints";
            this.bpCheck.UseVisualStyleBackColor = true;
            // 
            // settingsButton
            // 
            this.settingsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.settingsButton.Enabled = false;
            this.settingsButton.Location = new System.Drawing.Point(507, 351);
            this.settingsButton.Name = "settingsButton";
            this.settingsButton.Size = new System.Drawing.Size(75, 23);
            this.settingsButton.TabIndex = 11;
            this.settingsButton.Text = "Settings";
            this.settingsButton.UseVisualStyleBackColor = true;
            // 
            // stateChangeButton
            // 
            this.stateChangeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.stateChangeButton.Location = new System.Drawing.Point(12, 351);
            this.stateChangeButton.Name = "stateChangeButton";
            this.stateChangeButton.Size = new System.Drawing.Size(75, 23);
            this.stateChangeButton.TabIndex = 12;
            this.stateChangeButton.Text = "Stop Game";
            this.stateChangeButton.UseVisualStyleBackColor = true;
            this.stateChangeButton.Click += new System.EventHandler(this.stateChangeButton_Click);
            // 
            // ControlWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 386);
            this.Controls.Add(this.stateChangeButton);
            this.Controls.Add(this.settingsButton);
            this.Controls.Add(this.bpCheck);
            this.Controls.Add(this.modulelistCheck);
            this.Controls.Add(this.threadlistCheck);
            this.Controls.Add(this.disasmCheck);
            this.Controls.Add(this.memoryViewCheck);
            this.Controls.Add(this.disconnectButton);
            this.Controls.Add(this.logList);
            this.MinimumSize = new System.Drawing.Size(610, 425);
            this.Name = "ControlWindow";
            this.Text = "EmDbg";
            this.Load += new System.EventHandler(this.ControlWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox logList;
        private Button disconnectButton;
        private CheckBox memoryViewCheck;
        private CheckBox disasmCheck;
        private CheckBox threadlistCheck;
        private CheckBox modulelistCheck;
        private CheckBox bpCheck;
        private Button settingsButton;
        private Button stateChangeButton;
    }
}