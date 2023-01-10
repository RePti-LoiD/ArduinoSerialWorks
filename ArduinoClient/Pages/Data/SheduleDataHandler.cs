using ArduinoClient.Pages.SheduleConstructor;
using System.Collections.Generic;

namespace ArduinoClient.Pages.Data
{
    public class SheduleDataHandler
    {
        private static SheduleDataHandler instance;

        private List<SheduleCtor> sheduleCtors = new List<SheduleCtor>();

        public List<SheduleCtor> SheduleCtors
        {
            get => sheduleCtors;
            set => sheduleCtors = value;
        }

        public SheduleDataHandler Init()
        {
            instance ??= new SheduleDataHandler();

            return instance;
        }
    }
}