using Core.Models;
using ShouldIWashMyCar;
using System.Threading.Tasks;
using Xamarin.Forms.Labs.Services.Geolocation;

namespace Core.Services
{
	public interface IForecastService
	{
		Task<Forecast> GetForecastAsync (Position location);
	}

}
