namespace EmDbg.WinForms
{
    partial class DisassemblyView
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
            this.addressBox = new System.Windows.Forms.TextBox();
            this.addrLabel = new System.Windows.Forms.Label();
            this.jumpButton = new System.Windows.Forms.Button();
            this.disasmList = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.nopButton = new System.Windows.Forms.Button();
            this.assembleButton = new System.Windows.Forms.Button();
            this.memoryButton = new System.Windows.Forms.Button();
            this.breakpointButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // addressBox
            // 
            this.addressBox.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.addressBox.Location = new System.Drawing.Point(70, 12);
            this.addressBox.Name = "addressBox";
            this.addressBox.Size = new System.Drawing.Size(66, 22);
            this.addressBox.TabIndex = 0;
            this.addressBox.Text = "80000000";
            // 
            // addrLabel
            // 
            this.addrLabel.AutoSize = true;
            this.addrLabel.Location = new System.Drawing.Point(12, 15);
            this.addrLabel.Name = "addrLabel";
            this.addrLabel.Size = new System.Drawing.Size(52, 15);
            this.addrLabel.TabIndex = 1;
            this.addrLabel.Text = "Address:";
            // 
            // jumpButton
            // 
            this.jumpButton.Location = new System.Drawing.Point(142, 11);
            this.jumpButton.Name = "jumpButton";
            this.jumpButton.Size = new System.Drawing.Size(80, 23);
            this.jumpButton.TabIndex = 2;
            this.jumpButton.Text = "Jump";
            this.jumpButton.UseVisualStyleBackColor = true;
            this.jumpButton.Click += new System.EventHandler(this.jumpButton_Click);
            // 
            // disasmList
            // 
            this.disasmList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.disasmList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.disasmList.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.disasmList.Location = new System.Drawing.Point(12, 41);
            this.disasmList.Name = "disasmList";
            this.disasmList.Size = new System.Drawing.Size(334, 398);
            this.disasmList.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.disasmList.TabIndex = 3;
            this.disasmList.UseCompatibleStateImageBehavior = false;
            this.disasmList.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Address";
            this.columnHeader1.Width = 80;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Disassembly";
            this.columnHeader2.Width = 225;
            // 
            // nopButton
            // 
            this.nopButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nopButton.Enabled = false;
            this.nopButton.Location = new System.Drawing.Point(352, 41);
            this.nopButton.Name = "nopButton";
            this.nopButton.Size = new System.Drawing.Size(88, 23);
            this.nopButton.TabIndex = 4;
            this.nopButton.Text = "Insert NOP...";
            this.nopButton.UseVisualStyleBackColor = true;
            // 
            // assembleButton
            // 
            this.assembleButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.assembleButton.Enabled = false;
            this.assembleButton.Location = new System.Drawing.Point(352, 70);
            this.assembleButton.Name = "assembleButton";
            this.assembleButton.Size = new System.Drawing.Size(88, 23);
            this.assembleButton.TabIndex = 5;
            this.assembleButton.Text = "Assemble...";
            this.assembleButton.UseVisualStyleBackColor = true;
            // 
            // memoryButton
            // 
            this.memoryButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.memoryButton.Enabled = false;
            this.memoryButton.Location = new System.Drawing.Point(352, 416);
            this.memoryButton.Name = "memoryButton";
            this.memoryButton.Size = new System.Drawing.Size(88, 23);
            this.memoryButton.TabIndex = 6;
            this.memoryButton.Text = "Memory View";
            this.memoryButton.UseVisualStyleBackColor = true;
            // 
            // breakpointButton
            // 
            this.breakpointButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.breakpointButton.Enabled = false;
            this.breakpointButton.Location = new System.Drawing.Point(352, 12);
            this.breakpointButton.Name = "breakpointButton";
            this.breakpointButton.Size = new System.Drawing.Size(88, 23);
            this.breakpointButton.TabIndex = 7;
            this.breakpointButton.Text = "Breakpoint";
            this.breakpointButton.UseVisualStyleBackColor = true;
            // 
            // DisassemblyView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(452, 451);
            this.Controls.Add(this.breakpointButton);
            this.Controls.Add(this.memoryButton);
            this.Controls.Add(this.assembleButton);
            this.Controls.Add(this.nopButton);
            this.Controls.Add(this.disasmList);
            this.Controls.Add(this.jumpButton);
            this.Controls.Add(this.addrLabel);
            this.Controls.Add(this.addressBox);
            this.MinimumSize = new System.Drawing.Size(380, 380);
            this.Name = "DisassemblyView";
            this.Text = "Disassembly";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox addressBox;
        private Label addrLabel;
        private Button jumpButton;
        private ListView disasmList;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private Button nopButton;
        private Button assembleButton;
        private Button memoryButton;
        private Button breakpointButton;
    }
}