using System;

using WinRT.Interop;

using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using ArduinoClient.Data;
using Windows.Graphics;

namespace ArduinoClient
{
    public sealed partial class SettingsWindow : Window
    {
        public bool IsActivated { get;  private set; }

        public AppWindow appWindow;
        public static SerialPortConfig PortConfig;

        private SizeInt32 windowSize;
        public SizeInt32 WindowSize { get => windowSize; set => windowSize = value; }

        public SettingsWindow()
        {
            InitializeComponent();

            VisibilityChanged += (x, y) =>
            {
                IsActivated = true;
            };

            Activated += (x, y) =>
            {
                windowSize.Width = 700;
                windowSize.Height = 600;

                PortConfig = SerialPortConfigHandler.TryRestoreConfig();

                IntPtr winHandle = WindowNative.GetWindowHandle(this);
                WindowId winId = Win32Interop.GetWindowIdFromWindow(winHandle);

                appWindow = AppWindow.GetFromWindowId(winId);

                ResizeAndCenterWindow(WindowSize.Width, WindowSize.Height);
                
                SizeChanged += (x, y) =>
                {
                    ResizeAndCenterWindow(WindowSize.Width, WindowSize.Height);
                };
            };

            Closed += (x, y) =>
            {
                IsActivated = false;
            };
        }

        private void ResizeAndCenterWindow(int width, int height)
        {
            appWindow.Resize(new SizeInt32(_Width: width, _Height: height));
            appWindow.Title = "Arduino Serial";
            appWindow.Move(new PointInt32(_X: (1920 - width) / 2, _Y: (1080 - height) / 2));
        }
    }
}
