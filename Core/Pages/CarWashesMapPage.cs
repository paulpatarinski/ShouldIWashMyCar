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

			var map = new CustomMap (MapSpan.FromCenterAndRadius (location, Distance.FromMiles (5))) {
				IsShowingUser = true
			};
      
			//Show current location for Win Phone
			if (Device.OS == TargetPlatform.WinPhone) {
				map.CustomPins.Add (new CustomPin {
					Position = location,
					Label = "Current Location"
				});
			}


			//Add the car wash pins
			Task.Run (async () => {
				var carWashPins = await _viewModel.GetMapPinsAsync ();

				Device.BeginInvokeOnMainThread (() => {
					foreach (var carWashPin in carWashPins) {
						map.CustomPins.Add (carWashPin);
					}

				});
			});

			var selectedPinLabel = new LargeLabel { HorizontalOptions = LayoutOptions.Center, TextColor = Color.Black };

			selectedPinLabel.BindingContext = map;
			selectedPinLabel.SetBinding<CustomMap> (Label.TextProperty, vm => vm.SelectedPin.Label);

			var selectedPinAddress = new Label { HorizontalOptions = LayoutOptions.Center, TextColor = Color.Black };

			selectedPinAddress.BindingContext = map;
			selectedPinAddress.SetBinding<CustomMap> (Label.TextProperty, vm => vm.SelectedPin.Address);

			var stack = new StackLayout { Spacing = 0,  BackgroundColor = Color.FromHex ("#ecf0f1") };

			map.VerticalOptions = LayoutOptions.FillAndExpand;

			stack.Children.Add (map);
			stack.Children.Add (selectedPinLabel);
			stack.Children.Add (selectedPinAddress);

			Content = stack;
		}
	}
}