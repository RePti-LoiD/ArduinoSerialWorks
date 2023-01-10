using System.Collections.Generic;

namespace ArduinoClient.Pages.Data
{
    public class SheduleDayData
    {
        private string dayName = string.Empty;

        List<SheduleData> dayData = new List<SheduleData>();

        public string DayName { get => dayName; set => dayName = value; }
        public List<SheduleData> DayData { get => dayData; }

        public SheduleDayData InitSheduleDayData(string dayName)
        {
            this.dayName = dayName;

            return InitSheduleDayData();
        }

        public SheduleDayData InitSheduleDayData()
        {
            for (int i = 0; i < 12; i++)
            {
                dayData.Add(new SheduleData() { LessonNumber = i + 1 });
            }
            return this;
        }
    }
}