using System;

namespace Lab1
{
    public class RandomMeasurement
    {
        public static double random(int min, int max)
        {
            Random random = new Random();
            return random.NextDouble() * (max - min) + min;
        }
    }
}