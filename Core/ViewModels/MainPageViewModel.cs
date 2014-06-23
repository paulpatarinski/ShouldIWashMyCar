using System;
using Core.Services;
using Xamarin.Forms;
using Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms.Labs.Services.Geolocation;
using System.Threading;

namespace Core
{
	public class MainPageViewModel : BaseViewModel
	{
		public MainPageViewModel (INavigation navigation, IForecastService forecastService)
		{
			_navigation = navigation;
			_forecastService = forecastService;

			Setup ();

			if (_geolocator.IsGeolocationEnabled) {
				GetForecastAsync ();
			} else {
				StatusMessage = "GPS Disabled, please enable GPS.";
			}
		}

		INavigation _navigation;
		readonly IForecastService _forecastService;
		IGeolocator _geolocator;
		CancellationTokenSource _cancelSource;
		private TaskScheduler scheduler = TaskScheduler.FromCurrentSynchronizationContext ();

		void Setup ()
		{
			if (_geolocator != null)
				return;

			_geolocator = DependencyService.Get<IGeolocator> ();
			_geolocator.PositionError += GeolocatorOnPositionError;
		}

		void GeolocatorOnPositionError (object sender, PositionErrorEventArgs e)
		{
			StatusMessage = e.Error.ToString ();
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

		private async Task GetForecastAsync ()
		{
			StatusMessageIsVisible = true;
			StatusMessage = "Getting current location...";

			_cancelSource = new CancellationTokenSource ();

			Position position = null;

			await _geolocator.GetPositionAsync (timeout: 10000, cancelToken: _cancelSource.Token, includeHeading: true).ContinueWith (t => {
				if (t.IsFaulted) {
					var geolocationException = t.Exception.InnerException as GeolocationException;

					if (geolocationException == null)
						StatusMessage = t.Exception.InnerException.ToString ();
					else
						StatusMessage = geolocationException.Error.ToString ();
				} else if (t.IsCanceled)
					StatusMessage = "Permission Denied";
				else {
					position = t.Result;
				}
			}, scheduler);

			if (position != null) {
				StatusMessage = "Getting weather forecast...";

				var forecast = await _forecastService.GetForecastAsync (position);

				StatusMessageIsVisible = false;

				DaysClean = forecast.DaysClean.ToString ();
				Reason = forecast.Reason;
				WeatherList = forecast.WeatherList;
			}
		}
	}
}