using MapBoxWrapper;
using System;
using System.Collections.Generic;

namespace RouteMath {

	public class RouteGeneration {

        public static double GetRandomNumberInRange(double minNumber, double maxNumber)
        {
            return new Random().NextDouble() * (maxNumber - minNumber) + minNumber;
        }

        public static void routePath(MapBox myBox) {

            //basic info
            double desiredDistance = myBox.getDistance();
            Tuple<double, double> startPoint = myBox.getStartPoint();
            Tuple<double, double> endPoint = myBox.getEndPoint();

            //start at two to include starting and ending points
            int waypointCount = 2;
            List<Tuple<double, double>> waypoints = new List<Tuple<double, double>>();
            waypoints.Add(startPoint);

            //compute real distance
            double realDistance = myBox.getRealDistance().Result* 0.000621371;//convert to miles
            double distanceDifference = desiredDistance - realDistance;
            double minDistanceDifference = 0.5;//need some positive distance between them to have route variation

            bool goodRatio = true;
            if (distanceDifference <= minDistanceDifference)
            {
                goodRatio = false;
            }

            //don't want to generate routes if poor distance between them
			if (goodRatio)
			{
				waypointCount += (int)(distanceDifference/minDistanceDifference);//vary number of waypoints

                //creates random points in between (vary distances)
                for (int i = 2; i < waypointCount; i++)
                {
                    double smallerLongitude = startPoint.Item1;
                    double largerLongitude = endPoint.Item1;
                    //longitude
                    if (startPoint.Item1 - endPoint.Item1 > 0)
                    {
                        smallerLongitude = endPoint.Item1;
                        largerLongitude = startPoint.Item1;
                    }
                    double smallerLatitude = startPoint.Item2;
                    double largerLatitude = endPoint.Item2;
                    //latitude
                    if (startPoint.Item1 - endPoint.Item1 > 0)
                    {
                        smallerLatitude = endPoint.Item2;
                        largerLatitude = startPoint.Item2;
                    }

                    double coordinateOffset = 0.005;

                    double longitude = GetRandomNumberInRange(smallerLongitude - coordinateOffset, largerLongitude + coordinateOffset);
                    double latitude = GetRandomNumberInRange(smallerLatitude - coordinateOffset, largerLatitude + coordinateOffset);

                    Tuple<double, double> waypoint = Tuple.Create(longitude, latitude);
                    //check
                    Console.WriteLine(waypoint.Item1 + ", " + waypoint.Item2);
                }
                //add endpoint to end of matrix for ease of use
                waypoints.Add(endPoint);

			}
		}
	}
}