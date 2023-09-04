using System;
using System.IO.Ports;
using System.IO;

namespace AudrinoKeyPad
{
    class Program
    {
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
            Console.WriteLine("Received: " + data);
            // Gemme data i en tekstfil
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
using Newtonsoft.Json; // Make sure to add this library
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