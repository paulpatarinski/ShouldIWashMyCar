using NUnit.Framework;
using Xamarin.Forms.Labs.Services.Geolocation;
using System.Collections.Generic;

namespace Core.Test
{
	[TestFixture]
	public class GooglePlacesServiceTest
	{
		[Test]
		public async void GetCarWashesAsync_ShouldReturnCarWashes ()
		{
			var googlePlacesService = new GooglePlacesService (new System.Net.Http.HttpClient ());

			var location = new GeoLocation {
				Latitude = 41.890969, Longitude = -87.676392 
			};

			var resultTask = googlePlacesService.GetCarWashesAsync (location).Result;

			var places = resultTask as List<Place>;

			Assert.IsNotNull (places);
		}
	}
}

