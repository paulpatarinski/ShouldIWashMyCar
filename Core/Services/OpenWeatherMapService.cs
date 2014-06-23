using System;
using Core.Models;
using System.Net.Http;
using ShouldIWashMyCar;
using System.Threading.Tasks;
using Xamarin.Forms.Labs.Services.Geolocation;

namespace Core.Services
{
	public class OpenWeatherMapService : IOpenWeatherMapService
	{
		public OpenWeatherMapService (HttpClient restClient)
		{
			_restClient = restClient;
		}

		private readonly HttpClient _restClient;

		private const string API_KEY = "f1b95129238500926b4806dfdee9a05a";

		private const string OpenWeatherApiUrl =
			"http://api.openweathermap.org/data/2.5/forecast/daily?lat={0}&lon={1}&cnt=7&mode=json&units=imperial&APPID={2}";

		public async Task<OpenWeatherForecast> Get7DayForecastAsync (Position location)
		{
			var uri = string.Format (OpenWeatherApiUrl, location.Latitude,
				          location.Longitude, API_KEY);

			var result = await _restClient.GetAsync<OpenWeatherForecast> (uri);

			return result;
		}
	}
}