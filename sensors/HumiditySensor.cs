using System;
using System.Runtime.Serialization;
using Lab1;

namespace Lab1
{
    public class HumiditySensor : Sensor, IHumidity
    {
        private int _humidity;
        
        public int Humidity
        {
            get
            {
                _humidity = (int) RandomMeasurement.random(20, 100);
                return _humidity;
            }
            private set => _humidity = value;
        }

        public override string ToString()
        {
            return base.ToString() + "Humidity: " + Humidity + " %";
        }

        public void TakeMeasurement()
        {
            var m = new Measurement();
            m.Measurements.Add("Sensor name:",Name);
            m.Measurements.Add("Humidity",Humidity + "%");
            base.TakeMeasurement(m);
        }
    }
}