using System.Globalization;
using System.IO.Ports;

namespace TempApp.Services
{
    public class TempProvider : ITempProvider
    {
        SerialPort port;
        private double? currentTemp = null;
        Random rand = new Random();

        public TempProvider()
        {
            string[] portNames = SerialPort.GetPortNames();
            if (portNames.Length > 0)
            {
                port = new SerialPort(portNames[0], 9600, Parity.None, 8, StopBits.One);
                port.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);

                try
                {
                    port.Open();
                }
                catch (Exception)
                {
                    this.currentTemp = null;
                }
            }
        }
        ~TempProvider()
        {
            if(port.IsOpen)
                port.Close();
        }


        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string indata = sp.ReadLine();
            string valStr = indata.Substring(7).Trim(); ;

            try
            {
                this.currentTemp = double.Parse(valStr, CultureInfo.InvariantCulture);
            }
            catch (FormatException)
            {
                this.currentTemp = -102d;
            }
        }

        public double GetTemp() => this.currentTemp ?? ((double)rand.Next(-2000, -1000)/10);
    }
}
