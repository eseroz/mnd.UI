using System;
using System.IO.Ports;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;

namespace mnd.UI.Modules.PaketlemeModule
{
    public class KantarSerialPort
    {
        public SerialPort s_port;

        public event Action<string> pandap_dataReceiveEvent;

        public KantarSerialPort(String port, string baudrate, string parity, string databits, string stopbits)
        {
            String _port = port;
            int _baudrate = Convert.ToInt32(baudrate);
            Parity _parity = (Parity)Enum.Parse(typeof(Parity), parity);
            int _databits = Convert.ToInt32(databits);
            StopBits _stopbits = (StopBits)Enum.Parse(typeof(StopBits), stopbits);

            serialport_connect(_port, _baudrate, _parity, _databits, _stopbits);
        }

        private void serialport_connect(String port, int baudrate, Parity parity, int databits, StopBits stopbits)
        {
            DateTime dt = DateTime.Now;
            String dtn = dt.ToShortTimeString();

            s_port = new SerialPort(port, baudrate, parity, databits, stopbits);
            try
            {
                s_port.Open();
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString(), "Error"); }
        }

        string tonaj = string.Empty;

        public void dinlemeyiBaslat()
        {
            s_port.DataReceived += sport_DataReceived;
        }

        public void dinlemeyiDurdur()
        {
            s_port.DataReceived -= sport_DataReceived;
        }


        string textTonaj = "";

        private void sport_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Thread.Sleep(300);

            int bytes = s_port.BytesToRead;
            byte[] data = new byte[bytes];
            s_port.Read(data, 0, bytes);


            string tonaj = string.Empty;

            if (data.Length > 7)
            {
                for (int i = data.Length - 8; i < data.Length - 1; i++)
                {
                    tonaj += Convert.ToChar(data[i]).ToString();
                }
            }
            else
            {
                for (int i = 1; i < data.Length; i++)
                {
                    tonaj += Convert.ToChar(data[i]).ToString();
                }
            }

            tonaj = GET_KantarValue(tonaj);

            if (tonaj.Trim() != textTonaj.Trim())
            {
                textTonaj = tonaj.Trim();
            }


            if (pandap_dataReceiveEvent != null)
                pandap_dataReceiveEvent(textTonaj);

        }

        public void MesajGonder(string data)
        {
            s_port.Write(data);
        }

        private void PortKapat()
        {
            if (s_port.IsOpen)
            {
                s_port.Close();
                MessageBox.Show("Port Kapandı");
            }
        }

        private string GET_KantarValue(string input)
        {
            string str = input;

            StringBuilder convert = new StringBuilder();

            string pattern = @"\d+";
            Regex regex = new Regex(pattern);

            MatchCollection matches = regex.Matches(str);

            foreach (Match match in matches)
            {
                convert.Append(match.Groups[0].ToString());
            }

            return convert.ToString();
        }


    }
}
