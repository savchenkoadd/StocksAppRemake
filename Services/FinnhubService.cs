using Entities;
using RepositoryContracts;
using ServiceContracts;
using System.Text.Json;

namespace Services
{
	public class FinnhubService : IFinnhubService
	{
		private readonly IFinnhubRepository _finnhubRepository;

        public FinnhubService(
				IFinnhubRepository finnhubRepository
			)
        {
            _finnhubRepository = finnhubRepository;
        }

		public async Task<Dictionary<string, object>?> GetCompanyProfile(string? stockSymbol)
		{
			if (string.IsNullOrEmpty(stockSymbol))
			{
				throw new ArgumentNullException("stockSymbol cannot be null or empty");
			}

			return await _finnhubRepository.GetCompanyProfile(stockSymbol);
		}

		public async Task<Dictionary<string, object>?> GetStockPriceQuote(string? stockSymbol)
		{
			if (string.IsNullOrEmpty(stockSymbol))
			{
				throw new ArgumentNullException("stockSymbol cannot be null or empty");
			}

			return await _finnhubRepository.GetStockPriceQuote(stockSymbol);
		}

		public async Task<List<Dictionary<string, string>>?> GetStocks()
		{
			return await _finnhubRepository.GetStocks();
		}

		public async Task<Dictionary<string, object>?> SearchStocks(string? stockSymbolToSearch)
		{
			if (stockSymbolToSearch is null)
			{
				throw new ArgumentNullException();
			}
			if (stockSymbolToSearch.Length == 0)
			{
				throw new ArgumentException();
			}

			return await _finnhubRepository.SearchStocks(stockSymbolToSearch);
		}
	}
}