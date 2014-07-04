using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Core
{
	public class CarWashesMapPage : ContentPage
	{
		private readonly CarWashesMapViewModel _viewModel;

		public CarWashesMapPage (GeoLocation location)
		{
			//Todo : inject the service
			_viewModel = new CarWashesMapViewModel (new GooglePlacesService (new HttpClient ()), location);

			Init (location);
		}

		private async Task Init (GeoLocation geoLocation)
		{
			var location = new Position (geoLocation.Latitude, geoLocation.Longitude);

			var map = new Map (MapSpan.FromCenterAndRadius (location, Distance.FromMiles (5))) {
				IsShowingUser = true
			};
      
			//iOS displays the current location by default for other platforms add it
			if (Device.OS != TargetPlatform.iOS) {
				map.Pins.Add (new Pin {
					Type = PinType.Generic,
					Position = location,
					Label = "Current Location"
				});
			}

			//Add the car wash pins
			Task.Run (async () => {
				var carWashPins = await _viewModel.GetMapPinsAsync ();

				Device.BeginInvokeOnMainThread (() => {
					foreach (var carWashPin in carWashPins) {
						map.Pins.Add (carWashPin);
					}

				});
			});

			var stack = new StackLayout { Spacing = 0 };

			map.VerticalOptions = LayoutOptions.FillAndExpand;

			stack.Children.Add (map);
			Content = stack;
		}
	}
}