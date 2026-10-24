using System.Numerics;
using ImGuiNET;

namespace EmDbg.ImGuiUI;

public class MemoryViewWindow
{
    public static bool Showing = true;
    public static uint startAddress = 0;


    private static string UIJumpAddr = "40000000";

    public static void ShowWindow()
    {
        if (Showing)
        {
            if (ImGui.Begin("Memory View", ref Showing, ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.MenuBar))
            {
                if (ImGui.BeginMenuBar())
                {
                    if (ImGui.MenuItem("Refresh"))
                    {
                        MemoryAccess.memoryBlocks.Clear();
                    }

                    if (ImGui.BeginMenu("Jump"))
                    {
                        ImGui.InputText("Address", ref UIJumpAddr, 8);
                        if (ImGui.Button("Jump"))
                        {
                            try
                            {
                                startAddress = Convert.ToUInt32(UIJumpAddr, 16);
                            }
                            catch (Exception e)
                            {
                            }
                        }

                        ImGui.EndMenu();
                    }

                    ImGui.EndMenuBar();
                }

                if (ImGui.GetIO().MouseWheel != 0 && ImGui.IsWindowHovered())
                {
                    var diff = (int)(ImGui.GetIO().MouseWheel * 16);
                    // hack: just do it with signed longs lmao
                    var newScroll = (long)startAddress;
                    newScroll -= diff;
                    if (newScroll < 0)
                    {
                        newScroll = 0;
                    }

                    startAddress = (uint)newScroll;
                }

                //ImGui.InputInt("Start Address", ref startAddress);
                var drawlist = ImGui.GetWindowDrawList();

                ImGui.PushStyleVar(ImGuiStyleVar.ItemSpacing, ImGui.GetStyle().ItemSpacing with { Y = 0 });
                if (ImGui.BeginTable("Mem", 4, ImGuiTableFlags.Borders | ImGuiTableFlags.RowBg))
                {
                    ImGui.TableSetupColumn("Address", ImGuiTableColumnFlags.WidthFixed);
                    ImGui.TableSetupColumn("Data1", ImGuiTableColumnFlags.WidthStretch);
                    ImGui.TableSetupColumn("Data2", ImGuiTableColumnFlags.WidthStretch);
                    ImGui.TableSetupColumn("ASCII", ImGuiTableColumnFlags.WidthFixed);

                    var contentRegionAvail = ImGui.GetContentRegionAvail();
                    for (uint i = 0; ImGui.GetCursorPosY() < contentRegionAvail.Y + 20; i++)
                    {
                        var addr = startAddress + i * 16;
                        var data = MemoryAccess.GetMemory((uint)addr, 16);

                        ImGui.TableNextRow();
                        ImGui.TableSetColumnIndex(0);
                        var start = ImGui.GetCursorScreenPos();
                        ImGui.Text(addr.ToString("X8"));
                        if (data != null)
                        {
                            for (int x = 0; x < 2; x++)
                            {
                                ImGui.TableSetColumnIndex(x + 1);
                                for (int y = 0; y < 8; y++)
                                {
                                    ImGui.Text(data[y + x * 8].ToString("X2"));
                                    ImGui.SameLine();
                                }
                            }

                            ImGui.TableSetColumnIndex(3);
                            char[] asciis = new char[16];
                            for (int c = 0; c < 16; c++)
                            {
                                if (data[c] >= 0x20 && data[c] <= 0x7E)
                                {
                                    asciis[c] = (char)data[c];
                                }
                                else
                                {
                                    asciis[c] = '.';
                                }
                            }

                            ImGui.TextUnformatted(new string(asciis));
                        }
                        else
                        {
                            ImGui.TableSetColumnIndex(1);
                            var block = MemoryAccess.GetBlocksForAddressRange((uint)addr, (uint)addr)[0];
                            if (block.status == MemoryAccess.MemoryBlock.Status.REQUESTED)
                            {
                                ImGui.Text("Loading...");
                            }

                            if (block.status == MemoryAccess.MemoryBlock.Status.ERROR)
                            {
                                ImGui.Text("Error");
                            }
                        }
                    }

                    ImGui.EndTable();
                }

                ImGui.PopStyleVar();
            }

            ImGui.End();
        }
    }
}