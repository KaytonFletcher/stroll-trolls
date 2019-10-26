using System;
using MapBoxWrapper;

namespace RouteMath
{
    class Program
    {
        static void Main(string[] args)
        {

            mapBox newBox = new mapBox(5.0, new Tuple<double, double>(-73.98,43.7), new Tuple<double, double>(-74, 40.733));

            double realDistance = newBox.getRealDistance().Result;

            Console.WriteLine("Hello World!");
        }
    }
}