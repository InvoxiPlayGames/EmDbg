using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmDbg.WinForms
{
    public partial class DisassemblyView : Form
    {
        private XboxDebugger Debugger;

        public DisassemblyView(XboxDebugger debugger)
        {
            Debugger = debugger;
            InitializeComponent();
        }

        public void JumpToAddress(uint address)
        {
            Show();
            Invoke(() =>
            {
                uint startingAddress = address - 0x20;
                byte[] memory = Debugger.GetMemory(startingAddress, 0x80);
                string[] insts = PPCDisassembler.DisassembleToStrings(memory, startingAddress, true);
                disasmList.Items.Clear();
                uint currentAddress = startingAddress;
                foreach(string inst in insts)
                {
                    ListViewItem item = disasmList.Items.Add(new ListViewItem(new string[] { currentAddress.ToString("X8"), inst }));
                    if (currentAddress == address)
                        item.Selected = true;
                    currentAddress += 4;
                }
                disasmList.Select();
                addressBox.Text = address.ToString("X8");
            });
        }

        private void jumpButton_Click(object sender, EventArgs e)
        {
            uint address = Convert.ToUInt32(addressBox.Text, 16);
            JumpToAddress(address);

        }
    }
}
