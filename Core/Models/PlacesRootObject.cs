using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Core
{
	public class PlacesRootObject
	{
		public List<object> html_attributions { get; set; }

		[JsonProperty (PropertyName = "results")]
		public List<Place> Places { get; set; }

		public string status { get; set; }
	}
}

