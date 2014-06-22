using System;
using Core.Services;
using Xamarin.Forms;
using Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core
{
	public class MainPageViewModel : BaseViewModel
	{
		public MainPageViewModel (INavigation navigation, IForecastService forecastService)
		{
			_navigation = navigation;
			_forecastService = forecastService;
			GetForecast ();
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

		private string _daysClean;

		public string DaysClean {
			get { return _daysClean; }
			set { ChangeAndNotify (ref _daysClean, value); }
		}

		private string _statusMessage;

		public string StatusMessage {
			get { return _statusMessage; }
			set { ChangeAndNotify (ref _statusMessage, value); }
		}

		private bool _statusMessageIsVisible;

		public bool StatusMessageIsVisible {
			get { return _statusMessageIsVisible; }
			set { ChangeAndNotify (ref _statusMessageIsVisible, value); }
		}

		private string _reason;

		public string Reason {
			get { return _reason; }
			set { ChangeAndNotify (ref _reason, value); }
		}

		private async Task GetForecast ()
		{
			StatusMessageIsVisible = true;
			StatusMessage = "Getting current location...";

			//TODO get current location
			var location = new Location {
				Latitude = 41.890969, Longitude = -87.676392 
			};

			StatusMessage = "Getting weather forecast...";
			var forecast = await _forecastService.GetForecastAsync (location);

			StatusMessageIsVisible = false;

			DaysClean = forecast.DaysClean.ToString ();
			Reason = forecast.Reason;
			WeatherList = forecast.WeatherList;
		}
	}
}

