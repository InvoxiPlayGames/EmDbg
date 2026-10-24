using System.Runtime.CompilerServices;
using EmDbg.Types;
using ImGuiNET;

namespace EmDbg.ImGuiUI;

public class ThreadContextWindow
{
    public static bool Showing = true;
    
    public static ThreadContext? ctx = null;
    public static uint threadId;
    public static bool contextIsCurrent;

    protected static void SetContext(ThreadContext ctx)
    {
        ThreadContextWindow.ctx = ctx;
        contextIsCurrent = true;
        // Flush all the memory blocks that registers might be pointing to
        for (uint i = 0; i < 32; i++)
        {
            if (ctx.gpr[i] != 0)
            {
                var ptr = (uint)ctx.gpr[i];
                MemoryAccess.FlushMemoryAtRange(ptr, ptr);
            }
        }
    }

    public static void SetThread(uint newthreadId)
    {
        try
        {
            var newCtx = MemoryAccess.currentDebugger.GetThreadContext(newthreadId);
            SetContext(newCtx);
            threadId = newthreadId;
        }
        catch (Exception e)
        {
            Program.Exceptions.Add(e);
        }
        
    }

    public static void SetContextStale()
    {
        contextIsCurrent = false;
    }
    
    public static void ShowWindow()
    {
        if (Showing)
        {
            if (ImGui.Begin("Thread Context", ref Showing, ImGuiWindowFlags.AlwaysAutoResize))
            {
                if (ctx == null)
                {
                    ImGui.Text("Context will be shown on break or exception.");
                }
                else
                {
                    if (!contextIsCurrent)
                    {
                        ImGui.BeginDisabled();
                    }
                    ImGui.TextUnformatted("Thread 0x" + threadId.ToString("X8"));
                    ImGui.TextUnformatted("PC:");
                    ImGui.SameLine();
                    if (ImGui.TextLink("0x" + ctx.pc.ToString("X8")))
                    {
                        DisassemblyWindow.startAddress = (ctx.pc - 0x20);
                    }

                    if (ImGui.BeginTable("GPR", 2, ImGuiTableFlags.Borders | ImGuiTableFlags.RowBg))
                    {
                        ImGui.TableSetupColumn("reg", ImGuiTableColumnFlags.WidthFixed);
                        ImGui.TableSetupColumn("value", ImGuiTableColumnFlags.WidthStretch);

                        for (int i = 0; i < 32; i++)
                        {
                            ImGui.PushID(i);
                            ImGui.TableNextRow();
                            
                            ImGui.TableSetColumnIndex(0);
                            ImGui.TextUnformatted("r" + i);
                            
                            ImGui.TableSetColumnIndex(1);
                            bool isPointer = false;
                            

                            if (ctx.gpr[i] != 0)
                            {
                                var pointed = MemoryAccess.GetWord((uint)ctx.gpr[i]);
                                if (pointed != null)
                                {
                                    var guessString = MemoryAccess.GuessString((uint)ctx.gpr[i]);
                                    if (ImGui.TextLink(ctx.gpr[i].ToString("X8")))
                                    {
                                        DisassemblyWindow.startAddress = ((uint)ctx.gpr[i] - 0x20);
                                        MemoryViewWindow.startAddress = (((uint)ctx.gpr[i] - 0x80) / 16) * 16;
                                    }
                                    ImGui.SameLine();
                                    if (guessString != null)
                                    {
                                        ImGui.Text($"-> \"${guessString}\"");
                                    }
                                    else
                                    {
                                        ImGui.Text("-> " + pointed.Value.ToString("X8"));
                                    }

                                    isPointer = true;
                                }
                            }

                            if(!isPointer)
                            {
                                ImGui.TextUnformatted(ctx.gpr[i].ToString("X8"));
                            }
                            ImGui.PopID();
                        }
                        
                        ImGui.EndTable();
                    }

                    if (!contextIsCurrent)
                    {
                        ImGui.EndDisabled();
                    }
                }
            }
            ImGui.End();
        }
    }
}