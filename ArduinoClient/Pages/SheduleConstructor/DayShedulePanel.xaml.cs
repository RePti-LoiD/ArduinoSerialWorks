using ArduinoClient.Pages.Data;
using Microsoft.UI.Xaml.Controls;
using System.Text;

namespace ArduinoClient.Pages.SheduleConstructor
{
    public sealed partial class DayShedulePanel : UserControl
    {
        private string dayName = string.Empty;

        private SheduleDayData sheduleDayData = new SheduleDayData();
        
        public string DayName { get => dayName; set => dayName = value; }

        public DayShedulePanel()
        {
            InitializeComponent();

            Loaded += (x, y) =>
            {
                sheduleDayData = sheduleDayData.InitSheduleDayData(DayName);

                foreach (SheduleData item in sheduleDayData.DayData)
                {
                    Container.Children.Add(new ConstructorNode(item));
                }
            };
        }

        public string GetDayShedule()
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (SheduleData item in sheduleDayData.DayData)
            {
                stringBuilder.Append($"{(item.EndTime - item.StartTime).TotalSeconds.ToString()},");
            }

            return stringBuilder.ToString();
        }
    }
}