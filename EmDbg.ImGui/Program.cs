using ImGuiNET;
using NeighborSharp;
using NeighborSharp.Types;
using System.Diagnostics;
using System.Numerics;
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

        static void Main(string[] args)
        {
            // Create all the objects necessary for ImGui
            VeldridStartup.CreateWindowAndGraphicsDevice(
                new WindowCreateInfo(50, 50, 1280, 720, WindowState.Normal, "EmDbg"),
                new GraphicsDeviceOptions(true, null, true, ResourceBindingModel.Improved, true, true),
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
            _is_connected = true;
            // set up console logging
            _debugger.ReportDebugLogs = true;
            _debugger.cbDebugString += ConsoleWindow.HandleDebugMessage;
            _debugger.cbExecutionStateChange += StatusWindow.HandleExecutionState;
            // subscribe to the notifications
            _debugger.SubscribeNotifications(true);
        }

        private static void SubmitUI()
        {
            //ImGui.ShowDemoWindow();

            // display connection discovery dialog
            if (!_is_connected && ConnectWindow.ShowWindow())
                ConnectToXbox(ConnectWindow.SelectedIP());

            // display dialogs for when the console is connected
            if (_is_connected && _debugger != null)
            {
                StatusWindow.ShowWindow(_debugger);
                ConsoleWindow.ShowWindow();
            }
        }
    }
}