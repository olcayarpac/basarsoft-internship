using Leaflet.API.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver.GeoJsonObjectModel;
using System.Collections;
using System.Web;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Leaflet.API.Controllers
{
    [Route("api/sube")]
    [ApiController]
    [EnableCors("MyPolicy")]
    public class SubeController : ControllerBase
    {
        // GET: api/<SubeController>
        [HttpGet]
        public IActionResult AddPolygonToDb()
        {
            DateTime time = DateTime.Now;
            Console.WriteLine("------------");
            Console.WriteLine("GET / Sube " + time.TimeOfDay.ToString());
            return Ok();
        }

        [HttpPost]
        [Route("addPolygonSube/")]
        public IActionResult AddSubeToDb([FromBody] SubePolygon sube)
        {
            DateTime time = DateTime.Now;
            Console.WriteLine("------------");
            Console.WriteLine("Sube " + time.TimeOfDay.ToString());
            Console.WriteLine("------------");
            AddToDb<SubePolygon>(sube);
            return Ok();
        }

        [HttpPost]
        [Route("addMultiPolygonSube/")]
        public IActionResult AddSubeMultiToDb([FromBody] SubeMultiPolygon sube)
        {
            DateTime time = DateTime.Now;
            Console.WriteLine("------------");
            Console.WriteLine("Multi Sube " + time.TimeOfDay.ToString());
            Console.WriteLine("------------");
            AddToDb<SubeMultiPolygon>(sube);
            return Ok();
        }

        [HttpPost]
        [Route("deneme2/")]
        public IActionResult DenemeFunction(BsonDocument sube)
        {
            DateTime time = DateTime.Now;
            Console.WriteLine("------------");
            Console.WriteLine("Deneme Req" + time.TimeOfDay.ToString());
            Console.WriteLine("------------");

            return Ok(sube);
        }

        [HttpGet]
        [Route("getBolgeler/")]
        public IActionResult GetBolgeler()
        {
            Console.WriteLine("get bolgeler api");
            var client = new MongoClient("mongodb://localhost:27017/");
            var database = client.GetDatabase("basarsoft");
            var collection = database.GetCollection<BsonDocument>("bolgeler");
            var documents = collection.Find(new BsonDocument()).ToList();

            foreach (var docx in documents)
            {
                docx["_id"] = docx["_id"].ToString();
            }

            var dotNetObjList = documents.ConvertAll(BsonTypeMapper.MapToDotNetValue);

            return Ok(dotNetObjList);
        }

        [HttpGet]
        [Route("getSubeler/")]
        public IActionResult GetSubeler()
        {
            Console.WriteLine("get subeler api");
            var client = new MongoClient("mongodb://localhost:27017/");
            var database = client.GetDatabase("basarsoft");
            var collection = database.GetCollection<BsonDocument>("subelerPoint");

            string _id = Request.Query["_id"];
            Console.WriteLine(_id);
            //var bolge = database.GetCollection<Bolge>("bolgeler").Find($"{{_id: ObjectId('60dda0afaa19f36669cd4405')}}").FirstOrDefault();
            var bolge = database.GetCollection<Bolge>("bolgeler").Find(x=>x._id==ObjectId.Parse(_id)).FirstOrDefault();

            Console.WriteLine(bolge.bolgeAdi);
            //var polygon = GeoJson.Polygon();

            double[,] polygon = new double[bolge.geo.coordinates[0].Count, 2];
            int i = 0;

            foreach (var coordinate in bolge.geo.coordinates[0])
            {
                polygon[i, 0] = coordinate[0];
                polygon[i, 1] = coordinate[1];
                i++;

            }

            double[,] polygon2 = new double[,] { { 29.084930419921875, 41.19622318190573 }, { 28.856964111328125, 41.20655580884106 }, { 28.74847412109375, 41.062786068733026 }, { 28.900909423828125, 40.97678774053034 }, { 29.051971435546875, 41.049323867571616 }, { 29.084930419921875, 41.19622318190573 } };

            var filter = Builders<BsonDocument>.Filter.GeoWithinPolygon("geo", polygon);
            var subeler = collection.Find(filter).ToList();

            foreach (var sube in subeler)
            {
                sube["_id"] = sube["_id"].ToString();
            }

            var dotNetObjList = subeler.ConvertAll(BsonTypeMapper.MapToDotNetValue);

            return Ok(dotNetObjList);
        }


        [HttpGet]
        [Route("getSubeler/getSubeGeo")]
        public IActionResult GetSubeGeo()
        {
            Console.WriteLine("get subeler api");
            var client = new MongoClient("mongodb://localhost:27017/");
            var database = client.GetDatabase("basarsoft");
            var collection = database.GetCollection<BsonDocument>("subeler");

            string _id = Request.Query["_id"];
            Console.WriteLine(_id);
            //var bolge = database.GetCollection<Bolge>("bolgeler").Find($"{{_id: ObjectId('60dda0afaa19f36669cd4405')}}").FirstOrDefault();
            var subeler = database.GetCollection<BsonDocument>("subeler").Find(x => x["SubeId"] == _id).ToList();


            foreach (var sube in subeler)
            {
                sube["_id"] = sube["_id"].ToString();
            }

            var dotNetObjList = subeler.ConvertAll(BsonTypeMapper.MapToDotNetValue);

            return Ok(dotNetObjList);
        }

        public void AddToDb<Type>(Type geoJsonObject)
        {
            var client = new MongoClient("mongodb://localhost:27017/");
            var database = client.GetDatabase("basarsoft");
            IMongoCollection<Type> __polygons;
            __polygons = database.GetCollection<Type>("subeler");
            __polygons.InsertOne(geoJsonObject);
        }

    }
}
