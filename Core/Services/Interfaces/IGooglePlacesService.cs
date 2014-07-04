using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core
{
	public interface IGooglePlacesService
	{
		Task<List<Place>> GetCarWashesAsync (GeoLocation location);
	}
}