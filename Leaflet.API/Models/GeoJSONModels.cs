using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Leaflet.API
{
	public class GeoJSONModels
	{
	}

	public class Geometry
	{
		public string type { get; set; }
	}
	public class Point : Geometry
	{
		public List<float> coordinates { get; set; }
	}
	public class MultiPoint : Geometry
	{
		public List<List<float>> coordinates { get; set; }
	}
	public class LineString : Geometry
	{
		public List<List<float>> coordinates { get; set; }
	}
	public class MultiLineString : Geometry
	{
		public List<List<List<float>>> coordinates { get; set; }
	}
	public class Polygon : Geometry
	{
		public List<List<List<double>>> coordinates { get; set; }
	}
	public class MultiPolygon : Geometry
	{
		public List<List<List<List<float>>>> coordinates { get; set; }
	}
}
