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

            //RouteGeneration.generateOtherPoints()
            

            //List<Tuple<double, double>> value = mapbox.generateMatrix(points).Result.Item2;

            //return value;

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