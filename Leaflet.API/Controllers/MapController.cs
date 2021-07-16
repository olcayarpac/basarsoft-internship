using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace Leaflet.API.Controllers
{
	

	[Route("api/")]
	[ApiController]
	[EnableCors("MyPolicy")]
	public class MapController : ControllerBase
	{

		
		[HttpGet]
		public IActionResult AddPolygonToDb()
		{
			DateTime time = DateTime.Now;
			Console.WriteLine("------------");
			Console.WriteLine("New get req at: " +time.TimeOfDay.ToString());
			return Ok();
		}


		[HttpPost]
		[Route("deneme/")]
		public IActionResult Deneme([FromBody] String str)
		{
			DateTime time = DateTime.Now;
			Console.WriteLine("------------");
			Console.WriteLine("New DENEME Req at: " + time.TimeOfDay.ToString());
			Console.WriteLine(str);
			Console.WriteLine("------------");
			return Ok();
		}


		[HttpPost]
		[Route("addPoint/")]
		public IActionResult AddPointToDb([FromBody] Point point)
		{
			printTime("Point");
			AddToDb<Point>(ref point);
			return Ok();
		}

		[HttpPost]
		[Route("addMultiPoint/")]
		public IActionResult AddMultiPointToDb([FromBody] MultiPoint multiPoint)
		{
			printTime("MultiPoint");
			AddToDb<MultiPoint>(ref multiPoint);
			return Ok();
		}

		[HttpPost]
		[Route("addLineString/")]
		public IActionResult AddLineStringToDb([FromBody] LineString lineString)
		{
			printTime("LineString");
			AddToDb<LineString>(ref lineString);
			return Ok();
		}

		[HttpPost]
		[Route("addMultiLineString/")]
		public IActionResult AddMultiLineStringToDb([FromBody] MultiLineString multilineString)
		{
			printTime("MultiLineString");
			AddToDb<MultiLineString>(ref multilineString);
			return Ok();
		}

		[HttpPost]
		[Route("addPolygon/")]
		public IActionResult AddPolygonToDb([FromBody] Polygon polygon)
		{
			printTime("Polygon");
			AddToDb<Polygon>(ref polygon);
			return Ok();
		}

		[HttpPost]
		[Route("addMultiPolygon/")]
		public IActionResult AddMultiPolygonToDb([FromBody] MultiPolygon multiPolygon)
		{
			printTime("MultiPolygon");
			AddToDb<MultiPolygon>(ref multiPolygon);
			return Ok();
		}


		public void printTime(String shape)
		{
			Console.WriteLine("------------");
			DateTime time = DateTime.Now;
			Console.WriteLine("POST req / "+ shape + " / " +time.TimeOfDay.ToString());
			Console.WriteLine("------------");
		}

		public void AddToDb<Type>(ref Type geoJsonObject)
		{
			var client = new MongoClient("mongodb://localhost:27017/");
			var database = client.GetDatabase("basarsoft");
			IMongoCollection<Type> __polygons;
			__polygons = database.GetCollection<Type>("coordinates");
			__polygons.InsertOne(geoJsonObject);
		}


	}
}
