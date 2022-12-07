using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NeighborSharp;
using NeighborSharp.Types;

namespace EmDbg.WinForms
{
    public partial class StartWindow : Form
    {
        public StartWindow()
        {
            InitializeComponent();
        }

        void SearchForXbox()
        {
            Invoke(() =>
            {
                searchingLabel.Visible = true;
                refreshButton.Visible = false;
                consolesList.ClearSelected();
                consolesList.Items.Clear();
            });
            DiscoveredConsole[] consoles = Xbox360Discovery.DiscoverAllConsoles();
            Invoke(() =>
            {
                searchingLabel.Visible = false;
                refreshButton.Visible = true;
                consolesList.Items.AddRange(consoles);
            });
        }

        private void refreshButton_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Task.Run(SearchForXbox);
        }

        private void ipBox_TextChanged(object sender, EventArgs e)
        {
            IPAddress? ip;
            connectButton.Enabled = IPAddress.TryParse(ipBox.Text, out ip);
        }

        private void consolesList_SelectedIndexChanged(object sender, EventArgs e)
        {
            DiscoveredConsole? console = (DiscoveredConsole)consolesList.SelectedItem;
            if (console == null)
            {
                ipBox.Text = "";
                connectButton.Enabled = false;
            } else
            {
                ipBox.Text = console.EndPoint.Address.ToString();
                connectButton.Enabled = true;
            }
        }

        private void StartWindow_Load(object sender, EventArgs e)
        {
            Task.Run(SearchForXbox);
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            Xbox360 console = new(ipBox.Text);
            XboxDebugger debugger = new(console);
            ControlWindow control = new(debugger);
            Hide();
            control.ShowDialog();
            Show();
        }
    }
}
