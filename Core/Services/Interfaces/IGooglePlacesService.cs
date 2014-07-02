using System.Collections.Generic;
using System.Threading.Tasks;
using Position = Xamarin.Forms.Maps.Position;

namespace Core
{
  public interface IGooglePlacesService
  {
    Task<List<Place>> GetCarWashesAsync (Position location);
  }
}