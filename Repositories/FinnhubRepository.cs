using Entities;
using Microsoft.Extensions.Options;
using Options;
using RepositoryContracts;
using System.Net.Http;
using System.Text.Json;

namespace Repositories
{
	public class FinnhubRepository : IFinnhubRepository
	{
		private readonly FinnhubOptions _finnhubOptions;
		private readonly IHttpClientFactory _httpClientFactory;

		public FinnhubRepository(
				IOptions<FinnhubOptions> finnhubOptions,
				IHttpClientFactory httpClientFactory
			)
		{
			_finnhubOptions = finnhubOptions.Value;
			_httpClientFactory = httpClientFactory;
		}

		public async Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol)
		{
			using (HttpClient httpClient = _httpClientFactory.CreateClient())
			{
				HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"https://finnhub.io/api/v1/stock/profile2?symbol={stockSymbol}&token={_finnhubOptions.ApiKey}");

				var response = await httpClient.SendAsync(request);

				var responseString = await response.Content.ReadAsStringAsync();

				return JsonSerializer.Deserialize<Dictionary<string, object>>(responseString);
			}
		}

		public async Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol)
		{
			using (HttpClient httpClient = _httpClientFactory.CreateClient())
			{
				HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"https://finnhub.io/api/v1/quote?symbol={stockSymbol}&token={_finnhubOptions.ApiKey}");

				var response = await httpClient.SendAsync(request);

				var responseString = await response.Content.ReadAsStringAsync();

				return JsonSerializer.Deserialize<Dictionary<string, object>>(responseString);
			}
		}

		public async Task<List<Dictionary<string, string>>?> GetStocks()
		{
			using (HttpClient httpClient = _httpClientFactory.CreateClient())
			{
				HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"https://finnhub.io/api/v1/stock/symbol?exchange=US&token={_finnhubOptions.ApiKey}");

				var response = await httpClient.SendAsync(request);

				var responseString = await response.Content.ReadAsStringAsync();

				return JsonSerializer.Deserialize<List<Dictionary<string, string>>>(responseString);
			}
		}

		public async Task<Dictionary<string, object>?> SearchStocks(string stockSymbolToSearch)
		{
			using (HttpClient httpClient = _httpClientFactory.CreateClient())
			{
				HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, $"https://finnhub.io/api/v1/search?q={stockSymbolToSearch}&token={_finnhubOptions.ApiKey}");

				var response = await httpClient.SendAsync(httpRequestMessage);

				var responseAsString = await response.Content.ReadAsStringAsync();

				return JsonSerializer.Deserialize<Dictionary<string, object>>(responseAsString);
			}
		}
	}
}