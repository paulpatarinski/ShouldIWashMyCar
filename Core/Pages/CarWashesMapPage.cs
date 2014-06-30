using System;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Core
{
	public class CarWashesMapPage : ContentPage
	{
		public CarWashesMapPage (Xamarin.Forms.Labs.Services.Geolocation.Position location)
		{
			this.location = location;
			var map = MakeMap ();
			var stack = new StackLayout { Spacing = 0 };

			map.VerticalOptions = LayoutOptions.FillAndExpand;

			stack.Children.Add (map);
			Content = stack;
		}

		readonly Xamarin.Forms.Labs.Services.Geolocation.Position location;

		List<Place> GetCarWashes ()
		{
			var googlePlacesService = new GooglePlacesService (new System.Net.Http.HttpClient ());

			var carWashesTask = googlePlacesService.GetCarWashesAsync (location);
			carWashesTask.Wait ();
			return carWashesTask.Result;
		}

		Map MakeMap ()
		{
			// TODO: Uncomment once Xamarin.Forms supports this, hopefully w/ version 1.1.
			//var dict = pins.Zip(ViewModel.Models, (p, m)=>new KeyValuePair<Pin,T>(p, m)).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
			//PinMap = dict;


			//TODO : Move this to the viewmodel
			var carWashes = GetCarWashes ();

			var pin = new Pin ();
			pin.Type = PinType.Generic;
			pin.Position = new Position (location.Latitude, location.Longitude);
			pin.Label = "Current Location";

			var map = new Map (MapSpan.FromCenterAndRadius (pin.Position, Distance.FromMiles (2))) {
				IsShowingUser = true
			};

			map.Pins.Add (pin);

			foreach (var carWash in carWashes) {
				var carWashPin = new Pin ();
				pin.Type = PinType.SearchResult;
				pin.Position = new Position (carWash.geometry.location.lat, carWash.geometry.location.lng);
				pin.Label = carWash.name;
				pin.Address = carWash.vicinity;

				map.Pins.Add (carWashPin);
			}

			// TODO: Uncomment once Xamarin.Forms supports this, hopefully w/ version 1.1.
			//            map.PinSelected += (sender, args)=>
			//            {
			//                var pin = args.SelectedItem as Pin;
			//                var details = PinMap[pin];
			//                var page = new DetailPage<T>(details);
			//                Navigation.PushAsync(page);
			//            };

			return map;
		}

	}
}

