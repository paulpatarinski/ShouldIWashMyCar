using System;
using System.Globalization;
using System.Linq;
using Core.Models;
using ShouldIWashMyCar;
using System.Threading.Tasks;
using Xamarin.Forms.Labs.Services.Geolocation;

namespace Core.Services
{
	public class ForecastService : IForecastService
	{
		OpenWeatherMapService _openWeatherMapService {
			get;
			set;
		}

		public ForecastService (OpenWeatherMapService openWeatherMapService)
		{
			_openWeatherMapService = openWeatherMapService;
		}

		public async Task<Forecast> GetForecastAsync (Position location)
		{
			var openWeatherForecast = await _openWeatherMapService.Get7DayForecastAsync (location);
			var forecast = new Forecast () {
				Location = location
			};

			var daysClean = 0;
			var dtf = new DateTimeFormatInfo ();
			
			foreach (var forecastItem in openWeatherForecast.Forecasts) {
				var weather = forecastItem.WeatherList.FirstOrDefault ();
				var date = new DateTime (1970, 1, 1).AddSeconds (forecastItem.Dt);
			
				forecast.WeatherList.Add (new WeatherViewTemplate {
					WeatherCondition = weather.Description,
					DayAbbreviation = dtf.GetAbbreviatedDayName (date.DayOfWeek),
					TempHigh = Convert.ToInt32(forecastItem.Temperature.Max) + "º",
					TempLow = Convert.ToInt32(forecastItem.Temperature.Min) + "º",
					Icon = GetWeatherIcon (weather.Main)
				});
			
			}

			foreach (var forecastItem in openWeatherForecast.Forecasts) {
				var date = new DateTime (1970, 1, 1).AddSeconds (forecastItem.Dt);
			
				if (date.Date.Date < DateTime.Now.Date.Date)
					continue;
			
				var weatherForToday = forecastItem.WeatherList [0];
			
				forecast.BadWeatherDay = date;
				forecast.Reason = ConvertReason (weatherForToday.Main);
				forecast.ReasonDescription = weatherForToday.Description;
			
				if (WeatherIsBad (weatherForToday))
					break;
			
				daysClean++;
			}
			
			forecast.DaysClean = daysClean;

			return forecast;
		}

		string GetWeatherIcon (string description)
		{
			var descriptionToLower = description.ToLower ();

			switch (descriptionToLower) {
			case "rain": 
				return "Rain";	
			case "clouds":
				return "Cloud";
			case "snow": 
				return "Snow";
			case "clear":
				return "Clear";
			}

			return string.Empty;
		}

		/// <summary>
		/// Convert the Service Weather Reason to a custom reason
		/// </summary>
		/// <param name="originalReason"></param>
		/// <returns></returns>
		private string ConvertReason (string originalReason)
		{
			//For testing purposes
			//return "Rain";
			//return "Snow";

			if (!string.IsNullOrEmpty (originalReason)) {
				switch (originalReason) {
				case "Clear":
					return string.Empty;
				}
			}

			return originalReason;
		}

		private bool WeatherIsBad (Weather weatherForToday)
		{
			if (weatherForToday.Main == "Rain" || weatherForToday.Main == "Snow") {
				return true;
			}

			return false;
		}
	}
}
