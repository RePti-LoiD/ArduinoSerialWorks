using System;

using WinRT.Interop;

using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using ArduinoClient.Data;

namespace ArduinoClient
{
    public sealed partial class MainWindow : Window
    {
        public AppWindow appWindow;
        public static SerialPortConfig PortConfig;

        public MainWindow()
        {
            InitializeComponent();

            Activated += (x, y) =>
            {
                PortConfig = SerialPortConfigHandler.TryRestoreConfig();

                IntPtr winHandle = WindowNative.GetWindowHandle(this);
                WindowId winId = Win32Interop.GetWindowIdFromWindow(winHandle);

                appWindow = AppWindow.GetFromWindowId(winId);

                ResizeAndCenterWindow(700, 600);
                
                SizeChanged += (x, y) =>
                {
                    ResizeAndCenterWindow(700, 600);
                };
            };
        }

        private void ResizeAndCenterWindow(int width, int height)
        {
            appWindow.Resize(new Windows.Graphics.SizeInt32(_Width: width, _Height: height));
            appWindow.Title = "Arduino Serial Test";
            appWindow.Move(new Windows.Graphics.PointInt32(_X: (1920 - width) / 2, _Y: (1080 - height) / 2));
        }
    }
}
