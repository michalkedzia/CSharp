using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Newtonsoft.Json;

namespace Lab1
{
    public class WeatherStation
    {
        public const string WeatherStationIp = "127.0.0.1";
        public const int WeatherStationPort = 8080;
        private List<Sensor> _sensors = new();
        [JsonPropertyAttribute("Weather Station measurements")]
        private List<Measurement> _measurements = new();

        private WeatherStationServer _weatherStationServer;

        public void ReadMeasurement(object sender, Measurement e)
        {
            foreach (var (key, value) in e.Measurements)
            {
                Console.WriteLine(key + " :" + value);
            }
        }

        public void AddSensor(Sensor sensor)
        {
            _sensors.Add(sensor);
        }

        public void ReadAllSensors()
        {
            foreach (var sensor in _sensors)
            {
                Console.WriteLine(sensor.ToString());
            }
        }

        public void ReadSensorsImplementingGivenInterface<T>()
        {
            foreach (var sensor in _sensors)
            {
                if (sensor is T)
                {
                    Console.WriteLine(sensor.ToString());
                }
            }
        }

        public List<Sensor> GetItems(Func<Sensor, bool> typePredicate, Func<Sensor, bool> conditionPredicate)
        {
            return _sensors.Where(typePredicate).Where(conditionPredicate).ToList();
        }
        
        public void StopServer()
        {
            _weatherStationServer.StopServer();
        }

        public void StartServer()
        {
            _weatherStationServer = new WeatherStationServer(_measurements);
            _weatherStationServer.StartServer();
        }

        // ************************************************************
        // ************************************************************
        private class WeatherStationServer
        {
            private List<Measurement> _measurements;
            private Thread _weatherStationThread;
            private Thread _writeToFileThread;
            private bool _isStoped = false;
            
            public WeatherStationServer(List<Measurement> list)
            {
                this._measurements = list;
            }
            
            public void WeatherReport()
            {
                while (! _isStoped)
                {
                    Thread.Sleep(1000 * 2);
                    File.AppendAllText("C:\\Users\\Michal\\RiderProjects\\PNET\\Lab1\\WeatherReport.json",
                        JsonConvert.SerializeObject(_measurements) + Environment.NewLine);
                }
            }

            public void StopServer()
            {
                _isStoped = true;
                _weatherStationThread.Interrupt();
                _weatherStationThread.Interrupt();
                
            }

            public void StartServer()
            {
                _weatherStationThread = new Thread(StartListener);
                _writeToFileThread = new Thread(WeatherReport);
                _weatherStationThread.Start();
                _writeToFileThread.Start();
            }

            private void StartListener()
            {
                TcpListener listener = null;
                try
                {
                    listener = new TcpListener(IPAddress.Parse(WeatherStationIp), WeatherStationPort);
                    listener.Start();
                    while (! _isStoped)
                    {
                        if (!listener.Pending())
                        {
                            Thread.Sleep(500);
                            continue;
                        }

                        TcpClient client = listener.AcceptTcpClient();
                        Thread t = new Thread(ProcessSensorRequests);
                        t.Start(client);
                    }
                }
                catch (ThreadInterruptedException e)
                {
                    Console.WriteLine("*** Server stop ***");
                }
                catch (Exception e)
                {
                    Console.WriteLine("** Unknown Exception ***");
                }
                finally
                {
                    listener?.Stop();
                }
            }

            private void ProcessSensorRequests(object argument)
            {
                TcpClient sensor = (TcpClient) argument;
                try
                {
                    StreamReader reader = new StreamReader(sensor.GetStream());
                    string json = reader.ReadLine();
                    try
                    {
                        Measurement measurement = JsonConvert.DeserializeObject<Measurement>(json);
                        _measurements.Add(measurement);
                    }
                    catch (JsonReaderException e)
                    {
                        Console.WriteLine("** Incorrect data ***");
                    }
                    reader.Close();
                    sensor.Close();
                }
                catch (IOException)
                {
                    Console.WriteLine("Problem with client communication. Exiting thread.");
                }
                finally
                {
                    if (sensor != null)
                    {
                        sensor.Close();
                    }
                }
            }
        }
    }
}