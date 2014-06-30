using System;
using System.Collections.Generic;
using Xamarin.Forms.Labs.Services.Geolocation;

namespace Core.Models
{
	public class Forecast
	{
		private List<WeatherViewTemplate> _weatherList;

		public int DaysClean { get; set; }

		public List<WeatherViewTemplate> WeatherList {
			get {
				if (_weatherList == null) {
					_weatherList = new List<WeatherViewTemplate> ();
				}

				return _weatherList;
			}
			set { _weatherList = value; }
		}

		public string Reason { get; set; }

		public DateTime BadWeatherDay { get; set; }

		public string ReasonDescription { get; set; }

		public Position Location { get; set; }
	}
}
