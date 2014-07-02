using Xamarin.Forms.Maps;

namespace Core
{
  public class CarWashesMapViewModel
  {
    public CarWashesMapViewModel(IGooglePlacesService googlePlacesService,
      Xamarin.Forms.Labs.Services.Geolocation.Position currentPosition)
    {
      _googlePlacesService = googlePlacesService;
      _currentPosition = currentPosition;
    }

    private readonly IGooglePlacesService _googlePlacesService;
    private readonly Xamarin.Forms.Labs.Services.Geolocation.Position _currentPosition;


    public Map GetMap()
    {

      var currentLocationPin = new Pin();
      currentLocationPin.Type = PinType.Generic;
      currentLocationPin.Position = new Position(_currentPosition.Latitude, _currentPosition.Longitude);
      currentLocationPin.Label = "Current Location";

      var map = new Map(MapSpan.FromCenterAndRadius(currentLocationPin.Position, Distance.FromMiles(5)))
      {
        IsShowingUser = true
      };

      var carWashes = _googlePlacesService.GetCarWashesAsync(_currentPosition).Result;

      map.Pins.Add(currentLocationPin);

      foreach (var carWash in carWashes)
      {
        var carWashPin = new Pin
        {
          Type = PinType.SavedPin,
          Position = new Position(carWash.geometry.location.lat, carWash.geometry.location.lng),
          Label = carWash.name,
          Address = carWash.vicinity
        };

        map.Pins.Add(carWashPin);
      }

      return map;
    }
  }
}