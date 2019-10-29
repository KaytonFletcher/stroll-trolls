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

        
        public static bool verifyDistance(MapBox myBox, double realDistance, double minDistanceDifference) {
	    


	        return  !(myBox.getDistance() - realDistance <= minDistanceDifference);
        }
        
        
        public static List<Tuple<double, double>> generateOtherPoints(MapBox myBox) {

            //basic info

            
            double realDistance = myBox.getRealDistance().Result* 0.000621371;//convert to miles
            double distanceDifference = myBox.getDistance() - realDistance;
            
            double minDistanceDifference = 0.5;//need some positive distance between them to have route variation

            //don't want to generate routes if poor distance between them
            if (!verifyDistance(myBox, realDistance, minDistanceDifference)) {
	            throw new Exception("Distance validity failed");
            }
            
            //start at two to include starting and ending points
            int waypointCount = 2;
            List<Tuple<double, double>> waypoints = new List<Tuple<double, double>>();
            waypoints.Add(myBox.startPoint);

            //compute real distance
            
			
			waypointCount = 2 + (int)(distanceDifference/minDistanceDifference);//vary number of waypoints

			//creates random points in between (vary distances)
			for (int i = 2; i < waypointCount; i++)
			{
				double smallerLongitude = myBox.startPoint.Item1;
				double largerLongitude = myBox.endPoint.Item1;
				//longitude
				if (myBox.startPoint.Item1 - myBox.endPoint.Item1 > 0)
				{
					smallerLongitude = myBox.endPoint.Item1;
					largerLongitude = myBox.startPoint.Item1;
				}
				double smallerLatitude = myBox.startPoint.Item2;
				double largerLatitude = myBox.endPoint.Item2;
				//latitude
				if (myBox.startPoint.Item1 - myBox.endPoint.Item1 > 0)
				{
					smallerLatitude = myBox.endPoint.Item2;
					largerLatitude = myBox.startPoint.Item2;
				}

				double coordinateOffset = 0.0075;

				double longitude = GetRandomNumberInRange(smallerLongitude - coordinateOffset, largerLongitude + coordinateOffset);
				double latitude = GetRandomNumberInRange(smallerLatitude - coordinateOffset, largerLatitude + coordinateOffset);

				Tuple<double, double> waypoint = Tuple.Create(longitude, latitude);
				waypoints.Add(waypoint);
				
				//check
				//Console.WriteLine(waypoint.Item1 + ", " + waypoint.Item2);
			}
			//add endpoint to end of matrix for ease of use
			waypoints.Add(myBox.endPoint);

			return waypoints;
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


        public static List<Tuple<double, List<Tuple<int, int>>>> 
	        getDistancesAndPoints(List<List<double>> matrix, double distance, double variance) {

	        var allPairs = generatePairs(matrix.Count);

	        List<Tuple<double, List<Tuple<int, int>>>> literallyAllDistances = new List<Tuple<double, List<Tuple<int, int>>>>();


	        literallyAllDistances = grossRecursion(matrix, allPairs, allPairs[0][0]);

	        foreach (var value in literallyAllDistances) {
		        value.Item2.Reverse();
	        }

	        literallyAllDistances = CullValues(literallyAllDistances, distance, variance);

	        return literallyAllDistances;

        }



        private static List<Tuple<double, List<Tuple<int, int>>>> grossRecursion(List<List<double>> matrix, 
	        List<List<Tuple<int, int>>> allPairs, Tuple<int, int> currTuple) {

	        var currValue = matrix[currTuple.Item1][currTuple.Item2];

	        if (currTuple.Item2 == allPairs.Count - 1) {

		        List<Tuple<int, int>> coords = new List<Tuple<int, int>> {
			        new Tuple<int, int>(currTuple.Item1, currTuple.Item2)
		        };
		        
		        return new List<Tuple<double, List<Tuple<int, int>>>> {
			        new Tuple<double, List<Tuple<int, int>>>(currValue, coords)
		        };
	        }
	        
	        
	        List<Tuple<double, List<Tuple<int, int>>>> results = new List<Tuple<double, List<Tuple<int, int>>>>();

	        foreach (Tuple<int, int> tuple in allPairs[currTuple.Item1]) {


		        
		        
		        List<Tuple<double, List<Tuple<int, int>>>> recursiveresults = new List<Tuple<double, List<Tuple<int, int>>>>();

		        if (tuple.Item2 == allPairs.Count - 1) {

			        var value1 = matrix[tuple.Item1][tuple.Item2];
			        var value2 = new List<Tuple<int, int>> {new Tuple<int, int>(tuple.Item1, tuple.Item2)};

			        recursiveresults.Add(new Tuple<double, List<Tuple<int, int>>>(value1, value2));
		        }
		        else {
			        recursiveresults = grossRecursion(matrix, allPairs, allPairs[tuple.Item2][0]);
		        }


		        for (int i = 0; i < recursiveresults.Count; i++) {
			        var newDouble = recursiveresults[i].Item1 + currValue;
			        recursiveresults[i].Item2.Add(tuple);
			        recursiveresults[i] = new Tuple<double, List<Tuple<int, int>>>(newDouble, recursiveresults[i].Item2);
		        }

		        results = results.Concat(recursiveresults).ToList();

	        }

	        return results;


        }
        
        
        
        
        //distance in MILES, variance in MILES
        private static List<Tuple<double, List<Tuple<int, int>>>> 
	        CullValues(List<Tuple<double, List<Tuple<int, int>>>> values, double distance, double variance){
        
        
	        List<Tuple<double, List<Tuple<int, int>>>> newValues = new List<Tuple<double, List<Tuple<int, int>>>>();

	        for (int i = 0; i < values.Count; i++) {

		        if (Math.Abs(values[i].Item1*0.000621371 - distance) <= variance) {


			        if (values[i].Item2.Distinct().Count() == values[i].Item2.Count) {

				        newValues.Add(values[i]);
			        }

//			        else {
//				        Console.WriteLine("Found duplicate data:");
//			        }

		        }
	        }

	        return newValues;
        }
        
        
	}
	
	
	
	
}