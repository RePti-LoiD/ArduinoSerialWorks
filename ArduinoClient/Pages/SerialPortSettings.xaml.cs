using ArduinoClient.Data;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using Windows.ApplicationModel.DataTransfer;

namespace ArduinoClient.Pages
{
    public sealed partial class SerialPortSettings : Page
    {
        private SerialPortConfig portConfig;

        public SerialPortSettings()
        {
            InitializeComponent();

            Loaded += PageLoaded;
        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            SetValues();
        }

        private void SetValues()
        {
            portConfig = MainWindow.PortConfig;

            PortNames.ItemsSource = SerialPort.GetPortNames().ToList() ?? new List<string>() { "None" };
            PortNames.SelectedIndex = 0;

            BaudRate.PlaceholderText = portConfig.BaudRate.ToString();

            ParityComboBox.ItemsSource = Enum.GetValues(typeof(Parity));
            ParityComboBox.SelectedItem = portConfig.Parity;

            DataBits.PlaceholderText = portConfig.DataBits.ToString();

            HandshakeComboBox.ItemsSource = Enum.GetValues(typeof(Handshake));
            HandshakeComboBox.SelectedItem = portConfig.Handshake;
        }

        private void SaveButtonClicked(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            SerialPortConfigHandler.SaveConfig(portConfig);

            DataPackage dataPackage = new DataPackage();
            dataPackage.SetText(SerialPortConfigHandler.JsonDataPath);

            Clipboard.SetContent(dataPackage);
        }

        private void TextBox_TextChanged(object sender, Microsoft.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter && sender is TextBox textBox)
                switch (textBox.Name)
                {
                    case nameof(BaudRate):
                        portConfig.BaudRate = int.Parse(textBox.Text);
                        break;

                    case nameof(DataBits):
                        portConfig.DataBits = int.Parse(textBox.Text);
                        break;
                }
        }

        private void ParityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            portConfig.Parity = (Parity)(sender as ComboBox).SelectedItem;
        }

        private void HandshakeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            portConfig.Handshake = (Handshake)(sender as ComboBox).SelectedItem;
        }

        private void PortName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            portConfig.PortName = (sender as ComboBox).SelectedItem.ToString();
        }

        private void UpdateButtonClick(object sender, RoutedEventArgs e)
        {
            SetValues();
        }
    }
}