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

namespace EmDbg.ImGuiUI
{
    internal class ConnectWindow
    {
        private static int _selected_console_index = -1;
        private static bool _is_searching = false;
        private static string _ip_address = "";
        private static List<DiscoveredConsole> _consoles = new List<DiscoveredConsole>();

        public static void DiscoverConsoles()
        {
            lock (_consoles) _consoles.Clear();
            _is_searching = true;
            DiscoveredConsole[] consoles = Xbox360Discovery.DiscoverAllConsoles();
            _is_searching = false;
            lock (_consoles) _consoles.AddRange(consoles);
        }

        public static string SelectedIP()
        {
            return _ip_address;
        }

        public static void Search()
        {
            _selected_console_index = -1;
            Task.Run(DiscoverConsoles);
        }

        public static bool ShowWindow()
        {
            // set the window data
            ImGui.SetNextWindowPos(ImGui.GetMainViewport().GetCenter(), ImGuiCond.Once, new Vector2(0.5f, 0.5f));
            ImGui.SetNextWindowSize(new Vector2(210, 280));
            ImGui.Begin("Select Console", ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoResize);

            // console list box
            if (ImGui.BeginListBox("##Consoles", new Vector2(200, 200)))
            {
                lock (_consoles)
                {
                    for (int i = 0; i < _consoles.Count; i++)
                        if (ImGui.Selectable(_consoles[i].ToString(), _selected_console_index == i))
                        {
                            _selected_console_index = i;
                            _ip_address = _consoles[i].EndPoint.Address.ToString();
                        }
                }
                ImGui.EndListBox();
            }
            ImGui.InputText("##IP Address", ref _ip_address, 16);
            if (ImGui.Button("Connect")) // if we press the button, return and have the main thread handle connections
                return true;
            ImGui.SameLine();

            // search for consoles button
            if (_is_searching)
                ImGui.Text("Searching...");
            else if (ImGui.Button("Refresh"))
                Search();

            // end displaying the window
            ImGui.End();
            return false;
        }
    }
}
