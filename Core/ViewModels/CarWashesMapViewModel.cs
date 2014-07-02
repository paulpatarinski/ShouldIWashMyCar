using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;

namespace Core
{
  public class CarWashesMapViewModel
  {
    public CarWashesMapViewModel(IGooglePlacesService googlePlacesService,
      Position currentPosition)
    {
      _googlePlacesService = googlePlacesService;
      _currentPosition = currentPosition;
    }

    private readonly IGooglePlacesService _googlePlacesService;
    private readonly Position _currentPosition;


    public async Task<List<Pin>> GetMapPinsAsync()
    {
      var carWashPins = new List<Pin>();

      var carWashes = await _googlePlacesService.GetCarWashesAsync(_currentPosition);

      foreach (var carWash in carWashes)
      {
        var carWashPin = new Pin
        {
          Type = PinType.SavedPin,
          Position = new Position(carWash.geometry.location.lat, carWash.geometry.location.lng),
          Label = carWash.name,
          Address = carWash.vicinity
        };

        carWashPins.Add(carWashPin);
      }

      return carWashPins;
    }
  }
}