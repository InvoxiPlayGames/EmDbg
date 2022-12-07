using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EmDbg;
using EmDbg.Types;

namespace EmDbg.WinForms
{
    public partial class ControlWindow : Form
    {
        private XboxDebugger Debugger;
        private DisassemblyView disassemblyView;
        private ExceptOrBreakWindow exceptOrBreak;
        private bool ShowingException;

        private void HandleDebugMessage(ThreadInfo? thread, string debugMsg, bool lf)
        {
            Invoke(() =>
            {
                logList.AppendText(debugMsg);
                if (lf) logList.AppendText("\r\n");
            });
        }

        private void HandleExecutionState(bool running)
        {
            Invoke(() =>
            {
                Text = $"EmDbg - {Debugger.Identification()} - {Debugger.RunningTitle()}{(!running ? " (paused)" : "")}";
                stateChangeButton.Text = running ? "Stop Game" : "Start Game";
            });
        }

        private void HandleFinishedException()
        {
            Invoke(() =>
            {
                ShowingException = false;
            });
        }

        private void HandleException(ExceptionInfo ex)
        {
            Invoke(() =>
            {
                if (ShowingException) return;
                ShowingException = true;
                exceptOrBreak.ShowException(ex);
            });
        }

        private void HandleBreakpoint(Breakpoint bp)
        {
            Invoke(() =>
            {
                if (ShowingException) return;
                ShowingException = true;
                exceptOrBreak.ShowBreakpoint(bp);
            });
        }

        public ControlWindow(XboxDebugger debugger)
        {
            Debugger = debugger;
            debugger.cbDebugString += HandleDebugMessage;
            debugger.cbExecutionStateChange += HandleExecutionState;
            debugger.cbExceptionHit += HandleException;
            debugger.cbBreakpointHit += HandleBreakpoint;
            disassemblyView = new DisassemblyView(Debugger);
            exceptOrBreak = new ExceptOrBreakWindow(Debugger, HandleFinishedException, disassemblyView.JumpToAddress, disassemblyView.JumpToAddress);
            InitializeComponent();
        }

        private void ControlWindow_Load(object sender, EventArgs e)
        {
            Debugger.SubscribeNotifications();
            Debugger.ClearAllBreakpoints();
        }

        private void stateChangeButton_Click(object sender, EventArgs e)
        {
            if (!Debugger.IsExecutionRunning())
                Debugger.ResumeExecution();
            else
                Debugger.StopExecution();
        }

        private void disasmCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (disasmCheck.Checked)
                disassemblyView.Show();
            else
                disassemblyView.Hide();
        }

        private void disconnectButton_Click(object sender, EventArgs e)
        {
            // todo: actually clean shit up
            Close();
        }
    }
}
