using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MapBoxWrapper;


namespace StrollTrollAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class MapBoxController : ControllerBase {
        
        [HttpGet]
        public ActionResult<List<Tuple<double, double>>> GetRoute(double distance, [FromQuery] double[] coordinates) {
            
            //coordinates: long, lat, start to end
            
            
            if (coordinates.Length < 4 || coordinates.Length % 2 != 0) {
                return BadRequest();
            }

            MapBox mapbox = new MapBox(distance, new Tuple<double, double>(coordinates[0], coordinates[1]),
                new Tuple<double, double>(coordinates[2], coordinates[3]));

            List<Tuple<double, double>> points = new List<Tuple<double, double>>();
            
            for (int i = 0; i < coordinates.Length; i+=2) {
                
                points.Add(new Tuple<double, double>(coordinates[i], coordinates[i+1]));
                
                
            }

            List<Tuple<double, double>> value = mapbox.generateMatrix(points).Result.Item2;

            return value;

        }
        
        
        
    }
}