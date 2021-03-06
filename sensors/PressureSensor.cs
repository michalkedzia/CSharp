using System;
using System.Runtime.Serialization;

namespace Lab1
{
    public class PressureSensor : Sensor, IPressure
    {
        private int _pressure;
        
        public int Pressure
        {
            get
            {
                _pressure = (int) RandomMeasurement.random(800, 1000);
                return _pressure;
            }
            private set => _pressure = value;
        }

        public override string ToString()
        {
            return base.ToString() + "Pressure: " + Pressure + " hPa";
        }
        
        public void TakeMeasurement()
        {
            var m = new Measurement();
            m.Measurements.Add("Sensor name:",Name);
            m.Measurements.Add("Pressure",Pressure + " " + "hpa");
            base.TakeMeasurement(m);
        }
    }
}