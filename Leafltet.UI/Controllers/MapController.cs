using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Leaflet.UI.Models;


namespace Leaflet.UI.Controllers
{
    public class MapController : Controller
    {

        public class Geometry
        {
            public string type { get; set; }
            //public List<Coordinates> coordinates { get; set; }
            public List<List<List<List<float>>>> coordinates { get; set; }
        }
        public class FigureData
        {
            public string type { get; set; }
            //public List<List<List<List<float>>>> coordinates { get; set; }

        }


        private readonly ILogger<MapController> _logger;
        // şuradan web apiye istek yaparsın HttpClient
        public MapController(ILogger<MapController> logger)
        {
            _logger = logger;
        }

        public IActionResult Map()
        {
            return View();
        }


        [HttpPost]
        public void PostPolygon([FromBody] Polygon polygon)
        {
            Console.WriteLine("PostPolygon");
            PostToBackend<Polygon>(ref polygon, "Polygon");
        }

        [HttpPost]
        public void PostMultiPolygon([FromBody] MultiPolygon multiPolygon)
        {
            PostToBackend<MultiPolygon>(ref multiPolygon, "MultiPolygon");
        }

        [HttpPost]
        public void PostPoint([FromBody] Point point)
        {
            Console.WriteLine("PostPoint");
            PostToBackend<Point>(ref point, "Point");
        }

        [HttpPost]
        public void PostLineString([FromBody] LineString lineString)
        {
            Console.WriteLine("PostLineString");
            PostToBackend<LineString>(ref lineString, "LineString");
        }

        public void PostToBackend<Type>(ref Type data, String dataType)
        {
            Console.WriteLine("Post to Backend");
            using (var client = new HttpClient())
            {
                //HTTP POST
                var postTask = client.PostAsJsonAsync<Type>(("http://127.0.0.1:5001/api/add" + dataType), data);
                postTask.Wait();
            }
        }

        [HttpPost]
        public void PostPolygonSube([FromBody] SubePolygon sube)
        {
            Console.WriteLine("sube polygon");
            PostSubeToBackend<SubePolygon>(ref sube, "api/sube/addPolygonSube");
        }

        [HttpPost]
        public void PostMultiPolygonSube([FromBody] SubeMultiPolygon sube)
        {
            Console.WriteLine("sube multi polygon");
            PostSubeToBackend<SubeMultiPolygon>(ref sube, "api/sube/addMultiPolygonSube");
        }

        public void PostSubeToBackend<Type>(ref Type data, String endpoint)
        {
            Console.WriteLine("http://127.0.0.1:5001/" + endpoint);
            using (var client = new HttpClient())
            {
                //HTTP POST
                var postTask = client.PostAsJsonAsync<Type>(("http://127.0.0.1:5001/" + endpoint), data);
                postTask.Wait();

                Console.WriteLine(postTask);
            }
        }

        [HttpGet]
        public string GetBolgeler()
        {

            string bolgeler = "";
            using (var client = new HttpClient())
            {
                try
                {

                    var responseTask = client.GetAsync("http://127.0.0.1:5001/api/sube/getBolgeler");
                    responseTask.Wait();
                    var readTask = responseTask.Result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    bolgeler = readTask.Result;
                }
                catch
                {
                    Console.WriteLine("connection fail");
                }
            }
            return bolgeler;
        }

        [HttpGet]
        public string GetSubeler()
        {
            string _id = Request.Query["_id"];
            Console.WriteLine(_id);
            string subeler = "";
            using (var client = new HttpClient())
            {
                try
                {
                    var responseTask = client.GetAsync("http://127.0.0.1:5001/api/sube/getSubeler?_id=" + _id);
                    responseTask.Wait();
                    var readTask = responseTask.Result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    subeler = readTask.Result;
                }
                catch
                {
                    Console.WriteLine("connection fail");
                }
            }
            return subeler;
        }

        [HttpGet]
        public string GetSubeArea()
        {
            string _id = Request.Query["_id"];
            Console.WriteLine(_id);
            string subeler = "";
            using (var client = new HttpClient())
            {
                try
                {
                    var responseTask = client.GetAsync("http://127.0.0.1:5001/api/sube/getSubeler/getSubeGeo?_id=" + _id);
                    responseTask.Wait();
                    var readTask = responseTask.Result.Content.ReadAsStringAsync();
                    readTask.Wait();
                    subeler = readTask.Result;
                }
                catch
                {
                    Console.WriteLine("connection fail");
                }
            }
            return subeler;
        }

    }
}
