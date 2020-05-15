using System;

namespace Jrpg.System
{
    public class RandomDouble
    {
        public static double Get(double min, double max)
        {
            return (new Random((int)DateTime.Now.Ticks).NextDouble() * (max - min)) + min;
        }
    }
}
