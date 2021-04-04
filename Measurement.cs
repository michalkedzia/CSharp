using System;
using System.Collections.Generic;

namespace Lab1
{
    public class Measurement : EventArgs
    {
        public Dictionary<string, string> Measurements = new();
    }
}