using System;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization;
using Newtonsoft.Json;


namespace Lab1
{
    public class Sensor
    {
        private string _name;
        private static int _counter = 1;
        
        public string Name
        {
            get => _name;
            set => _name = value.Length > 16 ? _name = value.Substring(0, 16) : _name = value;
        }
        public Sensor()
        {
            _name = "Sensor" + _counter;
            _counter++;
        }

        public override string ToString()
        {
            return "Sensor name: " + _name + " ";
        }

        protected virtual void TakeMeasurement(Measurement m)
        {
            try {
                TcpClient client = new TcpClient(WeatherStation.WeatherStationIp, WeatherStation.WeatherStationPort);
                StreamWriter writer = new StreamWriter(client.GetStream());
                string json = JsonConvert.SerializeObject(m);
                writer.WriteLine(json);
                writer.Flush();
                writer.Close();
                client.Close();
            } catch(Exception e){
                Console.WriteLine(e);
            }
        }
    }
}