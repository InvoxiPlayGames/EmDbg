using System.Numerics;
using EmDbg.Types;
using ImGuiNET;

namespace EmDbg.ImGuiUI;

public class DisassemblyWindow
{
    public static bool Showing = true;
    public static uint startAddress = 0x80000000;

    public enum InstructionHighlightState
    {
        NONE,
        EXCEPTION,
        BREAK
    }

    public static InstructionHighlightState highlightState = InstructionHighlightState.NONE;
    public static uint highlightAddr = 0;

    private static string UIJumpAddr = "80000000";

    public static List<uint> tempBreakpoints = new();

    public static void ShowWindow()
    {
        if (Showing)
        {
            if (ImGui.Begin("Disassembly", ref Showing, ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.MenuBar))
            {
                if (ImGui.BeginMenuBar())
                {
                    if (highlightState == InstructionHighlightState.BREAK)
                    {
                        if (ImGui.MenuItem("Step"))
                        {
                            if (!MemoryAccess.currentDebugger.IsBreakpointed(highlightAddr + 4))
                            {
                                MemoryAccess.currentDebugger.ExecuteBreakpoint(highlightAddr + 4);
                                tempBreakpoints.Add(highlightAddr + 4);
                            }

                            MemoryAccess.currentDebugger.ResumeExecution();
                            highlightState = InstructionHighlightState.NONE;
                        }
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
                    var diff = (int)(ImGui.GetIO().MouseWheel * 4);
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
                uint[] stack = [];
                if (ThreadContextWindow.contextIsCurrent)
                {
                    stack = ThreadContextWindow.GetStack();
                }

                if (ImGui.BeginTable("Instructions", 3, ImGuiTableFlags.Borders | ImGuiTableFlags.RowBg))
                {
                    ImGui.TableSetupColumn("Actions", ImGuiTableColumnFlags.WidthFixed);
                    ImGui.TableSetupColumn("Address", ImGuiTableColumnFlags.WidthFixed);
                    ImGui.TableSetupColumn("Instruction", ImGuiTableColumnFlags.WidthStretch);

                    var contentRegionAvail = ImGui.GetContentRegionAvail();
                    for (uint i = 0; ImGui.GetCursorPosY() < contentRegionAvail.Y + 12; i++)
                    {
                        var addr = startAddress + i * 4;
                        var data = MemoryAccess.GetMemory((uint)addr, 4);
                        ImGui.PushID(addr.ToString("X"));
                        ImGui.TableNextRow();
                        ImGui.TableSetColumnIndex(0);
                        var start = ImGui.GetCursorScreenPos();
                        bool inStack = stack.Contains(addr);
                        if ((addr == highlightAddr && highlightState != InstructionHighlightState.NONE) || inStack)
                        {
                            uint color = 0x5000ffff;
                            if (highlightState == InstructionHighlightState.EXCEPTION)
                            {
                                color = 0x500000ff;
                            }

                            if (inStack)
                            {
                                color = 0x50ff0000;
                            }

                            drawlist.AddRectFilled(new Vector2(start.X, start.Y),
                                new Vector2(contentRegionAvail.X + start.X - 10, start.Y + ImGui.GetTextLineHeight()),
                                color);
                        }

                        var breakPointPos = ImGui.GetCursorScreenPos();
                        if (ImGui.InvisibleButton("Breakpoint", new Vector2(ImGui.GetTextLineHeight())))
                        {
                            try
                            {
                                if (MemoryAccess.currentDebugger.IsBreakpointed(addr))
                                {
                                    MemoryAccess.currentDebugger.ClearExecuteBreakpoint(addr);
                                }
                                else
                                {
                                    MemoryAccess.currentDebugger.ExecuteBreakpoint(addr);
                                }
                            }
                            catch (Exception e)
                            {
                                Program.Exceptions.Add(e);
                            }
                        }

                        uint breakpointOpacity = 0;
                        if (MemoryAccess.currentDebugger.IsBreakpointed(addr))
                        {
                            breakpointOpacity = 255;
                        }
                        else if (ImGui.IsItemHovered())
                        {
                            breakpointOpacity = 80;
                        }

                        if (breakpointOpacity > 0)
                        {
                            var col = 0x006060ff | (breakpointOpacity << 24);
                            drawlist.AddCircleFilled(breakPointPos + new Vector2(ImGui.GetTextLineHeight() / 2),
                                ImGui.GetTextLineHeight() / 2, col);
                        }

                        ImGui.TableSetColumnIndex(1);
                        ImGui.Text(addr.ToString("X8"));
                        ImGui.TableSetColumnIndex(2);
                        if (data != null)
                        {
                            ImGui.Text(PPCDisassembler.DisassembleToStrings(data, 0, true)[0]);
                        }
                        else
                        {
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

                        ImGui.PopID();
                    }

                    ImGui.EndTable();
                }
            }

            ImGui.End();
        }
    }

    public static void BreakpointHit(Breakpoint bp)
    {
        if (bp.breakAddr < startAddress || bp.breakAddr > startAddress + (8 * 4))
        {
            startAddress = bp.breakAddr - 8;
        }

        highlightState = InstructionHighlightState.BREAK;
        highlightAddr = bp.breakAddr;
        
        ThreadContextWindow.SetThread(bp.thread);
        
        if (tempBreakpoints.Contains(bp.breakAddr))
        {
            tempBreakpoints.Remove(bp.breakAddr);
            try
            {
                MemoryAccess.currentDebugger.ClearExecuteBreakpoint(bp.breakAddr);
            }
            catch (Exception e)
            {
                Program.Exceptions.Add(e);
            }
        }
    }
}