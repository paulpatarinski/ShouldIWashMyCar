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
					new RowDefinition{ Height = new GridLength (0.2, GridUnitType.Star) },
					new RowDefinition{ Height = new GridLength (0.4, GridUnitType.Star) },
					new RowDefinition{ Height = new GridLength (0.4, GridUnitType.Star) }
				},
				ColumnDefinitions = new ColumnDefinitionCollection{ new ColumnDefinition{ Width = new GridLength (1, GridUnitType.Star) } }
			};

			var forecastListview = new ListView ();

			var forecastListviewItemTemplate = new DataTemplate (typeof(ImageCell));

			forecastListviewItemTemplate.SetBinding (ImageCell.TextProperty, "ItemTemplateTextProperty");
			forecastListviewItemTemplate.SetBinding (ImageCell.DetailProperty, "ItemTemplateDetailProperty");
			forecastListviewItemTemplate.SetBinding (ImageCell.ImageSourceProperty, "Icon");

			forecastListview.ItemTemplate = forecastListviewItemTemplate;
			forecastListview.SetBinding<ForecastViewModel> (ListView.ItemsSourceProperty, vm => vm.WeatherList);

			var carImage = new Image {
				Source = "CarSideView",
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				VerticalOptions = LayoutOptions.Start
			};

			masterGrid.Children.Add (CreateForecastStatusStackLayout (), 0, 0);
			masterGrid.Children.Add (carImage, 0, 1);
			masterGrid.Children.Add (forecastListview, 0, 2);

			Content = masterGrid;
		}

		StackLayout CreateForecastStatusStackLayout ()
		{
			var daysLabel = new ExtraLargeLabel { HorizontalOptions = LayoutOptions.Center };
			daysLabel.SetBinding<ForecastViewModel> (Label.TextProperty, vm => vm.DaysClean);

			var daysTextLabel = new LargeLabel { VerticalOptions = LayoutOptions.CenterAndExpand };
			daysTextLabel.SetBinding<ForecastViewModel> (Label.TextProperty, vm => vm.DaysText);

			var horizontalStackLayout = new StackLayout {
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Start,
				Padding = new Thickness (0, 0, 10, 0)
			};

			horizontalStackLayout.Children.Add (new LargeLabel {
				Text = "Clean for ",
				VerticalOptions = LayoutOptions.CenterAndExpand
			});
			horizontalStackLayout.Children.Add (daysLabel);
			horizontalStackLayout.Children.Add (daysTextLabel);

			return horizontalStackLayout;
		}
	}
}