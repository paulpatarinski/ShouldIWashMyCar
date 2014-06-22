using System;
using Core.Services;
using Xamarin.Forms;
using Core.Models;
using System.Collections.Generic;

namespace Core
{
	public class MainPageViewModel : BaseViewModel
	{
		public MainPageViewModel (INavigation navigation, IForecastService forecastService)
		{
			_navigation = navigation;
			_forecastService = forecastService;
		}


		INavigation _navigation;
		readonly IForecastService _forecastService;

		private List<WeatherViewTemplate> _weatherList;

		public List<WeatherViewTemplate> WeatherList {
			get {
				if (_weatherList == null) {
					_weatherList = new List<WeatherViewTemplate> ();
				}
				return _weatherList;
			}
			set { ChangeAndNotify (ref _weatherList, value); }
		}

		private int _daysClean;

		public int DaysClean {
			get { return _daysClean; }
			set { ChangeAndNotify (ref _daysClean, value); }
		}

		private async void GetForecast ()
		{
			//TODO get current location
			var location = new Location {
				Latitude = 41.890969, Longitude = -87.676392 
			};

			var forecast = await _forecastService.GetForecastAsync (location);

			DaysClean = forecast.DaysClean;
			WeatherList = forecast.WeatherList;
		}
	}
}

