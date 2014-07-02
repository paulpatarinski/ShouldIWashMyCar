using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms.Labs.Services.Geolocation;

namespace Core
{
  public interface IGooglePlacesService
  {
    Task<List<Place>> GetCarWashesAsync (Position location);
  }
}