using System;
using Microsoft.UI.Xaml.Controls;

namespace ArduinoClient.Pages
{
    public sealed partial class ConstructorNode : UserControl
    {
        private SheduleData sheduleData = new SheduleData();

        public int NodeIndex { get => sheduleData.LessonNumber; set => sheduleData.LessonNumber = value; }

        public ConstructorNode()
        {
            InitializeComponent();
        }

        public ConstructorNode(SheduleData sheduleData)
        {
            InitializeComponent();

            Loaded += (x, y) =>
            {
                this.sheduleData = sheduleData;
                NodeIndex = sheduleData.LessonNumber;

                StartTime.Time = this.sheduleData.StartTime;
                EndTime.Time = this.sheduleData.EndTime;
            };
        }

        private void StartTimeChanged(TimePicker sender, TimePickerSelectedValueChangedEventArgs args)
        {
            sheduleData.StartTime = (TimeSpan)args.NewTime;
        }

        private void EndTimeChanged(TimePicker sender, TimePickerSelectedValueChangedEventArgs args)
        {
            sheduleData.EndTime = (TimeSpan)args.NewTime;
        }
    }
}