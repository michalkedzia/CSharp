using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text.Json;
using System.Threading;

namespace Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            // WeatherStation weatherStation = new WeatherStation();
            // weatherStation.AddSensor(new TemperatureSensor());
            // weatherStation.AddSensor(new PressureSensor());
            // weatherStation.AddSensor(new HumiditySensor());
            // weatherStation.AddSensor(new TemperatureHumiditySensor());
            // weatherStation.AddSensor(new HumiditySensor());
            // weatherStation.AddSensor(new TemperatureHumiditySensor());
            // weatherStation.AddSensor(new TemperatureSensor());
            // weatherStation.AddSensor(new PressureSensor());
            // weatherStation.AddSensor(new HumiditySensor());
            // weatherStation.AddSensor(new TemperatureHumiditySensor());
            // weatherStation.AddSensor(new TemperatureSensor());
            //
            // weatherStation.ReadAllSensors();
            // weatherStation.ReadSensorsImplementingGivenInterface<ITemperature>();
            //
            //
            // Func<Sensor, bool> test1 = s => ((s is ITemperature) && (s is IHumidity));
            // Func<Sensor, bool> test2 = s => ((ITemperature) s).Temperature != 0;
            //
            // List<Sensor> l = weatherStation.GetItems(test1, test2);
            // foreach (var sensor in l)
            // {
            //     Console.WriteLine(sensor.ToString());
            // }
            //
            // weatherStation.WeatherReport();


            // ProcessBusinessLogic bl = new ProcessBusinessLogic();
            // bl.ProcessCompleted += bl_ProcessCompleted; // register with an event
            // bl.StartProcess();


            WeatherStation weatherStationA = new WeatherStation();
            WeatherStation weatherStationB = new WeatherStation();

            var humiditySensor = new HumiditySensor();
            var pressureSensor = new PressureSensor();
            var temperatureSensor = new TemperatureSensor();
            var temperatureHumiditySensor = new TemperatureHumiditySensor();

            humiditySensor.MeasurementEvent += weatherStationA.ReadMeasurement;
            pressureSensor.MeasurementEvent += weatherStationA.ReadMeasurement;
            temperatureSensor.MeasurementEvent += weatherStationA.ReadMeasurement;
            temperatureHumiditySensor.MeasurementEvent += weatherStationA.ReadMeasurement;

            humiditySensor.MeasurementEvent += weatherStationB.ReadMeasurement;
            pressureSensor.MeasurementEvent += weatherStationB.ReadMeasurement;
            temperatureSensor.MeasurementEvent += weatherStationB.ReadMeasurement;
            temperatureHumiditySensor.MeasurementEvent += weatherStationB.ReadMeasurement;

            humiditySensor.TakeMeasurement();
            pressureSensor.TakeMeasurement();
            temperatureSensor.TakeMeasurement();
            temperatureHumiditySensor.TakeMeasurement();
        }
    }
}