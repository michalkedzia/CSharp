using System;
using System.Runtime.Serialization;


namespace Lab1
{
    [DataContract]
    public class Sensor
    {
        private string _name;
        private static int _counter = 1;
        public event EventHandler<Measurement> MeasurementEvent; 

        [DataMember]
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

        protected void TakeMeasurement(Measurement m)
        {
            MeasurementEvent?.Invoke(this, m);
        }
    }
}