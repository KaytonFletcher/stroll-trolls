using MapBoxWrapper;
using System;
using RouteMath;

class Program
{
    static void Main(string[] args)
    {
        mapBox newBox = new mapBox(5, new Tuple<double, double> (-75, 40), new Tuple<double, double> (-74, 40));

        RouteGeneration.routePath(newBox);
    }
}