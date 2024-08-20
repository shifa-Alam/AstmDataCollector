using System;
using System.IO;
using System.IO.Ports;
namespace Astm.PortListener
{

    public class PortListtener : IportListener
    {

        private string qPath = "c:\\temp\\cobas_e411_Q.txt";
        private string rPath = "c:\\temp\\cobas_e411_R.txt";
        private string errorPath = "c:\\temp\\cobas_e411_E.txt";
        private string folderPath = "c:\\temp";
        private string allData = "";
        private string allDataNew = "";

        private byte[] ACK = new byte[] { 0x06 };
        private byte[] ENQ = new byte[] { 0x05 };
        private byte[] EOT = new byte[] { 0x04 };



        SerialPort serialPort = new SerialPort();




        public PortListtener()
        {


            serialPort.PortName = "COM3";
            serialPort.BaudRate = 9600;
            serialPort.Parity = Parity.None;
            serialPort.StopBits = StopBits.One;
            serialPort.DataBits = 8;
            serialPort.Handshake = Handshake.None;
            serialPort.RtsEnable = true;

            serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandlerNew);

            if (!Directory.Exists(folderPath))
            {
                DirectoryInfo folder = Directory.CreateDirectory(folderPath);
            }

        }
        public void PortOpen()
        {

            serialPort.Open();
            //ReadFromPortNew();
        }
        public void PortClose()
        {
            //gdSerialPort.Close();
            serialPort.Close();
        }
        public string ReadFromFile()
        {
            string st = "";
            using (var sr = new StreamReader(qPath))
            {
                st = sr.ReadToEnd();
            }
            return st;
        }
        public string ReadFromFileNew()
        {
            string st = "";
            using (var sr = new StreamReader(rPath))
            {
                st = sr.ReadToEnd();
            }
            return st;
        }
        public void WriteToFile(string data)
        {
            if (!string.IsNullOrEmpty(data))
            {
                //File.AppendText for append
                using (StreamWriter writer = File.CreateText(rPath))
                {
                    writer.WriteLine(data);
                }
                //allData = "";
            }
        }


        public void WriteToFileNew(string txt, bool isException = true)
        {
            if (!string.IsNullOrEmpty(txt))
            {
                //File.AppendText for append
                using (StreamWriter writer = File.CreateText(errorPath))
                {
                    writer.WriteLine(txt);
                }
                //allData = "";
            }
            else
            {
                //File.AppendText for append
                using (StreamWriter writer = File.CreateText(rPath))
                {
                    writer.WriteLine(txt);
                }
                //allData = "";
            }
        }

        private void DataReceivedHandlerNew(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                SerialPort sp = (SerialPort)sender;
                string indata = sp.ReadExisting();

                if (indata.IndexOf('\u0004') == -1 /*&& (indata.IndexOf('\u0004') == -1) && (indata.IndexOf('\u0005') == -1)*/)
                {
                    allDataNew = allDataNew + indata;
                    Console.WriteLine("outside : " + allDataNew + "\r\n");
                    sp.Write(ACK, 0, 1);
                }
                else if (!String.IsNullOrEmpty(allDataNew))
                {
                    WriteToFileNew(allDataNew.ToString());
                }
            }
            catch (Exception ex)
            {
                WriteToFileNew(ex.ToString(), true);
            }
            finally
            {
                if (!serialPort.IsOpen)
                {
                    serialPort.Open();
                }

            }

        }

    }
}
