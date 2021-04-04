using System;

namespace Lab1
{
    public interface ITemperature
    {
        string Degrees { get; set; }
        double Temperature { get; }
    }
}