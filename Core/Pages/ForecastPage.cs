using System;
using Xamarin.Forms;
using Core.Models;

namespace Core
{
	public class ForecastPage : ContentPage
	{
		public ForecastPage (Forecast forecast)
		{
			BindingContext = new ForecastViewModel (Navigation, forecast);

			NavigationPage.SetHasNavigationBar (this, false);

			var masterGrid = new Grid {
				RowDefinitions = new RowDefinitionCollection {
					new RowDefinition{ Height = new GridLength (0.5, GridUnitType.Star) },
					new RowDefinition{ Height = new GridLength (0.5, GridUnitType.Star) }
				},
				ColumnDefinitions = new ColumnDefinitionCollection{ new ColumnDefinition{ Width = new GridLength (1, GridUnitType.Star) } }
			};

			var forecastListview = new ListView ();

			var forecastListviewItemTemplate = new DataTemplate (typeof(TextCell));

			forecastListviewItemTemplate.SetBinding (TextCell.TextProperty, "WeatherCondition");
			forecastListviewItemTemplate.SetBinding (TextCell.DetailProperty, "TempHigh");

			forecastListview.ItemTemplate = forecastListviewItemTemplate;
			forecastListview.SetBinding<ForecastViewModel> (ListView.ItemsSourceProperty, vm => vm.WeatherList);

			masterGrid.Children.Add (CreateForecastStatusStackLayout (), 0, 0);
			masterGrid.Children.Add (forecastListview, 0, 1);

			Content = masterGrid;
		}

		StackLayout CreateForecastStatusStackLayout ()
		{
			var stackLayout = new StackLayout {
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand
			};

			var daysLabel = new LargeLabel ();
			daysLabel.SetBinding<ForecastViewModel> (Label.TextProperty, vm => vm.DaysClean);

			var reasonLabel = new LargeLabel ();
			reasonLabel.SetBinding<ForecastViewModel> (Label.TextProperty, vm => vm.Reason);

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