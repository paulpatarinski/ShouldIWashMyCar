using System;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ShouldIWashMyCar
{
	public static class HttpClientExtensions
	{
		public static async Task<T> GetAsync<T> (this HttpClient client, string url)
		{
			var httpRequest = new HttpRequestMessage (new HttpMethod ("GET"), url);

			client.Timeout = TimeSpan.FromSeconds (30);

			var response = await client.SendAsync (httpRequest);

			var jsonString = await response.Content.ReadAsStringAsync ();

			var result = JsonConvert.DeserializeObject<T> (jsonString);

			return result;
		}

		public static async Task<string> GetStringAsync (this HttpClient client, string url)
		{
			var httpRequest = new HttpRequestMessage (new HttpMethod ("GET"), url);

			var response = client.SendAsync (httpRequest).Result;

			var jsonString = await response.Content.ReadAsStringAsync ();

			return jsonString;
		}
	}
}