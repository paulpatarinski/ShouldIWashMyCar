using System.Net.Http;
using Xamarin.Forms;

namespace Core
{
	public class CarWashesMapPage : ContentPage
	{
		public CarWashesMapPage (Xamarin.Forms.Labs.Services.Geolocation.Position location)
		{
      //Todo : inject the service
		  var viewModel = new CarWashesMapViewModel(new GooglePlacesService(new HttpClient()), location);

      var map = viewModel.GetMap();
      
			var stack = new StackLayout { Spacing = 0 };

			map.VerticalOptions = LayoutOptions.FillAndExpand;

			stack.Children.Add (map);
			Content = stack;
		}
	}
}