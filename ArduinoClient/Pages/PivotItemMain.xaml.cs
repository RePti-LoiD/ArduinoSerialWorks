using ArduinoClient.Data;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Input;
using System;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;

namespace ArduinoClient.Pages
{
    public sealed partial class PivotItemMain : Page, IDisposable
    {
        private SerialPort serialPort;

        private bool toggleButtonState;

        public PivotItemMain()
        {
            InitializeComponent();

            Loaded += (x, y) =>
            {
                serialPort = MainWindow.PortConfig.BuildSerialPort();

                SerialPortConfigHandler.OnConfigChanged += (x) =>
                {
                    serialPort.Close();
                    toggle.IsChecked = false;

                    serialPort = x.BuildSerialPort();

                    PackageStateText.Text = serialPort.PortName;
                };
            };
        }

        private async void TryStartRead(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((bool)(sender as ToggleButton).IsChecked)
                {
                    await Task.Run(() => WritePortAsync());
                }
                else
                {
                    serialPort.Close();
                }
            }
            catch (Exception ex)
            {
                (sender as ToggleButton).IsChecked = false;

                ContentDialog dialog = new ContentDialog();

                dialog.XamlRoot = MainPage.instance.XamlRoot;
                dialog.Title = ex.Message;
                dialog.PrimaryButtonText = "Ok";
                dialog.DefaultButton = ContentDialogButton.Primary;
                dialog.Content = ex.StackTrace;

                SerialPortConfigHandler.StartDialogAsync(dialog);
            }
        }

        private void SliderNewValue(object sender, RangeBaseValueChangedEventArgs e)
        {
            PackageStateText.Text = $"Package sent: {e.NewValue}";
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
            serialPort?.Dispose();
        }

        //TODO: Добавить в настройки частоту записи в порт в МС.
        private void WritePortAsync()
        {
            serialPort.Open();

            while (true)
            {
                if (serialPort.IsOpen)
                {
                    byte data = 0;
                    if (toggleButtonState)
                    {
                        data = 1;
                    }

                    serialPort.WriteLine(Convert.ToBoolean(data).ToString() + ".");
                }

                if (!serialPort.IsOpen)
                    break;

                Thread.Sleep(1000);
            }
        }

        private void ToggleButtonClicked(object sender, RoutedEventArgs e)
        {
            toggleButtonState = (bool)(sender as ToggleButton).IsChecked;
        }
    }
}