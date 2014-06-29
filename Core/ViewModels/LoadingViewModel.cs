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
			IsRefreshButtonVisible = false;
			IsActivityIndicatorVisible = false;

			Setup ();

			if (_geolocator.IsGeolocationEnabled) {
				GetForecastAsync ();
			} else {
				StatusMessage = "GPS is disabled, please enable GPS and refresh.";
				IsRefreshButtonVisible = true;
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

		private Command _getForecastCommand;

		public Command GetForecastCommand {
			get {
				return _getForecastCommand ?? (_getForecastCommand = new Command (async () => await GetForecastAsync ()));
			}
		}

		private bool _isRefreshButtonVisible;

		public bool IsRefreshButtonVisible {
			get { return _isRefreshButtonVisible; }
			set { ChangeAndNotify (ref _isRefreshButtonVisible, value); }
		}

		private bool _isActivityIndicatorVisible;

		public bool IsActivityIndicatorVisible {
			get { return _isActivityIndicatorVisible; }
			set { ChangeAndNotify (ref _isActivityIndicatorVisible, value); }
		}


		public async Task GetForecastAsync ()
		{
			IsActivityIndicatorVisible = true;
			StatusMessage = "Getting current location...";

			_cancelSource = new CancellationTokenSource ();

			Position position = null;
			_geolocator.DesiredAccuracy = 50;

			await _geolocator.GetPositionAsync (timeout: 10000, cancelToken: _cancelSource.Token, includeHeading: true).ContinueWith (t => {
				IsActivityIndicatorVisible = false;

				if (t.IsFaulted) {
					IsRefreshButtonVisible = true;

					var geolocationException = t.Exception.InnerException as GeolocationException;

					if (geolocationException == null)
						StatusMessage = t.Exception.InnerException.ToString ();
					else
						StatusMessage = geolocationException.Error.ToString ();
				} else if (t.IsCanceled) {
					StatusMessage = "Permission denied, please allow the application to access GPS and refresh.";
					IsRefreshButtonVisible = true;
				} else {
					position = t.Result;
				}
			}, scheduler);

			if (position != null) {
				IsActivityIndicatorVisible = true;
				IsRefreshButtonVisible = false;
				LoadingImage = "Sunny";
				StatusMessage = "Getting weather forecast...";

				Forecast = await _forecastService.GetForecastAsync (position);

				await _navigation.PopModalAsync ();
			}
		}
	}
}