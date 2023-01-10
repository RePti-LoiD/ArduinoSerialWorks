using System;

namespace ArduinoClient.Pages
{
    public class SheduleData
    {
        private TimeSpan startTime;
        private TimeSpan endTime;

        public int LessonNumber { get; set; }

        public TimeSpan StartTime { get => startTime; set => startTime = value; }
        public TimeSpan EndTime { get => endTime; set => endTime = value; }
    }
}