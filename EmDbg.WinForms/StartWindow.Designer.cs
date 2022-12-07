namespace EmDbg.WinForms
{
    partial class StartWindow
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
            this.refreshButton = new System.Windows.Forms.LinkLabel();
            this.searchingLabel = new System.Windows.Forms.Label();
            this.connectButton = new System.Windows.Forms.Button();
            this.ipBox = new System.Windows.Forms.TextBox();
            this.ipAddrLabel = new System.Windows.Forms.Label();
            this.consolesList = new System.Windows.Forms.ListBox();
            this.consoleHeading = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // refreshButton
            // 
            this.refreshButton.AutoSize = true;
            this.refreshButton.Location = new System.Drawing.Point(214, 9);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(46, 15);
            this.refreshButton.TabIndex = 17;
            this.refreshButton.TabStop = true;
            this.refreshButton.Text = "Refresh";
            this.refreshButton.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.refreshButton_LinkClicked);
            // 
            // searchingLabel
            // 
            this.searchingLabel.AutoSize = true;
            this.searchingLabel.ForeColor = System.Drawing.SystemColors.GrayText;
            this.searchingLabel.Location = new System.Drawing.Point(192, 9);
            this.searchingLabel.Name = "searchingLabel";
            this.searchingLabel.Size = new System.Drawing.Size(68, 15);
            this.searchingLabel.TabIndex = 16;
            this.searchingLabel.Text = "Searching...";
            this.searchingLabel.Visible = false;
            // 
            // connectButton
            // 
            this.connectButton.Enabled = false;
            this.connectButton.Location = new System.Drawing.Point(189, 172);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(71, 23);
            this.connectButton.TabIndex = 13;
            this.connectButton.Text = "Connect";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // ipBox
            // 
            this.ipBox.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ipBox.Location = new System.Drawing.Point(83, 174);
            this.ipBox.Name = "ipBox";
            this.ipBox.Size = new System.Drawing.Size(100, 20);
            this.ipBox.TabIndex = 12;
            this.ipBox.TextChanged += new System.EventHandler(this.ipBox_TextChanged);
            // 
            // ipAddrLabel
            // 
            this.ipAddrLabel.AutoSize = true;
            this.ipAddrLabel.Location = new System.Drawing.Point(12, 175);
            this.ipAddrLabel.Name = "ipAddrLabel";
            this.ipAddrLabel.Size = new System.Drawing.Size(65, 15);
            this.ipAddrLabel.TabIndex = 11;
            this.ipAddrLabel.Text = "IP Address:";
            // 
            // consolesList
            // 
            this.consolesList.FormattingEnabled = true;
            this.consolesList.ItemHeight = 15;
            this.consolesList.Location = new System.Drawing.Point(12, 27);
            this.consolesList.Name = "consolesList";
            this.consolesList.Size = new System.Drawing.Size(248, 139);
            this.consolesList.TabIndex = 10;
            this.consolesList.SelectedIndexChanged += new System.EventHandler(this.consolesList_SelectedIndexChanged);
            // 
            // consoleHeading
            // 
            this.consoleHeading.AutoSize = true;
            this.consoleHeading.Location = new System.Drawing.Point(12, 9);
            this.consoleHeading.Name = "consoleHeading";
            this.consoleHeading.Size = new System.Drawing.Size(88, 15);
            this.consoleHeading.TabIndex = 9;
            this.consoleHeading.Text = "Xbox Consoles:";
            // 
            // StartWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(272, 205);
            this.Controls.Add(this.refreshButton);
            this.Controls.Add(this.searchingLabel);
            this.Controls.Add(this.connectButton);
            this.Controls.Add(this.ipBox);
            this.Controls.Add(this.ipAddrLabel);
            this.Controls.Add(this.consolesList);
            this.Controls.Add(this.consoleHeading);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StartWindow";
            this.Text = "EmDbg";
            this.Load += new System.EventHandler(this.StartWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private LinkLabel refreshButton;
        private Label searchingLabel;
        private Button connectButton;
        private TextBox ipBox;
        private Label ipAddrLabel;
        private ListBox consolesList;
        private Label consoleHeading;
    }
}