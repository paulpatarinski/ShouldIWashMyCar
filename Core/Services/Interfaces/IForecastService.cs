using Core.Models;
using ShouldIWashMyCar;
using System.Threading.Tasks;

namespace Core.Services
{
	public interface IForecastService
	{
		Task<Forecast> GetForecastAsync (Location location);
	}

}
