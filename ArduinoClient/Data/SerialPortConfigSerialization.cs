using Microsoft.UI.Xaml.Controls;
using Newtonsoft.Json;
using System;
using System.IO;

namespace ArduinoClient.Data
{
    public static class SerialPortConfigSerialization
    {
        private static string directory = Windows.Storage.ApplicationData.Current.LocalFolder.Path;
        public static readonly string JsonDataPath = Path.Combine(directory, "SerialPortConfig.json");

        public static void SaveConfig(SerialPortConfig portConfig)
        {
            string jsonPresentation = JsonConvert.SerializeObject(portConfig);

            File.WriteAllText(JsonDataPath, jsonPresentation);
        }

        public static SerialPortConfig TryRestoreConfig()
        {
            try
            {
                if (File.Exists(JsonDataPath))
                {
                    string jsonPresentation = File.ReadAllText(JsonDataPath);

                    return SerialPortConfig.SetNewConfig(JsonConvert.DeserializeObject<SerialPortConfig>(jsonPresentation));
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

            return SerialPortConfig.InitSerialPortConfig();
        }
    }
}