using MapBoxWrapper;
using System;

namespace RouteMath {

	public class RouteGeneration {

        public double GetRandomNumberInRange(double minNumber, double maxNumber)
        {
            return new Random().NextDouble() * (maxNumber - minNumber) + minNumber;
        }

        public static Tuple<double, double> routePath(mapBox myBox) {

            double desiredDistance = myBox.getDistance().Result;
            Tuple<double, double> startPoint = myBox.getStartPoint();
            Tuple<double, double> endPoint = myBox.getEndPoint();

            int waypointCount = 2;
            vector<Tuple<double, double>> waypoints;
            waypointCount.push_back(startPoint);
            waypointCount.push_back(endPoint);

            double realDistance = myBox.getRealDistance();
            double distanceDifference = desiredDistance - realDistance;
            double minDistanceDifference = 0.5;

            bool goodRatio = true;
            if (distanceDifference <= minDistanceDifference)
            {
                routeGeneration.setDistanceRatio(false);
            }

			if (goodRatio)
			{
				waypointCount += 2*distanceDifference/minDistanceDifference + 1;
				for (i = 2; i < waypointCount; i++)
				{
                    double smallerLongitude = startPoint.Item1();
                    double largerLongitude = endPoint.Item1();
					//longitude
                    if (startPoint.Item1() - endPoint.Item1() > 0)
                    {
                        smallerLongitude = endPoint.Item1();
                        largerLongitude = startPoint.Item1();
                    }
                    double smallerLattitude = startPoint.Item2();
                    double largerLattitude = endPoint.Item2();
                    //lattitude
                    if (startPoint.Item1() - endPoint.Item1() > 0)
                    {
                        smallerLattitude = endPoint.Item2();
                        largerLattitude = startPoint.Item2();
                    }

                    double longitude = GetRandomNumberInRange(smallerLongitude, largerLongitude);
                    double lattitude = GetRandomNumberInRange(smallerLattitude, largerLattitude);

                    Tuple<double, double> waypoint;
                    waypoint.Item1(longitude);
                    waypoint.Item2(lattitude);

                }
			}
		}
	}
}