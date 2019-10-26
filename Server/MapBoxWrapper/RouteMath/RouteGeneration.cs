using MapBoxWrapper;
using System;
using System.Collections.Generic;

namespace RouteMath {

	public class RouteGeneration {

        public static double GetRandomNumberInRange(double minNumber, double maxNumber)
        {
            return new Random().NextDouble() * (maxNumber - minNumber) + minNumber;
        }

<<<<<<< HEAD
        public static void routePath(mapBox myBox) {
=======
        public static Tuple<double, double> routePath(MapBox myBox) {
>>>>>>> 236cd377ddd508940c0d36bae4f50c2e1c0518f6

            double desiredDistance = myBox.getDistance();
            Tuple<double, double> startPoint = myBox.getStartPoint();
            Tuple<double, double> endPoint = myBox.getEndPoint();

            int waypointCount = 2;
            List<Tuple<double, double>> waypoints = new List<Tuple<double, double>>();
            waypoints.Add(startPoint);
            waypoints.Add(endPoint);

            double realDistance = myBox.getRealDistance().Result* 0.000621371;//convert to miles
            double distanceDifference = desiredDistance - realDistance;
            double minDistanceDifference = 0.5;

            bool goodRatio = true;
            if (distanceDifference <= minDistanceDifference)
            {
                goodRatio = false;
            }

			if (goodRatio)
			{
				waypointCount += 2*(int)(distanceDifference/minDistanceDifference) + 1;

                Console.WriteLine("We got a good ratio boys.");

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

                    double longitude = GetRandomNumberInRange(smallerLongitude, largerLongitude);
                    double latitude = GetRandomNumberInRange(smallerLatitude, largerLatitude);

                    Tuple<double, double> waypoint = Tuple.Create(longitude, latitude);
                    Console.WriteLine(waypoint.Item1 + ", " + waypoint.Item2);

                }
			}
		}
	}
}