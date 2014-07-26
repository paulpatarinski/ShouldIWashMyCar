using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;

namespace Core
{
	public class CarWashesMapViewModel
	{
		public CarWashesMapViewModel (IGooglePlacesService googlePlacesService,
		                              GeoLocation currentPosition)
		{
			_googlePlacesService = googlePlacesService;
			_currentPosition = currentPosition;
		}

		private readonly IGooglePlacesService _googlePlacesService;
		private readonly GeoLocation _currentPosition;


		public async Task<List<CustomPin>> GetMapPinsAsync ()
		{
			var carWashPins = new List<CustomPin> ();

			var carWashes = await _googlePlacesService.GetCarWashesAsync (_currentPosition);

			foreach (var carWash in carWashes) {
				var carWashPin = new CustomPin {
					Position = new Position (carWash.geometry.location.lat, carWash.geometry.location.lng),
					Label = carWash.name,
					Address = carWash.vicinity,
					PinIcon = "CarWashMapIcon"
				};

				carWashPins.Add (carWashPin);
			}

			return carWashPins;
		}
	}
}