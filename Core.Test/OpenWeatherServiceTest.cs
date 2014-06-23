using NUnit.Framework;
using Core.Services;
using System.Net.Http;
using ShouldIWashMyCar;
using FluentAssertions;
using Xamarin.Forms.Labs.Services.Geolocation;

namespace Core.Test
{
	[TestFixture]
	public class OpenWeatherServiceTest
	{
		[Test]
		public async void GetOpenWeatherForecast_ShouldReturnTheWeatherForecast ()
		{
			var openWeatherMapService = new OpenWeatherMapService (new HttpClient ());

			var location = new Position {
				Latitude = 41.890969, Longitude = -87.676392 
			};

			var resultTask = openWeatherMapService.Get7DayForecastAsync (location);
			resultTask.Wait ();

			Assert.IsNotNull (resultTask.Result);
		}
	}
}
