using System;
using RouteMath;
using MapBoxWrapper;

class Program
{
    static void Main(string[] args)
    {
<<<<<<< HEAD
        mapBox newBox = new mapBox(5, new Tuple<double, double> (-82.334380, 29.653420), new Tuple<double, double> (-82.323640, 29.650510));
=======
        MapBox newBox = new MapBox(5, new Tuple<double, double> (-75, 40), new Tuple<double, double> (-74, 40));
>>>>>>> 236cd377ddd508940c0d36bae4f50c2e1c0518f6

        RouteGeneration.routePath(newBox);
    }
}