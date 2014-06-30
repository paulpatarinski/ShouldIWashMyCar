using System;
using Newtonsoft.Json;

namespace Core
{
	public class Place
	{
		public string name { get; set; }

		public string vicinity { get; set; }

		public Geometry geometry { get; set; }
	}

	public class GoogleLocation
	{
		public double lat { get; set; }

		public double lng { get; set; }
	}

	public class Geometry
	{
		public GoogleLocation location { get; set; }
	}
}

