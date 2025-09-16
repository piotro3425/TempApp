using System.Globalization;
using System.IO.Ports;

namespace TempApp.Services
{
    public class TempProvider : ITempProvider
    {
        SerialPort port;
        private double currentTemp = -100d;

        public TempProvider()
        {
            port = new SerialPort("COM3", 9600, Parity.None, 8, StopBits.One);
            port.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);

            try
            {
                port.Open();
            }
            catch (Exception)
            {
                this.currentTemp = -101d;
            }
        }
        ~TempProvider()
        {
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

        public double GetTemp() => this.currentTemp;
    }
}
