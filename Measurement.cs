using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Lab1
{

    public class Measurement
    {
        public Dictionary<string, string> Measurements { get; private set; }

        public Measurement()
        {
            Measurements = new Dictionary<string, string>();
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            foreach (var keyValuePair in Measurements)
            {
                result.Append(keyValuePair.Key + ": ");
                result.Append(keyValuePair.Value + " ");
            }

            return result.ToString();
        }
    }
}