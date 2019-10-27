using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MapBoxWrapper;
using StrollTrollAPI.Models;
using Newtonsoft.Json;
using RouteMath;


namespace StrollTrollAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class MapBoxController : ControllerBase {
        
        private static HttpClient webber = new HttpClient();
        private const string PROJECT_ID = "wonderlandgt2019";

        
        [HttpGet]
        public ActionResult<List<Tuple<double, double>>> GetRoute(double distance, [FromQuery] double[] coordinates) {
            
            //coordinates: long, lat, start to end
            
            
            if (coordinates.Length < 4 || coordinates.Length % 2 != 0) {
                return BadRequest();
            }

            MapBox mapbox = new MapBox(distance, new Tuple<double, double>(coordinates[0], coordinates[1]),
                new Tuple<double, double>(coordinates[2], coordinates[3]));

            var wayPoints = RouteGeneration.generateOtherPoints(mapbox);
            

            var matrixResult = mapbox.generateMatrix(wayPoints).Result;
            var matrix = matrixResult.Item1;
            var realCoords = matrixResult.Item2;
            

            var distancesAndPoints = RouteGeneration.getDistancesAndPoints(matrix, mapbox.getDistance(), 0.5);

            var random = new Random();
            var selectedRoute = distancesAndPoints[random.Next(distancesAndPoints.Count)];

            var listOfPoints = selectedRoute.Item2;

            List<int> indices = new List<int>();
            for (int i = 0; i < listOfPoints.Count; i++) {
                indices.Add(listOfPoints[i].Item1);
            }
            indices.Add(listOfPoints[listOfPoints.Count-1].Item2);
            
            List<Tuple<double, double>> latsAndLongs = new List<Tuple<double, double>>();

            for (int i = 0; i < indices.Count; i++) {
                
                latsAndLongs.Add(realCoords[indices[i]]);
            }
            
            return latsAndLongs;

        }


//        [HttpPut]
//        [Route("{id}")]
//        public async Task<ActionResult<string>> PutData(string Id, [FromBody]Route route) {
//
//           
//            
//            
//            
//
//
//        }
        
        
        
    }
}