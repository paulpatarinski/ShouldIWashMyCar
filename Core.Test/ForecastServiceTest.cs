using System;
using NUnit.Framework;
using Core.Services;
using System.Net.Http;
using ShouldIWashMyCar;
using FluentAssertions;
using Xamarin.Forms.Labs.Services.Geolocation;

namespace Core.Test
{
	[TestFixture]
	public class ForecastServiceTest
	{
		[Test]
		public void GetForecast_ShouldReturnA7DayForecast ()
		{
			//TODO supply mock instance 
			var openWeatherMapService = new OpenWeatherMapService (new HttpClient ());
			var forecastService = new ForecastService (openWeatherMapService);
			var location = new Position {
				Latitude = 41.890969, Longitude = -87.676392 
			};

			var resultTask = forecastService.GetForecastAsync (location);

			resultTask.Wait ();

			Assert.IsNotNull (resultTask.Result);
			Assert.AreEqual (resultTask.Result.WeatherList.Count, 7);
		}
	}
}

