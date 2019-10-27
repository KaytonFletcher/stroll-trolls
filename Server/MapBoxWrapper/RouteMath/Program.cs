using System;
using System.Collections.Generic;
using RouteMath;
using MapBoxWrapper;

class Program
{
    static void Main(string[] args)
    {
        MapBox newBox = new MapBox(5, new Tuple<double, double> (-82.334380, 29.653420), new Tuple<double, double> (-82.323640, 29.650510));

        List<Tuple<double, double>> newList = new List<Tuple<double, double>>{newBox.startPoint, newBox.endPoint, 
            new Tuple<double, double>(-83,29.7), new Tuple<double, double>(-83.2,29.8)};
        
        var matrix = newBox.generateMatrix(newList).Result.Item1;
        
        //RouteGeneration.routePath(newBox);

        var distances = RouteGeneration.getAllDistances(matrix);
        
        
        
    }
}