using System;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace ShouldIWashMyCar
{
	public static class HttpClientExtensions
	{
		public static async Task<T> GetAsync<T>(this HttpClient client, string url)
		{
			var httpRequest = new HttpRequestMessage (new HttpMethod ("GET"), url);

			var response = await client.SendAsync (httpRequest);

			var jsonString = response.Content.ReadAsStringAsync ();

			while (jsonString.Result == null) {
				Task.Delay (TimeSpan.FromMilliseconds (1));
			}

			var result = JsonConvert.DeserializeObject<T> (jsonString.Result);


			return result;


		}
	}
}

