using System;
using Core.Models;
using System.Net.Http;
using ShouldIWashMyCar;
using System.Threading.Tasks;
using Xamarin.Forms.Labs.Services.Geolocation;

namespace Core.Services
{
	public interface IOpenWeatherMapService
	{
		Task<OpenWeatherForecast> Get7DayForecastAsync (Position location);
	}

}