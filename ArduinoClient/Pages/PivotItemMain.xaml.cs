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

        private bool ass;

        public PivotItemMain()
        {
            InitializeComponent();

            Loaded += (x, y) => serialPort = SerialPortConfigSerialization.TryRestoreConfig().BuildSerialPort();
        }

        private async void TryStartRead(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((bool)(sender as ToggleButton).IsChecked)
                {
                    myButton.Content = "Stop Write";

                    await Task.Run(() => WritePort());
                }
                else
                {
                    serialPort.Close();

                    myButton.Content = "Start Write";
                }
            }
            catch (Exception ex)
            {
                ContentDialog dialog = new ContentDialog();

                dialog.XamlRoot = MainPage.instance.XamlRoot;
                dialog.Title = ex.Message;
                dialog.PrimaryButtonText = "Ok";
                dialog.DefaultButton = ContentDialogButton.Primary;
                dialog.Content = ex.StackTrace;

                dialog.ShowAsync();
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
        private void WritePort()
        {
            serialPort.Open();

            while (true)
            {
                if (serialPort.IsOpen)
                {
                    byte data = 0;
                    if (ass)
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

        private void toggle_Click(object sender, RoutedEventArgs e)
        {
            ass = (bool)(sender as ToggleButton).IsChecked;
        }
    }
}