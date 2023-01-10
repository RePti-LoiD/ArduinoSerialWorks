using Microsoft.UI.Xaml;

namespace ArduinoClient
{
    public partial class App : Application
    {
        public SettingsWindow settingsWindow;
        public ConstructorWindow constructorWindow;

        public App()
        {
            InitializeComponent();
        }
        
        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            settingsWindow = new SettingsWindow();
            constructorWindow = new ConstructorWindow();

            settingsWindow.Activate();
        }

        public void ActivateSheduleConstructor()
        {
            constructorWindow.Activate();
        }

        public void DeactivateSheduleConstructor()
        {
            constructorWindow.Close();
        }
    }
}