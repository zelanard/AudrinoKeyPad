using System;
using System.IO.Ports;
using System.IO;
using System.Threading;

namespace AudrinoKeyPad
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create and start a thread for SerialPortListener
            Thread serialPortThread = new Thread(SerialPortListener);
            serialPortThread.Start();

            // Create and start a thread for WebServer
            Thread webServerThread = new Thread(() =>
            {
                WebServer webServer = new WebServer();
                webServer.Host();
            });
            webServerThread.Start();
        }

        /// <summary>
        /// This method listens to the specified port for input.
        /// </summary>
        private static void SerialPortListener()
        {
            try
            {
                SerialPort serialPort = new SerialPort("COM8", 9600);
                serialPort.DataReceived += new SerialDataReceivedEventHandler(SerialPortDataReceived);
                serialPort.Open();
                Console.WriteLine("Listening on " + serialPort.PortName);
                Console.ReadKey();
                serialPort.Close();
            }
            catch (UnauthorizedAccessException ex)
            {
                //if COM8 is busy, write a message to the console, wait, and try again.
                Console.WriteLine(ex.Message);
                Thread.Sleep(5000);
                SerialPortListener();
            }

        }

        /// <summary>
        /// Output data to textfile, when data is recieved through the COM8 port.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void SerialPortDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string data = sp.ReadLine();
            Console.WriteLine("Received: " + data);
            // Gem data i en tekstfil
            using (StreamWriter sw = new StreamWriter("output.html", true))
            {
                sw.Write(data);
                if (data != "Password Reset")
                {
                    sw.WriteLine("<br>");
                }
            }
        }
    }
}




/*
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArduinoKeyPad
{
    class Program
    {
        private static List<string> dataArray = new List<string>(); // To store received data

        static void Main(string[] args)
        {
            SerialPort serialPort = new SerialPort("COM8", 9600);
            serialPort.DataReceived += new SerialDataReceivedEventHandler(SerialPortDataReceived);
            serialPort.Open();
            Console.WriteLine("Listening on " + serialPort.PortName);
            Console.ReadKey();
            serialPort.Close();
        }

        private static void SerialPortDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string data = sp.ReadLine();
            //Console.WriteLine("Received: " + data);

            // Add the received data to the array
            dataArray.Add(data);


            // Create a JSON object with a named array
            var jsonObject = new
            {
                data = dataArray
            };

            // Serialize and save the JSON object to a file
            SerializeAndSaveToJson(jsonObject, "output.json");

        }

        private static void SerializeAndSaveToJson(object dataObject, string fileName)
        {
            // Serialize the data object to JSON format
            string jsonData = JsonConvert.SerializeObject(dataObject);

            // Write the JSON data to the file (overwrite the file if it exists)
            File.WriteAllText(fileName, jsonData);
        }
    }
}
*/