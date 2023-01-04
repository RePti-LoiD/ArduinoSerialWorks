using Microsoft.UI.Xaml.Controls;

namespace ArduinoClient
{
    public sealed partial class MainPage : Page
    {
        public static MainPage instance;

        public MainPage()
        {
            InitializeComponent();

            Loaded += (x, y) => instance = this;
        }
    }
}