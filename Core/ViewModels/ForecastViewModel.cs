using System;
using Core.Models;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core
{
	public class ForecastViewModel : BaseViewModel
	{
		readonly INavigation navigation;

		readonly Forecast forecast;

		public ForecastViewModel (INavigation navigation, Forecast forecast)
		{
			this.forecast = forecast;
			this.navigation = navigation;

			DaysClean = forecast.DaysClean.ToString ();

			if (forecast.DaysClean == 1)
				DaysText = "day";
			else
				DaysText = "days";
				
			Reason = forecast.Reason;
			WeatherList = forecast.WeatherList;
		}

		private string _reason;

		public string Reason {
			get { return _reason; }
			set { ChangeAndNotify (ref _reason, value); }
		}

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

		private string _daysText;

		public string DaysText {
			get { return _daysText; }
			set { ChangeAndNotify (ref _daysText, value); }
		}

		private Command _showCarWashesMapCommand;

		public Command ShowCarWashesMapCommand {
			get {
				return _showCarWashesMapCommand ?? (_showCarWashesMapCommand = new Command (async () => await ShowCarWashesMapAsync ()));
			}
		}

		async Task ShowCarWashesMapAsync ()
		{
			await navigation.PushAsync (new CarWashesMapPage (forecast.Location));
		}
	}
}