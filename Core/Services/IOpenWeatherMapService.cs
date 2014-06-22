using System;
using Core.Models;
using System.Net.Http;
using ShouldIWashMyCar;
using System.Threading.Tasks;

namespace Core.Services
{
	public interface IOpenWeatherMapService
	{
		Task<OpenWeatherForecast> Get7DayForecastAsync (Location location);
	}

}