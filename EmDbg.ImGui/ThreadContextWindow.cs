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
                    ImGui.Text("Waiting for break or exception...");
                }
                else
                {
                    if (!contextIsCurrent)
                    {
                        ImGui.PushStyleVar(ImGuiStyleVar.Alpha, 0.5f);
                    }
                    ImGui.TextUnformatted("Thread 0x" + threadId.ToString("X8"));
                    ImGui.TextUnformatted("PC:");
                    ImGui.SameLine();
                    if (ImGui.TextLink("0x" + ctx.pc.ToString("X8")))
                    {
                        DisassemblyWindow.startAddress = (ctx.pc - 0x20);
                    }
                    
                    ImGui.TextUnformatted($"CTR: 0x{ctx.ctr:X8}");
                    ImGui.TextUnformatted($"MSR: 0x{ctx.msr:X8}");
                    ImGui.TextUnformatted($"CR: 0x{ctx.cr:X8}");
                    ImGui.TextUnformatted($"XER: 0x{ctx.xer:X8}");
                    ImGui.TextUnformatted($"LR: 0x{ctx.lr:X8}");

                    if (ImGui.CollapsingHeader("GPR", ImGuiTreeNodeFlags.DefaultOpen))
                    {
                        DrawGPR();
                    }

                    if (ImGui.CollapsingHeader("Stack"))
                    {
                        DrawStack();
                    }

                    if (!contextIsCurrent)
                    {
                        ImGui.PopStyleVar();
                    }
                }
            }
            ImGui.End();
        }
    }

    public static uint[] GetStack()
    {
        List<uint> stack = new();
        uint stackPtr = (uint)ctx.gpr[1];
        for (int i = 0; i < 7; i++)
        {
            var read = MemoryAccess.GetWord(stackPtr);
            if (read == null) break;

            stackPtr = read.Value;
            var lr = MemoryAccess.GetWord(stackPtr - 8);
            if (lr == null) break;

            stack.Add(lr.Value);
        }
        return stack.ToArray();
    }

    private static void DrawStack()
    {
        var stack = GetStack();
        for (int i = 0; i < stack.Length; i++)
        {
            if (ImGui.TextLink($"{i + 1}: 0x{stack[i]:X8}"))
            {
                DisassemblyWindow.startAddress = (stack[i] - 0x20);
            }
        }
    }

    private static void DrawGPR()
    {
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
                            ImGui.Text($"-> \"{guessString}\"");
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
    }
}