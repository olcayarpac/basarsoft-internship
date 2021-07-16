using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Leaflet.API.Models
{
	public class SubeModels
	{
	}

	public class Sube
	{
		public ObjectId _id { get; set; }
		public string SubeId { get; set; }
		public string IlceAdi { get; set; }
		public string IlAdi { get; set; }
		public string Adi { get; set; }
		public string SorumluKisi { get; set; }
		public int PersonelSayisi{ get; set; }
	}

	public class SubeMultiPolygon : Sube
	{
		public MultiPolygon Geo { get; set; }
	}

	public class SubePolygon : Sube
	{
		public Polygon Geo { get; set; }
	}

	public class Bolge
    {
		public ObjectId _id;
		public String bolgeAdi;
		public Polygon geo;
    }

	public class SubePoint
    {
		public ObjectId _id;
		public String subeAdi;
		public Point geo;
    }

}
