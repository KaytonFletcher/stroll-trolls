using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MapBoxWrapper {
    public class mapBox {

        
        //Longitude, latitude

        private const string walkingURL = "https://api.mapbox.com/directions/v5/mapbox/walking/";
        private const string matrixURL = "https://api.mapbox.com/directions-matrix/v1/mapbox/walking/";
        private double distance { set; get; }
        public Tuple<double, double> startPoint { set; get; }
        public Tuple<double, double> endPoint { set; get; }

        private string accessToken { get; }
        
        private static HttpClient webber = new HttpClient();

        
        
        


        public mapBox(double distance, Tuple<double, double> startPoint, Tuple<double, double> endPoint) {
            this.distance = distance;
            this.startPoint = startPoint;
            this.endPoint = endPoint;
            StreamReader file = new StreamReader("accesstoken.cfg");
            accessToken = file.ReadLine();

        }
        
        

        public async Task<Tuple<List<List<double>>, List<Tuple<double, double>>>> firstStep(List<Tuple<double, double>> listOfPoints) {
            
            //calling matrix API

            string fullURL = matrixURL + createPathOfPoints(listOfPoints) + 
                             "?annotations=distance" + $"&access_token={accessToken}";
            
            
            var response = await webber.GetAsync(fullURL);

            var content = await response.Content.ReadAsStringAsync();

            JObject jsonData = JsonConvert.DeserializeObject<JObject>(content);

            List<List<double>> resultDistanceMatrix = new List<List<double>>();
            
            var ArrayOfArrays = jsonData["distances"];

            foreach (var array in ArrayOfArrays) {

                List<double> temp = new List<double>();

                foreach (var value in array) {
                    
                    temp.Add(value.ToObject<Double>());
                    
                }
                
                resultDistanceMatrix.Add(temp);
                
            }



            var waypoints = jsonData["destinations"];
            List<Tuple<double, double>> listOfWayPoints = new List<Tuple<double, double>>();
            foreach (var waypoint in waypoints) {

                var coords = waypoint["location"];
                var temp = new Tuple<double, double>(coords[0].ToObject<Double>(), coords[1].ToObject<Double>());

                listOfWayPoints.Add(temp);

            }
            
            
            return new Tuple<List<List<double>>, List<Tuple<double, double>>>(resultDistanceMatrix, listOfWayPoints);

        }

        public async Task<double> getRealDistance() {


            string fullURL = walkingURL + createPathOfPoints(new List<Tuple<double, double>>{startPoint, endPoint});

            fullURL += $".json?access_token={accessToken}";
            
            var response = await webber.GetAsync(fullURL);

            var content = await response.Content.ReadAsStringAsync();

            JObject jsonData = JsonConvert.DeserializeObject<JObject>(content);
            var routes = jsonData["routes"];
            var realDistance = routes[0]["distance"];

            
            return realDistance.ToObject<Double>();
        }
        
        
        
        
        


        public string createPathOfPoints(List<Tuple<double, double>> points) {
            
            
            
            string fullURL = "";

            for (int i = 0; i < points.Count - 1; i++) {
                
                fullURL += points[i].Item1 + "%2C" + points[i].Item2 + "%3B";

            }

            fullURL += points[points.Count - 1].Item1 + "%2C" + points[points.Count - 1].Item2;



            return fullURL;



        }





    }
}