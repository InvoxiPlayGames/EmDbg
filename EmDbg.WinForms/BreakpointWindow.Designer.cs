namespace EmDbg.WinForms
{
    partial class BreakpointWindow
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
            this.bpList = new System.Windows.Forms.ListBox();
            this.addrBox = new System.Windows.Forms.TextBox();
            this.executeButton = new System.Windows.Forms.Button();
            this.readButton = new System.Windows.Forms.Button();
            this.writeButton = new System.Windows.Forms.Button();
            this.removeButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // bpList
            // 
            this.bpList.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.bpList.FormattingEnabled = true;
            this.bpList.ItemHeight = 19;
            this.bpList.Location = new System.Drawing.Point(12, 12);
            this.bpList.Name = "bpList";
            this.bpList.Size = new System.Drawing.Size(228, 327);
            this.bpList.TabIndex = 0;
            // 
            // addrBox
            // 
            this.addrBox.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.addrBox.Location = new System.Drawing.Point(246, 12);
            this.addrBox.Name = "addrBox";
            this.addrBox.Size = new System.Drawing.Size(63, 23);
            this.addrBox.TabIndex = 1;
            this.addrBox.Text = "00000000";
            // 
            // executeButton
            // 
            this.executeButton.Location = new System.Drawing.Point(246, 41);
            this.executeButton.Name = "executeButton";
            this.executeButton.Size = new System.Drawing.Size(63, 23);
            this.executeButton.TabIndex = 2;
            this.executeButton.Text = "Execute";
            this.executeButton.UseVisualStyleBackColor = true;
            this.executeButton.Click += new System.EventHandler(this.executeButton_Click);
            // 
            // readButton
            // 
            this.readButton.Location = new System.Drawing.Point(246, 70);
            this.readButton.Name = "readButton";
            this.readButton.Size = new System.Drawing.Size(63, 23);
            this.readButton.TabIndex = 3;
            this.readButton.Text = "Read";
            this.readButton.UseVisualStyleBackColor = true;
            // 
            // writeButton
            // 
            this.writeButton.Location = new System.Drawing.Point(246, 99);
            this.writeButton.Name = "writeButton";
            this.writeButton.Size = new System.Drawing.Size(63, 23);
            this.writeButton.TabIndex = 4;
            this.writeButton.Text = "Write";
            this.writeButton.UseVisualStyleBackColor = true;
            // 
            // removeButton
            // 
            this.removeButton.Location = new System.Drawing.Point(246, 316);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(63, 23);
            this.removeButton.TabIndex = 5;
            this.removeButton.Text = "Remove";
            this.removeButton.UseVisualStyleBackColor = true;
            // 
            // BreakpointWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(318, 352);
            this.Controls.Add(this.removeButton);
            this.Controls.Add(this.writeButton);
            this.Controls.Add(this.readButton);
            this.Controls.Add(this.executeButton);
            this.Controls.Add(this.addrBox);
            this.Controls.Add(this.bpList);
            this.Name = "BreakpointWindow";
            this.Text = "Breakpoints";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ListBox bpList;
        private TextBox addrBox;
        private Button executeButton;
        private Button readButton;
        private Button writeButton;
        private Button removeButton;
    }
}