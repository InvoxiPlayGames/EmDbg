using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using EmDbg.Types;

namespace EmDbg.ImGuiUI
{
    class StatusWindow
    {
        public static bool Showing = true;
        private static bool _is_running = true;
        private static string _running_title = string.Empty;
        private static string _identification = string.Empty;
        private static bool _pending_change = true;

        public static void HandleExecutionState(bool running)
        {
            _is_running = running;
            _pending_change = true;
            if (running)
            {
                ThreadContextWindow.SetContextStale();
            }
        }
        
        static string XamLoaderTerminateTitleAddr = "82c4bccc";

        public static void ShowWindow(XboxDebugger debugger)
        {
            if (Showing)
            {
                if (ImGui.Begin("Status", ref Showing, ImGuiWindowFlags.AlwaysAutoResize))
                {
                    // fetch console status if we need to
                    if (_pending_change)
                    {
                        _running_title = debugger.RunningTitle();
                        _identification = debugger.Identification();
                        _pending_change = false;
                    }
                    // display console status
                    ImGui.Text($"Console: {_identification} | Title: {_running_title} | {(_is_running ? "Running" : "Paused")}");
                    ImGui.InputText("XamLoaderTerminateTitle", ref XamLoaderTerminateTitleAddr, 8);
                    if (ImGui.Button("Terminate"))
                    {
                        var ctx = debugger.GetThreadContext(0xF9000000);
                        ctx.pc = Convert.ToUInt32(XamLoaderTerminateTitleAddr, 16);
                        debugger.SetThreadContext(0xF9000000, ctx);
                        debugger.ResumeExecution();
                    }
                    ImGui.SameLine();
            

                    // pause/resume button
                    if (ImGui.Button(_is_running ? "Pause" : "Resume"))
                    {
                        if (_is_running)
                            debugger.StopExecution();
                        else
                        {
                            debugger.ResumeExecution();
                            DisassemblyWindow.highlightState = DisassemblyWindow.InstructionHighlightState.NONE;
                        }
                    
                    }
                }
                ImGui.End();
            }
        }
    }
}
