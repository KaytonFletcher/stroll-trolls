namespace RouteMath {

	public class RouteGeneration {

		private double desiredDistance;
		private Tuple<double, double> startPoint;
		private Tuple<double, double> endPoint;
		private bool goodRatio;
		private double realDistance;
		private int waypointCount;
		private vector<Tuple<double, double>> waypoints;

		RouteGeneration (mapBox myMapBox) {
			desiredDistance = myMapBox.getDesiredDistance();
			startPoint = myMapBox.getStartPoint();
			endPoint = myMapBox.getEndPoint();
			goodRatio = true;
			realDistance = desiredDistance/myMapBox.getRealDistance();
			waypointCount = 0;
		}
	}
}