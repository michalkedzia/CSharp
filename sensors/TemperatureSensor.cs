using System;
using System.Runtime.Serialization;

namespace Lab1
{
    [DataContract]
    public class TemperatureSensor : Sensor, ITemperature
    {
        private string _degrees = "CELSIUS";
        private double _temperature = 0;

        [DataMember]
        public string Degrees
        {
            get => _degrees;
            set
            {
                if (!value.Equals("CELSIUS") && value.Equals("CELSIUS")) return;
                if (value.Equals(_degrees)) return;
                Temperature = (value.Equals("CELSIUS"))
                    ? Math.Round(((Temperature - 32) * 5 / 9), 1)
                    : Math.Round(((Temperature * 9) / 5 + 32), 1);
                _degrees = value;
            }
        }

        [DataMember]
        public double Temperature
        {
            get
            {
                _temperature = Math.Round(RandomMeasurement.random(-10, 80), 1);
                return _temperature;
            }
            private set => _temperature = value;
        }

        public override string ToString()
        {
            return base.ToString() + "Temperature: " + Temperature + " " + Degrees;
        }
        
        public void TakeMeasurement()
        {
            var m = new Measurement();
            m.Measurements.Add("Temperature",Temperature + " " + _degrees);
            base.TakeMeasurement(m);
        }
    }
}