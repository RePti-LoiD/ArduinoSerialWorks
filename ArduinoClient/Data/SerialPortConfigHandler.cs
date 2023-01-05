using Microsoft.UI.Xaml.Controls;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ArduinoClient.Data
{
    public static class SerialPortConfigHandler
    {
        private static string directory = Windows.Storage.ApplicationData.Current.LocalFolder.Path;
        public static readonly string JsonDataPath = Path.Combine(directory, "SerialPortConfig.json");


        public delegate void ConfigChanged(SerialPortConfig port);
        public static event ConfigChanged OnConfigChanged;

        private static SerialPortConfig instance;

        public static SerialPortConfig Instance
        {
            get
            {
                instance ??= new SerialPortConfig();

                return instance;
            }
            set
            {
                instance = value;

                OnConfigChanged?.Invoke(instance);
            }
        }

        public static void SaveConfig(SerialPortConfig portConfig)
        {
            string jsonPresentation = JsonConvert.SerializeObject(portConfig);

            File.WriteAllText(JsonDataPath, jsonPresentation);

            Instance = portConfig;
        }

        public static SerialPortConfig TryRestoreConfig()
        {
            try
            {
                if (File.Exists(JsonDataPath))
                {
                    string jsonPresentation = File.ReadAllText(JsonDataPath);
                    Instance = JsonConvert.DeserializeObject<SerialPortConfig>(jsonPresentation);

                    return Instance;
                }
            }
            catch (Exception ex)
            {
                ContentDialog dialog = new ContentDialog
                {
                    XamlRoot = MainPage.instance.XamlRoot,
                    Title = ex.Message,
                    PrimaryButtonText = "Ok",
                    DefaultButton = ContentDialogButton.Primary,
                    Content = ex.StackTrace
                };

                StartDialogAsync(dialog);
            }

            return Instance = new SerialPortConfig();
        }

        public static async void StartDialogAsync(ContentDialog contentDialog)
        {
            await Task.Run(() => contentDialog.ShowAsync());
        }
    }
}