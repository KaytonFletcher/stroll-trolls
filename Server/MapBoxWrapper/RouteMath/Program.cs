using System;
using RouteMath;
using MapBoxWrapper;

class Program
{
    static void Main(string[] args)
    {
        MapBox newBox = new MapBox(5, new Tuple<double, double> (-75, 40), new Tuple<double, double> (-74, 40));

        RouteGeneration.routePath(newBox);
    }
}