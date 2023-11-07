using RepositoryContracts;
using ServiceContracts;

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

		public async Task<Dictionary<string, object>?> SearchStocks(string? stockNameToSearch)
		{
			if (stockNameToSearch is null)
			{
				throw new ArgumentNullException("stockNameToSearch cannot be null");
			}

			return await _finnhubRepository.SearchStocks(stockNameToSearch);
		}
	}
}