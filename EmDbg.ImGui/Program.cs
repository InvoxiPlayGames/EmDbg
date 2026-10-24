using ImGuiNET;
using NeighborSharp;
using NeighborSharp.Types;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.InteropServices;
using Veldrid;
using Veldrid.Sdl2;
using Veldrid.StartupUtilities;
using Vulkan;
using EmDbg;
using Vortice.Mathematics;

namespace EmDbg.ImGuiUI
{
    class Program
    {
        // ImGui rendering variables
        private static Sdl2Window? _window;
        private static GraphicsDevice? _gd;
        private static CommandList? _cl;
        private static ImGuiController? _controller;
        private static Vector3 _clearColor = new Vector3(0.45f, 0.55f, 0.6f);

        // EmDbg variables
        private static Xbox360? _console;
        private static XboxDebugger? _debugger;
        private static bool _is_connected;
        
        public static List<Exception> Exceptions = new List<Exception>();

        static void Main(string[] args)
        {

            var backend = VeldridStartup.GetPlatformDefaultBackend();
            
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                // I'm just going to force Vulkan on Windows, trying to figure out DirectX shaders was too annoying.
                backend = GraphicsBackend.Vulkan;
            }
            
            // Create all the objects necessary for ImGui
            VeldridStartup.CreateWindowAndGraphicsDevice(
                new WindowCreateInfo(50, 50, 1280, 720, WindowState.Normal, "EmDbg"),
                new GraphicsDeviceOptions(false, null, true, ResourceBindingModel.Improved, true, true),
                backend,
                out _window,
                out _gd);
            _cl = _gd.ResourceFactory.CreateCommandList();
            _controller = new ImGuiController(_gd, _gd.MainSwapchain.Framebuffer.OutputDescription, _window.Width, _window.Height);
            _window.Resized += () =>
            {
                _gd.MainSwapchain.Resize((uint)_window.Width, (uint)_window.Height);
                _controller.WindowResized(_window.Width, _window.Height);
            };

            // Do initial background tasks
            ConnectWindow.Search();

            // Main application loop
            var stopwatch = Stopwatch.StartNew();
            float deltaTime = 0f;
            while (_window.Exists)
            {
                deltaTime = stopwatch.ElapsedTicks / (float)Stopwatch.Frequency;
                stopwatch.Restart();
                InputSnapshot snapshot = _window.PumpEvents();
                if (!_window.Exists) { break; }
                _controller.Update(deltaTime, snapshot); // Feed the input events to our ImGui controller, which passes them through to ImGui.


                // EmDbg handling functions
                if (_is_connected && _debugger != null)
                    _debugger.PulseNotify(); // listen for new messages on the notification thread

                // UI handling functions
                SubmitUI();

                // tell the imguicontroller to render to the graphics device
                _cl.Begin();
                _cl.SetFramebuffer(_gd.MainSwapchain.Framebuffer);
                _cl.ClearColorTarget(0, new RgbaFloat(_clearColor.X, _clearColor.Y, _clearColor.Z, 1f));
                _controller.Render(_gd, _cl);
                _cl.End();
                _gd.SubmitCommands(_cl);
                _gd.SwapBuffers(_gd.MainSwapchain);
            }

            // Clean up resources
            _gd.WaitForIdle();
            _controller.Dispose();
            _cl.Dispose();
            _gd.Dispose();
        }

        private static void ConnectToXbox(string ip)
        {
            // connect to the console
            _console = new(ip);
            _debugger = new(_console);
            MemoryAccess.currentDebugger = _debugger;
            _is_connected = true;
            // set up console logging
            _debugger.ReportDebugLogs = true;
            _debugger.cbDebugString += ConsoleWindow.HandleDebugMessage;
            _debugger.cbExecutionStateChange += StatusWindow.HandleExecutionState;
            _debugger.cbExceptionHit += ExceptionWindow.OnException;
            _debugger.cbBreakpointHit += DisassemblyWindow.BreakpointHit;
            // subscribe to the notifications
            _debugger.SubscribeNotifications(true);
        }

        private static void SubmitUI()
        {
            if (ImGui.BeginMainMenuBar())
            {
                if (ImGui.BeginMenu("Windows", _is_connected))
                {
                    if (ImGui.MenuItem("Status", null, StatusWindow.Showing))
                    {
                        StatusWindow.Showing = !StatusWindow.Showing;
                    }
                    if (ImGui.MenuItem("Console", null, ConsoleWindow.Showing))
                    {
                        ConsoleWindow.Showing = !ConsoleWindow.Showing;
                    }
                    if (ImGui.MenuItem("Memory View", null, MemoryViewWindow.Showing))
                    {
                        MemoryViewWindow.Showing = !MemoryViewWindow.Showing;
                    }
                    if (ImGui.MenuItem("Dissasembly", null, DisassemblyWindow.Showing))
                    {
                        DisassemblyWindow.Showing = !DisassemblyWindow.Showing;
                    }
                    if (ImGui.MenuItem("Thread Context", null, ThreadContextWindow.Showing))
                    {
                        ThreadContextWindow.Showing = !ThreadContextWindow.Showing;
                    }
                    ImGui.EndMenu();
                }
                ImGui.EndMainMenuBar();
            }
            ImGui.DockSpaceOverViewport(0);
            ImGui.ShowDemoWindow();

            // display connection discovery dialog
            try
            {
                if (!_is_connected && ConnectWindow.ShowWindow())
                    ConnectToXbox(ConnectWindow.SelectedIP());
            }
            catch (Exception e)
            {
                Exceptions.Add(e);
            }
            

            // display dialogs for when the console is connected
            if (_is_connected && _debugger != null)
            {
                StatusWindow.ShowWindow(_debugger);
                ConsoleWindow.ShowWindow();
                ExceptionWindow.ShowWindow();
                DisassemblyWindow.ShowWindow();
                MemoryViewWindow.ShowWindow();
                ThreadContextWindow.ShowWindow();
            }

            if (Exceptions.Count > 0)
            {
                ImGui.OpenPopup("Error");
                bool _ = true;
                if (ImGui.BeginPopupModal("Error", ref _, ImGuiWindowFlags.AlwaysAutoResize))
                {
                    ImGui.Text(Exceptions.First().GetType().Name);
                    ImGui.Text(Exceptions.First().Message);
                    if (ImGui.CollapsingHeader("Callstack"))
                    {
                        ImGui.Text(Exceptions.First().StackTrace);
                    }

                    if (ImGui.Button("OK"))
                    {
                        Exceptions.RemoveAt(0);
                        ImGui.CloseCurrentPopup();
                    }
                    ImGui.EndPopup();
                }
            }
        }
    }
}
