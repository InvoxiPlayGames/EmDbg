using EmDbg.Types;
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
    public delegate void FinishedHandler();
    public delegate void JumpInDisassembly(uint address);
    public delegate void JumpInMemoryViewer(uint address);

    public partial class ExceptOrBreakWindow : Form
    {
        private readonly XboxDebugger Debugger;
        private readonly FinishedHandler Finished;
        private readonly JumpInDisassembly JumpDisassembly;
        private readonly JumpInMemoryViewer JumpMemoryViewer;

        public ExceptOrBreakWindow(XboxDebugger debugger, FinishedHandler handler, JumpInDisassembly jumpInDisassembly, JumpInMemoryViewer jumpInMemoryViewer)
        {
            Debugger = debugger;
            Finished = handler;
            JumpDisassembly = jumpInDisassembly;
            JumpMemoryViewer = jumpInMemoryViewer;
            InitializeComponent();
        }

        private uint threadID;
        private uint address;
        private bool mustReboot;

        public void ShowBreakpoint(Breakpoint bp)
        {
            mustReboot = false;
            threadID = bp.thread;
            address = bp.breakAddr;
            Text = "Breakpoint Hit";
            restartButton.Text = "Exit Title";
            typeLabel.Text = "A breakpoint has been hit at:";
            primaryAddrText.Text = $"0x{bp.breakAddr:X8}";
            codeLabel.Visible = false;
            dataLabel.Visible = false;
            exceptCodeText.Visible = false;
            exceptDataText.Visible = false;
            threadModuleText.Text = $"0x{bp.thread:X8} / TODO";
            moduleText.Text = "TODO";
            Show();
        }

        public void ShowException(ExceptionInfo ex)
        {
            mustReboot = true;
            threadID = ex.thread;
            address = ex.exceptAddress;
            Text = "Exception Thrown";
            restartButton.Text = "Reboot Xbox";
            typeLabel.Text = "An exception has been thrown at:";
            codeLabel.Visible = true;
            dataLabel.Visible = true;
            exceptCodeText.Visible = true;
            exceptDataText.Visible = true;
            primaryAddrText.Text = $"0x{ex.exceptAddress:X8}";
            exceptCodeText.Text = $"0x{ex.exceptType:X8}";
            threadModuleText.Text = $"0x{ex.thread:X8} / TODO";
            moduleText.Text = "TODO";
            exceptDataText.Text = "unknown";
            if (ex.exceptType == 0xC0000005)
            {
                if (ex.write)
                    exceptDataText.Text = $"invalid write to 0x{ex.responsibleAddress:X8}";
                else
                    exceptDataText.Text = $"invalid read from 0x{ex.responsibleAddress:X8}";
            }
            Show();
        }

        private void skipOverButton_Click(object sender, EventArgs e)
        {
            // suspend threads just in case
            Debugger.StopExecution();
            // edit the thread context
            ThreadContext ctx = Debugger.GetThreadContext(threadID);
            ctx.pc += 4;
            Debugger.SetThreadContext(threadID, ctx);
            // resume execution
            Debugger.ResumeExecution();
            Hide();
            Finished();
        }

        private void continueButton_Click(object sender, EventArgs e)
        {
            Debugger.ResumeExecution();
            Hide();
            Finished();
        }

        private void restartButton_Click(object sender, EventArgs e)
        {
            if (mustReboot)
                Debugger.RestartSystem();
            else
                Debugger.RestartTitle();
            Hide();
            Finished();
        }

        private void viewDisassemblyButton_Click(object sender, EventArgs e)
        {
            JumpDisassembly(address);
        }
    }
}
