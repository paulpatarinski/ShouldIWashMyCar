using System;
using Xamarin.Forms;
using Core.Services;
using System.Net.Http;

namespace Core
{
	public class LoadingPage : ContentPage
	{
		public LoadingPage ()
		{
			NavigationPage.SetHasNavigationBar (this, false);

			//TODO : Inject ForecastService

			BindingContext = new LoadingViewModel (Navigation, new ForecastService (new OpenWeatherMapService (new HttpClient ())));

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
	}
}

