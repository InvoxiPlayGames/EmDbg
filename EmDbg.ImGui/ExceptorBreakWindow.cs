using ImGuiNET;
using NeighborSharp.Types;
using NeighborSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using EmDbg.Types;

namespace EmDbg.ImGuiUI
{
    internal class ExceptorBreakWindow
    {

        private static bool _show_ui = false;
        private static bool _is_breakpoint = false;
        private static ExceptionInfo _exception_info;
        private static Breakpoint _breakpoint_info;

        private static uint threadID;
        private static uint address;

        public static void ShowExceptionWindow(ExceptionInfo ex)
        {
            _show_ui = true;
            _exception_info = ex;
        }

        public static void ShowBreakpointWindow(Breakpoint bp)
        {
            _show_ui = true;
            _breakpoint_info = bp;
            _is_breakpoint = true;
        }

        public static bool RenderWindow()
        { 
            // set the window data
            ImGui.SetNextWindowPos(ImGui.GetMainViewport().GetCenter(), ImGuiCond.Once, new Vector2(0.5f, 0.5f));
            ImGui.SetNextWindowSize(new Vector2(300, 150));
            
            if (_show_ui)
            {
                if (_is_breakpoint)
                {
                    if (ImGui.Begin("Breakpoint Hit", ref _show_ui, ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoTitleBar ))
                    {
                    address = _breakpoint_info.breakAddr;
                    threadID = _breakpoint_info.thread;

                    ImGui.Text("A breakpoint has been hit at:");
                    ImGui.Text($"0x{address:X8}");
                    ImGui.Text($"Thread/Module: 0x{threadID:X8}/TODO");
                    ImGui.Text("Code Module: TODO");

                    if (ImGui.Button("Close"))
                        _show_ui = false;
                    }
                    // end displaying the window
                    ImGui.End();
                }
                else
                {
                    if (ImGui.Begin("Exception Thrown", ref _show_ui, ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoTitleBar ))
                    {
                    address = _exception_info.exceptAddress;
                    threadID = _exception_info.thread;

                    ImGui.Text("An exception has been thrown at:");
                    ImGui.Text($"0x{address:X8}");
                    ImGui.Text($"Exception Code: 0x{address:X8}");
                    ImGui.Text($"Exception Data: 0x{_exception_info.exceptType:X8}");
                    ImGui.Text($"Thread/Module: 0x{threadID:X8}/TODO");
                    ImGui.Text("Code Module: TODO");

                    if (ImGui.Button("Close"))
                        _show_ui = false;
                    }
                    // end displaying the window
                    ImGui.End();
                }

            }
            return false;
        }
    }
}
