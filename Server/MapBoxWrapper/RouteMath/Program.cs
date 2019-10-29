using System;
using System.Collections.Generic;
using RouteMath;
using MapBoxWrapper;

class Program
{
    static void Main(string[] args)
    {
        MapBox newBox = new MapBox(6, new Tuple<double, double> (-82.334380, 29.653420), new Tuple<double, double> (-82.323640, 29.650510));

        //List<Tuple<double, double>> newList = new List<Tuple<double, double>>{newBox.startPoint, newBox.endPoint, 
         //   new Tuple<double, double>(-83,29.7), new Tuple<double, double>(-83.2,29.8)};

        var newList = RouteGeneration.generateOtherPoints(newBox);
         
        var both = newBox.generateMatrix(newList).Result;
        var matrix = both.Item1;
        
        //RouteGeneration.routePath(newBox);

        var distances = RouteGeneration.getDistancesAndPoints(matrix, newBox.getDistance(), 0.5);

        int closeCounter = 0;
        
        foreach (var aids in distances) {

            var distance = aids.Item1;

            if (Math.Abs(6 - (distance * 0.000621371)) <= 0.5) {
                closeCounter++;
                
                
                Console.WriteLine($"Distance: {aids.Item1}, \nConnections:");
                foreach (var point in aids.Item2) {
                    Console.Write($"({point.Item1}, {point.Item2})");
                }
                Console.WriteLine();
                
                
            }
           
        }
        Console.WriteLine(closeCounter);
        
    }
}