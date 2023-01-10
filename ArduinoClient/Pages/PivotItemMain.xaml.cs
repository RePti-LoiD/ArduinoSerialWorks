using ArduinoClient.Data;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Input;
using System;
using System.IO.Ports;
using System.Threading.Tasks;

namespace ArduinoClient.Pages
{
    public sealed partial class PivotItemMain : Page, IDisposable
    {
        private SerialPort serialPort;
        private int sliderValue = 0;

        public PivotItemMain()
        {
            InitializeComponent();

            Loaded += (x, y) =>
            {
                serialPort = SettingsWindow.PortConfig.BuildSerialPort();

                SerialPortConfigHandler.OnConfigChanged += (x) =>
                {
                    serialPort.Close();

                    serialPort = x.BuildSerialPort();
                };
            };
        }

        private async void TryStartRead(object sender, RoutedEventArgs e)
        {
            if ((bool)(sender as ToggleButton).IsChecked)
            {
                //await Task.Run(() => WritePortAsync(serialPort));

                (Application.Current as App).ActivateSheduleConstructor();
            }
            else
            {
                serialPort.Close();
            }
        }

        private void SliderNewValue(object sender, RangeBaseValueChangedEventArgs e)
        {
            PackageStateText.Text = $"Package sent: {e.NewValue}";
            sliderValue = (int)e.NewValue;
        }

        private void TextBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                try
                {
                    Slider.Value = int.Parse((sender as TextBox).Text);
                }
                catch
                {
                    PackageStateText.Text = "Uncorrect value";
                }

                (sender as TextBox).Text = string.Empty;
            }
        }

        public void Dispose()
        {
            serialPort?.Close();
            serialPort?.Dispose();
        }

        private void WritePortAsync(SerialPort serialPort)
        {
            serialPort.Open();

            while (true)
            {
                serialPort.Write(sliderValue + ".");

                if (!serialPort.IsOpen)
                    break;
            }
        }
    }
}