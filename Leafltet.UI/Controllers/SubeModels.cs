using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Leaflet.UI.Models
{
	public class SubeModels
	{
	}

	public class Sube
	{
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
}
