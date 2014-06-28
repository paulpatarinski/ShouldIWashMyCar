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
	public class LoadingViewModel : BaseViewModel
	{
		public LoadingViewModel (INavigation navigation, IForecastService forecastService, RootPage rootPage)
		{
			this.rootPage = rootPage;
			_navigation = navigation;
			_forecastService = forecastService;

			LoadingImage = "Radar";

			Setup ();

			if (_geolocator.IsGeolocationEnabled) {
				GetForecastAsync ();
			} else {
				StatusMessage = "GPS Disabled, please enable GPS.";
			}
		}

		RootPage rootPage;
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

		private string _statusMessage;

		public string StatusMessage {
			get { return _statusMessage; }
			set { ChangeAndNotify (ref _statusMessage, value); }
		}


		private string _loadingImage;

		public string LoadingImage {
			get { return _loadingImage; }
			set { ChangeAndNotify (ref _loadingImage, value); }
		}

		public Forecast Forecast{ get; set; }

		private async Task GetForecastAsync ()
		{
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
				LoadingImage = "Sunny";
				StatusMessage = "Getting weather forecast...";

				Forecast = await _forecastService.GetForecastAsync (position);

				await _navigation.PopModalAsync ();
			}
		}
	}
}