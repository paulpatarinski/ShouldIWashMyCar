using Xamarin.Forms;
using System.Collections.Generic;

namespace Core
{
	public class MenuPage : ContentPage
	{
		readonly List<OptionItem> OptionItems = new List<OptionItem> ();

		public ListView Menu { get; set; }

		public MenuPage ()
		{
			this.BackgroundColor = Color.FromHex ("232323");

			OptionItems.Add (new ForecastOptionItem ());

			var stackLayout = new StackLayout {
				Spacing = 5,
				VerticalOptions = LayoutOptions.FillAndExpand,

			};

			var headerLabel = new Label {
				TextColor = Color.FromHex ("838383"),
				BackgroundColor = Color.FromHex ("474646"),
				Text = "BROWSE", 
			};

			Device.OnPlatform (iOS: () => headerLabel.Font = Font.SystemFontOfSize (NamedSize.Medium),
				Android: () => headerLabel.Font = Font.SystemFontOfSize (NamedSize.Large));

			var headerContentView = new ContentView {
				Content = headerLabel
			};

			Menu = new ListView {
				ItemsSource = OptionItems,
				VerticalOptions = LayoutOptions.FillAndExpand
			};

			var cell = new DataTemplate (typeof(TextCell));
			cell.SetBinding (TextCell.TextProperty, "Title");
			cell.SetBinding (ImageCell.ImageSourceProperty, "IconSource");

			Menu.ItemTemplate = cell;

			stackLayout.Children.Add (headerContentView);
			stackLayout.Children.Add (Menu);

			Content = stackLayout;
		}
	}

}


