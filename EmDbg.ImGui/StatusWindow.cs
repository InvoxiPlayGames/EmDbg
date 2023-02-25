using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EmDbg.ImGuiUI
{
    class StatusWindow
    {
        private static bool _is_running = true;
        private static string _running_title = string.Empty;
        private static string _identification = string.Empty;
        private static bool _pending_change = true;

        public static void HandleExecutionState(bool running)
        {
            _is_running = running;
            _pending_change = true;
        }

        public static bool ShowWindow(XboxDebugger debugger)
        {
            ImGui.SetNextWindowPos(new Vector2(5, 5), ImGuiCond.Always);
            ImGui.SetNextWindowBgAlpha(0.35f);
            ImGui.Begin("Status", ImGuiWindowFlags.NoDecoration | ImGuiWindowFlags.NoNav | ImGuiWindowFlags.NoMove | ImGuiWindowFlags.AlwaysAutoResize);
            
            // fetch console status if we need to
            if (_pending_change)
            {
                _running_title = debugger.RunningTitle();
                _identification = debugger.Identification();
                _pending_change = false;
            }
            // display console status
            ImGui.Text($"Console: {_identification}");
            ImGui.Text($"Title: {_running_title}");
            ImGui.Text($"State: {(_is_running ? "Running" : "Paused")}");

            // pause/resume button
            if (ImGui.Button(_is_running ? "Pause" : "Resume"))
            {
                if (_is_running)
                    debugger.StopExecution();
                else
                    debugger.ResumeExecution();
            }

            ImGui.End();
            return true;
        }
    }
}
