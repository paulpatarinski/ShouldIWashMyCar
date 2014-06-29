using System;
using Xamarin.Forms;
using System.Linq;
using Core.Models;
using System.Threading.Tasks;

namespace Core
{
	public class RootPage : MasterDetailPage
	{
		public RootPage ()
		{
			NavigationPage.SetHasNavigationBar (this, false);

			var optionsPage = new MenuPage { Title = "Menu" };

			optionsPage.Menu.ItemSelected += (sender, e) => NavigateTo (e.SelectedItem as OptionItem, null);

			Master = optionsPage;
			Detail = new ContentPage ();

			ShowLoadingDialog ();
		}

		public async Task ShowLoadingDialog ()
		{
			var page = new LoadingPage (this);
			await Navigation.PushModalAsync (page);
		}

		OptionItem previousItem;
		public Forecast WeatherForecast;

		public void NavigateTo (OptionItem option, object parameters)
		{
			if (previousItem != null)
				previousItem.Selected = false;

			option.Selected = true;
			previousItem = option;
			Title = option.Title;


			if (Device.OS == TargetPlatform.WinPhone) {
				Detail = new ContentPage ();//work around to clear current page.
			}

			Detail = PageForOption (option, parameters);

			IsPresented = false;
		}

		Page PageForOption (OptionItem option, object parameters)
		{
			if (option.Title == "Forecast" && parameters == null) {
				ShowLoadingDialog ();
				return new ContentPage ();
			}

			if (option.Title == "Forecast")
				return new ForecastPage (this, (Forecast)parameters);

			throw new NotImplementedException ("Unknown menu option: " + option.Title);
		}
	}
}

