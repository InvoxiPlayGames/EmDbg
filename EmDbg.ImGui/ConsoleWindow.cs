using EmDbg.Types;
using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace EmDbg.ImGuiUI
{
    class ConsoleWindow
    {
        public static List<string> console_messages = new List<string>(1);

        public static void HandleDebugMessage(ThreadInfo? thread, string message, bool newline)
        {
            lock (console_messages)
            {
                if (console_messages.Count == 0)
                    console_messages.Add(message);
                else
                    console_messages[console_messages.Count - 1] += message;
                if (newline) console_messages.Add("");
                if (console_messages.Count > 1000)
                    console_messages.RemoveAt(0);
            }
        }
        
        public static void ShowWindow()
        {
            ImGui.SetNextWindowSize(new Vector2(600, 400), ImGuiCond.FirstUseEver);
            ImGui.Begin("Debug Console");
            if (ImGui.BeginChild("ScrollingRegion", new Vector2(0, 0), false, ImGuiWindowFlags.HorizontalScrollbar))
            {
                ImGui.PushStyleVar(ImGuiStyleVar.ItemSpacing, new Vector2(4, 1));
                // display all the console messages in our list
                lock (console_messages)
                    foreach(string msg in console_messages)
                        ImGui.TextUnformatted(msg);
                // scroll to bottom if we're already at the bottom
                if (ImGui.GetScrollY() >= ImGui.GetScrollMaxY())
                    ImGui.SetScrollHereY(1.0f);
                ImGui.PopStyleVar();
            }
            ImGui.End();
        }
    }
}
