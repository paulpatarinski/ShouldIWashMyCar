using System;
using Xamarin.Forms;
using Core.Services;
using System.Net.Http;

namespace Core
{
	public class MainPage : ContentPage
	{
		public MainPage ()
		{	
			//TODO : Inject ForecastService
			BindingContext = new MainPageViewModel (Navigation, new ForecastService (new OpenWeatherMapService (new HttpClient ())));

			var statusMessageLabel = new Label { HorizontalOptions = LayoutOptions.Center, TextColor = Color.White };

			statusMessageLabel.SetBinding<MainPageViewModel> (Label.TextProperty, vm => vm.StatusMessage);
			statusMessageLabel.SetBinding<MainPageViewModel> (Label.IsVisibleProperty, vm => vm.StatusMessageIsVisible);

			Content = statusMessageLabel;
		}
	}
}

