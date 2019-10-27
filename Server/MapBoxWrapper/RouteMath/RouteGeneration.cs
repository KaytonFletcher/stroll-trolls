using MapBoxWrapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RouteMath {

	public class RouteGeneration {

        public static double GetRandomNumberInRange(double minNumber, double maxNumber)
        {
            return new Random().NextDouble() * (maxNumber - minNumber) + minNumber;
        }
        public static void routePath(MapBox myBox) {

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


        public static List<List<Tuple<int, int>>> generatePairs(int count) {
	        
	        List<List<Tuple<int, int>>> results = new List<List<Tuple<int, int>>>();

	        for (int i = 0; i < count; i++) {
		        
		        List<Tuple<int, int>> PossibleMoves = new List<Tuple<int, int>>();

		        for (int j = i + 1; j < count; j++) {
			        
			        Tuple<int, int> curr = new Tuple<int, int>(i, j);
			        PossibleMoves.Add(curr);
			        
			        
		        }
		        
		        results.Add(PossibleMoves);
		        
	        }


	        return results;

        }


        public static List<double> getAllDistances(List<List<double>> matrix) {

	        var allPairs = generatePairs(matrix.Count);

	        List<double> literallyAllDistances = new List<double>();


	        literallyAllDistances = grossRecursion(matrix, allPairs, allPairs[0][0]);


	        return literallyAllDistances;

        }



        private static List<double> grossRecursion(List<List<double>> matrix, List<List<Tuple<int, int>>> allPairs, Tuple<int, int> currTuple) {

	        var currValue = matrix[currTuple.Item1][currTuple.Item2];

	        if (currTuple.Item2 == allPairs.Count - 1) {
		        return new List<double>{currValue};
	        }
	        
	        
	        List<double> results = new List<double>();

	        foreach (Tuple<int, int> tuple in allPairs[currTuple.Item1]) {


		        List<double> recursiveresults = new List<double>();

		        if (tuple.Item2 == allPairs.Count - 1) {
			        recursiveresults.Add(matrix[tuple.Item1][tuple.Item2]);
		        }
		        else {
			        recursiveresults = grossRecursion(matrix, allPairs, allPairs[tuple.Item2][0]);
		        }


		        for (int i = 0; i < recursiveresults.Count; i++) {
			        recursiveresults[i] += currValue;
		        }

		        results = results.Concat(recursiveresults).ToList();

	        }

	        return results;


        }
        
        
        
	}
	
	
	
	
}