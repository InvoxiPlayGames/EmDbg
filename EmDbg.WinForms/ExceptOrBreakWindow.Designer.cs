namespace EmDbg.WinForms
{
    partial class ExceptOrBreakWindow
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
            this.typeLabel = new System.Windows.Forms.Label();
            this.primaryAddrText = new System.Windows.Forms.TextBox();
            this.codeLabel = new System.Windows.Forms.Label();
            this.dataLabel = new System.Windows.Forms.Label();
            this.continueButton = new System.Windows.Forms.Button();
            this.skipOverButton = new System.Windows.Forms.Button();
            this.editContextButton = new System.Windows.Forms.Button();
            this.viewDisassemblyButton = new System.Windows.Forms.Button();
            this.viewMemoryButton = new System.Windows.Forms.Button();
            this.exceptCodeText = new System.Windows.Forms.TextBox();
            this.exceptDataText = new System.Windows.Forms.TextBox();
            this.threadLabel = new System.Windows.Forms.Label();
            this.threadModuleText = new System.Windows.Forms.TextBox();
            this.restartButton = new System.Windows.Forms.Button();
            this.moduleLabel = new System.Windows.Forms.Label();
            this.moduleText = new System.Windows.Forms.TextBox();
            this.primarySymbolText = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // typeLabel
            // 
            this.typeLabel.AutoSize = true;
            this.typeLabel.Location = new System.Drawing.Point(12, 9);
            this.typeLabel.Name = "typeLabel";
            this.typeLabel.Size = new System.Drawing.Size(184, 15);
            this.typeLabel.TabIndex = 0;
            this.typeLabel.Text = "An exception has been thrown at:";
            // 
            // primaryAddrText
            // 
            this.primaryAddrText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.primaryAddrText.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.primaryAddrText.Location = new System.Drawing.Point(15, 26);
            this.primaryAddrText.Name = "primaryAddrText";
            this.primaryAddrText.ReadOnly = true;
            this.primaryAddrText.Size = new System.Drawing.Size(96, 19);
            this.primaryAddrText.TabIndex = 9;
            this.primaryAddrText.Text = "0x00000000";
            // 
            // codeLabel
            // 
            this.codeLabel.AutoSize = true;
            this.codeLabel.Location = new System.Drawing.Point(12, 49);
            this.codeLabel.Name = "codeLabel";
            this.codeLabel.Size = new System.Drawing.Size(93, 15);
            this.codeLabel.TabIndex = 11;
            this.codeLabel.Text = "Exception Code:";
            // 
            // dataLabel
            // 
            this.dataLabel.AutoSize = true;
            this.dataLabel.Location = new System.Drawing.Point(12, 69);
            this.dataLabel.Name = "dataLabel";
            this.dataLabel.Size = new System.Drawing.Size(89, 15);
            this.dataLabel.TabIndex = 12;
            this.dataLabel.Text = "Exception Data:";
            // 
            // continueButton
            // 
            this.continueButton.Location = new System.Drawing.Point(303, 171);
            this.continueButton.Name = "continueButton";
            this.continueButton.Size = new System.Drawing.Size(75, 23);
            this.continueButton.TabIndex = 13;
            this.continueButton.Text = "Continue";
            this.continueButton.UseVisualStyleBackColor = true;
            this.continueButton.Click += new System.EventHandler(this.continueButton_Click);
            // 
            // skipOverButton
            // 
            this.skipOverButton.Location = new System.Drawing.Point(303, 143);
            this.skipOverButton.Name = "skipOverButton";
            this.skipOverButton.Size = new System.Drawing.Size(75, 23);
            this.skipOverButton.TabIndex = 14;
            this.skipOverButton.Text = "Skip Over";
            this.skipOverButton.UseVisualStyleBackColor = true;
            this.skipOverButton.Click += new System.EventHandler(this.skipOverButton_Click);
            // 
            // editContextButton
            // 
            this.editContextButton.Enabled = false;
            this.editContextButton.Location = new System.Drawing.Point(40, 143);
            this.editContextButton.Name = "editContextButton";
            this.editContextButton.Size = new System.Drawing.Size(124, 23);
            this.editContextButton.TabIndex = 15;
            this.editContextButton.Text = "Edit Thread Context";
            this.editContextButton.UseVisualStyleBackColor = true;
            this.editContextButton.Visible = false;
            // 
            // viewDisassemblyButton
            // 
            this.viewDisassemblyButton.Location = new System.Drawing.Point(170, 171);
            this.viewDisassemblyButton.Name = "viewDisassemblyButton";
            this.viewDisassemblyButton.Size = new System.Drawing.Size(127, 23);
            this.viewDisassemblyButton.TabIndex = 16;
            this.viewDisassemblyButton.Text = "View in Disassembler";
            this.viewDisassemblyButton.UseVisualStyleBackColor = true;
            this.viewDisassemblyButton.Click += new System.EventHandler(this.viewDisassemblyButton_Click);
            // 
            // viewMemoryButton
            // 
            this.viewMemoryButton.Enabled = false;
            this.viewMemoryButton.Location = new System.Drawing.Point(170, 143);
            this.viewMemoryButton.Name = "viewMemoryButton";
            this.viewMemoryButton.Size = new System.Drawing.Size(127, 23);
            this.viewMemoryButton.TabIndex = 17;
            this.viewMemoryButton.Text = "View in Memory";
            this.viewMemoryButton.UseVisualStyleBackColor = true;
            this.viewMemoryButton.Visible = false;
            // 
            // exceptCodeText
            // 
            this.exceptCodeText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.exceptCodeText.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.exceptCodeText.Location = new System.Drawing.Point(111, 49);
            this.exceptCodeText.Name = "exceptCodeText";
            this.exceptCodeText.ReadOnly = true;
            this.exceptCodeText.Size = new System.Drawing.Size(186, 16);
            this.exceptCodeText.TabIndex = 18;
            this.exceptCodeText.Text = "EXCEPTION_CODE_HERE";
            // 
            // exceptDataText
            // 
            this.exceptDataText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.exceptDataText.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.exceptDataText.Location = new System.Drawing.Point(111, 69);
            this.exceptDataText.Name = "exceptDataText";
            this.exceptDataText.ReadOnly = true;
            this.exceptDataText.Size = new System.Drawing.Size(267, 16);
            this.exceptDataText.TabIndex = 19;
            this.exceptDataText.Text = "failed write to 0x00000000";
            // 
            // threadLabel
            // 
            this.threadLabel.AutoSize = true;
            this.threadLabel.Location = new System.Drawing.Point(12, 89);
            this.threadLabel.Name = "threadLabel";
            this.threadLabel.Size = new System.Drawing.Size(92, 15);
            this.threadLabel.TabIndex = 20;
            this.threadLabel.Text = "Thread/Module:";
            // 
            // threadModuleText
            // 
            this.threadModuleText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.threadModuleText.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.threadModuleText.Location = new System.Drawing.Point(111, 89);
            this.threadModuleText.Name = "threadModuleText";
            this.threadModuleText.ReadOnly = true;
            this.threadModuleText.Size = new System.Drawing.Size(267, 16);
            this.threadModuleText.TabIndex = 21;
            this.threadModuleText.Text = "0x00000000 / module name";
            // 
            // restartButton
            // 
            this.restartButton.Location = new System.Drawing.Point(40, 171);
            this.restartButton.Name = "restartButton";
            this.restartButton.Size = new System.Drawing.Size(124, 23);
            this.restartButton.TabIndex = 22;
            this.restartButton.Text = "Exit Title";
            this.restartButton.UseVisualStyleBackColor = true;
            this.restartButton.Click += new System.EventHandler(this.restartButton_Click);
            // 
            // moduleLabel
            // 
            this.moduleLabel.AutoSize = true;
            this.moduleLabel.Location = new System.Drawing.Point(12, 110);
            this.moduleLabel.Name = "moduleLabel";
            this.moduleLabel.Size = new System.Drawing.Size(82, 15);
            this.moduleLabel.TabIndex = 23;
            this.moduleLabel.Text = "Code Module:";
            // 
            // moduleText
            // 
            this.moduleText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.moduleText.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.moduleText.Location = new System.Drawing.Point(111, 110);
            this.moduleText.Name = "moduleText";
            this.moduleText.ReadOnly = true;
            this.moduleText.Size = new System.Drawing.Size(267, 16);
            this.moduleText.TabIndex = 24;
            this.moduleText.Text = "module name";
            // 
            // primarySymbolText
            // 
            this.primarySymbolText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.primarySymbolText.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.primarySymbolText.Location = new System.Drawing.Point(111, 28);
            this.primarySymbolText.Name = "primarySymbolText";
            this.primarySymbolText.ReadOnly = true;
            this.primarySymbolText.Size = new System.Drawing.Size(267, 16);
            this.primarySymbolText.TabIndex = 25;
            this.primarySymbolText.Text = "Symbol_Name_Here + 0x0";
            this.primarySymbolText.Visible = false;
            // 
            // ExceptOrBreakWindow
            // 
            this.AcceptButton = this.continueButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.restartButton;
            this.ClientSize = new System.Drawing.Size(394, 209);
            this.Controls.Add(this.primarySymbolText);
            this.Controls.Add(this.moduleText);
            this.Controls.Add(this.moduleLabel);
            this.Controls.Add(this.restartButton);
            this.Controls.Add(this.threadModuleText);
            this.Controls.Add(this.threadLabel);
            this.Controls.Add(this.exceptDataText);
            this.Controls.Add(this.exceptCodeText);
            this.Controls.Add(this.viewMemoryButton);
            this.Controls.Add(this.viewDisassemblyButton);
            this.Controls.Add(this.editContextButton);
            this.Controls.Add(this.skipOverButton);
            this.Controls.Add(this.continueButton);
            this.Controls.Add(this.dataLabel);
            this.Controls.Add(this.codeLabel);
            this.Controls.Add(this.primaryAddrText);
            this.Controls.Add(this.typeLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExceptOrBreakWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Exception Thrown";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label typeLabel;
        private TextBox primaryAddrText;
        private Label codeLabel;
        private Label dataLabel;
        private Button continueButton;
        private Button skipOverButton;
        private Button editContextButton;
        private Button viewDisassemblyButton;
        private Button viewMemoryButton;
        private TextBox exceptCodeText;
        private TextBox exceptDataText;
        private Label threadLabel;
        private TextBox threadModuleText;
        private Button restartButton;
        private Label moduleLabel;
        private TextBox moduleText;
        private TextBox primarySymbolText;
    }
}