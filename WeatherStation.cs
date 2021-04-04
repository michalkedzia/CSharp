using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text.Json;

namespace Lab1
{
    [DataContract]
    [KnownType(typeof(PressureSensor))]
    [KnownType(typeof(TemperatureSensor))]
    [KnownType(typeof(TemperatureHumiditySensor))]
    [KnownType(typeof(HumiditySensor))]
    public class WeatherStation
    {
        [DataMember] private List<Sensor> _sensors = new();
        
        public void ReadMeasurement(object sender, Measurement e)
        {
            foreach (var (key, value)  in e.Measurements)  
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
        
        public void WeatherReport()
        {
            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(WeatherStation));
            MemoryStream stream = new MemoryStream();
            jsonSerializer.WriteObject(stream, this);
            stream.Position = 0;
            var sr = new StreamReader(stream);
            string json = sr.ReadToEnd();
            stream.Close();
            File.AppendAllText("C:\\Users\\Michal\\RiderProjects\\PNET\\Lab1\\WeatherReport.json", json + Environment.NewLine);
        }
    }
}