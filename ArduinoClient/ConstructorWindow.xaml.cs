using Microsoft.UI.Xaml;

namespace ArduinoClient
{
    public sealed partial class ConstructorWindow : Window
    {
        public bool IsActivated { get; private set; }


        public ConstructorWindow()
        {
            InitializeComponent();

            VisibilityChanged += (x, y) =>
            {
                IsActivated = true;
            };

            Closed += (x, y) =>
            {
                IsActivated = false;
            };
        }
    }
}