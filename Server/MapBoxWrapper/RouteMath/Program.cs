using System;
using RouteMath;
using MapBoxWrapper;

class Program
{
    static void Main(string[] args)
    {
        mapBox newBox = new mapBox(5, new Tuple<double, double> (-82.334380, 29.653420), new Tuple<double, double> (-82.323640, 29.650510));

        RouteGeneration.routePath(newBox);
    }
}