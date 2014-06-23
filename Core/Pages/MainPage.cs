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

			var masterGrid = new Grid {
				RowDefinitions = new RowDefinitionCollection {
					new RowDefinition{ Height = new GridLength (0.1, GridUnitType.Star) },
					new RowDefinition{ Height = new GridLength (0.8, GridUnitType.Star) },
					new RowDefinition{ Height = new GridLength (0.1, GridUnitType.Star) }
				},
				ColumnDefinitions = new ColumnDefinitionCollection{ new ColumnDefinition{ Width = new GridLength (1, GridUnitType.Star) } }
			};

			var statusMessageLabel = new Label { HorizontalOptions = LayoutOptions.Center, TextColor = Color.White };

			statusMessageLabel.SetBinding<MainPageViewModel> (Label.TextProperty, vm => vm.StatusMessage);
			statusMessageLabel.SetBinding<MainPageViewModel> (VisualElement.IsVisibleProperty, vm => vm.StatusMessageIsVisible);

			masterGrid.Children.Add (statusMessageLabel, 0, 0);
			masterGrid.Children.Add (CreateForecastStatusStackLayout (), 0, 1);

			Content = masterGrid;
		}

		StackLayout CreateForecastStatusStackLayout ()
		{
			var stackLayout = new StackLayout {
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand
			};

			stackLayout.SetBinding<MainPageViewModel> (VisualElement.IsVisibleProperty, vm => vm.ForecastIsVisible);

			var daysLabel = new LargeLabel ();
			daysLabel.SetBinding<MainPageViewModel> (Label.TextProperty, vm => vm.DaysClean);

			var reasonLabel = new LargeLabel ();
			reasonLabel.SetBinding<MainPageViewModel> (Label.TextProperty, vm => vm.Reason);

			stackLayout.Children.Add (new LargeLabel {
				Text = "Clean for "
			});

			stackLayout.Children.Add (daysLabel);

			stackLayout.Children.Add (new LargeLabel {
				Text = "days...due to"
			});

			stackLayout.Children.Add (reasonLabel);

			return stackLayout;
		}
	}
}

