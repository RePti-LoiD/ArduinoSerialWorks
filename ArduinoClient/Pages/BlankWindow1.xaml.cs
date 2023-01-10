using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace ArduinoClient.Pages
{
    public sealed partial class BlankWindow1 : Window
    {
        public TextBlock debug;

        public BlankWindow1()
        {
            InitializeComponent();

            Activated += (x, y) =>
            {
                debug = Ass;
            };
        }
    }
}