using System.IO.Ports;

namespace ArduinoClient.Data
{
    public class SerialPortConfig
    {
        private static SerialPortConfig instance;

        private string portName = "COM 3";
        private int baudRate = 9600;
        private Parity parity = Parity.Space;
        private int dataBits = 8;
        private Handshake handshake = Handshake.XOnXOff;

        public string PortName { get => portName; set => portName = value; }
        public int BaudRate { get => baudRate; set => baudRate = value; }
        public Parity Parity { get => parity; set => parity = value; }
        public int DataBits { get => dataBits; set => dataBits = value; }
        public Handshake Handshake { get => handshake; set => handshake = value; }

        public static SerialPortConfig InitSerialPortConfig()
        {
            instance ??= new SerialPortConfig();

            return instance;
        }

        public static SerialPortConfig SetNewConfig(SerialPortConfig serialPortConfig)
        {
            if (instance == null)
            {
                instance = new SerialPortConfig();
            }

            instance.PortName = serialPortConfig.PortName;
            instance.BaudRate = serialPortConfig.BaudRate;
            instance.Parity = serialPortConfig.Parity;
            instance.DataBits = serialPortConfig.DataBits;
            instance.Handshake = serialPortConfig.Handshake;

            return instance;
        }

        public SerialPort BuildSerialPort() =>  new SerialPort()
                                                {
                                                    PortName = PortName,
                                                    BaudRate = BaudRate,
                                                    Parity = Parity,
                                                    DataBits = DataBits,
                                                    Handshake = Handshake
                                                };

        private SerialPortConfig() { }
    }
}