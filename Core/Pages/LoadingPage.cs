using System;
using Xamarin.Forms;
using Core.Services;
using System.Net.Http;

namespace Core
{
	public class LoadingPage : ContentPage
	{
		public LoadingPage (RootPage rootPage)
		{
			_rootPage = rootPage;
			NavigationPage.SetHasNavigationBar (this, false);

			//TODO : Inject ForecastService

			_viewModel = new LoadingViewModel (Navigation, new ForecastService (new OpenWeatherMapService (new HttpClient ())), rootPage);
			BindingContext = _viewModel;

			var statusMessageLabel = new LargeLabel {
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center,
				TextColor = Color.White
			};

			statusMessageLabel.SetBinding<LoadingViewModel> (Label.TextProperty, vm => vm.StatusMessage);

			var stackLayout = new StackLayout {
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center
			};

			var loadingImage = new Image ();

			loadingImage.SetBinding<LoadingViewModel> (Image.SourceProperty, vm => vm.LoadingImage);

			stackLayout.Children.Add (loadingImage);
			stackLayout.Children.Add (statusMessageLabel);
			stackLayout.Children.Add (new ActivityIndicator{ IsRunning = true }); 	

			Content = stackLayout;
		}

		RootPage _rootPage {
			get;
			set;
		}

		LoadingViewModel _viewModel {
			get;
			set;
		}

		protected override void OnDisappearing ()
		{
			base.OnDisappearing ();

			_rootPage.NavigateTo (new ForecastOptionItem (), _viewModel.Forecast);
		}

	}
}

