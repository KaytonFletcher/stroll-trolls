using System.Collections.Generic;

namespace StrollTrollAPI.Models {
    public class Route {


        public List<WayPoint> waypoints;
        public class WayPoint {
            public double longitude { get; set; }
            public double latitude { get; set; }
            
        }
        
        
        
    }
}