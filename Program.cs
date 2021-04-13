using System.Threading;


namespace Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            WeatherStation weatherStation = new WeatherStation();
            weatherStation.StartServer();

            var humiditySensor = new HumiditySensor();
            var pressureSensor = new PressureSensor();
            var temperatureSensor = new TemperatureSensor();
            var temperatureHumiditySensor = new TemperatureHumiditySensor();

            humiditySensor.TakeMeasurement();
            pressureSensor.TakeMeasurement();
            temperatureSensor.TakeMeasurement();
            temperatureHumiditySensor.TakeMeasurement();

            humiditySensor.TakeMeasurement();
            pressureSensor.TakeMeasurement();
            temperatureSensor.TakeMeasurement();
            temperatureHumiditySensor.TakeMeasurement();

            Thread.Sleep(5000);

            weatherStation.StopServer();
        }
    }
}