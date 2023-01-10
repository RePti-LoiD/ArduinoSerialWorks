using ArduinoClient.Data;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
using System.Threading.Tasks;

namespace ArduinoClient.Pages.SheduleConstructor
{
    public sealed partial class SheduleCtor : Page
    {
        private List<DayShedulePanel> dayShedulePanels = new List<DayShedulePanel>();
        private SerialPort serialPort;


        public SheduleCtor()
        {
            InitializeComponent();

            Loaded += (x, y) =>
            {
                PortStatus.Content = $"{SerialPortConfigHandler.Instance.PortName} Open";

                dayShedulePanels.Clear();
                foreach (UIElement item in Shedule.Children)
                {
                    dayShedulePanels.Add(item as DayShedulePanel);
                }

                serialPort = SettingsWindow.PortConfig.BuildSerialPort();
                SerialPortConfigHandler.OnConfigChanged += (x) => 
                {
                    serialPort.Close();

                    serialPort = x.BuildSerialPort();
                };
            };
        }

        private void BackButtonClicked(object sender, RoutedEventArgs e)
        {
            (Application.Current as App).DeactivateSheduleConstructor();
        }

        private async void SendShedule(object sender, RoutedEventArgs e)
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (DayShedulePanel item in dayShedulePanels)
            {
                stringBuilder.Append($"{item.GetDayShedule()}");
            }

            stringBuilder.Append(".");

            Send(stringBuilder.ToString());
            BlankWindow1 debugger = new BlankWindow1();
            debugger.Activate();

            debugger.debug.Text = stringBuilder.ToString();

        }

        private void Send(string data)
        {
            serialPort.Open();

            serialPort.Write(data);
            
            serialPort.Close();
        }
    }
}