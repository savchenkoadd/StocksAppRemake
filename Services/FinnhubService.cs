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

        public Task<Dictionary<string, object>?> GetCompanyProfile(string? stockSymbol)
		{
			throw new NotImplementedException();
		}

		public Task<Dictionary<string, object>?> GetStockPriceQuote(string? stockSymbol)
		{
			throw new NotImplementedException();
		}

		public Task<List<Dictionary<string, string>>?> GetStocks()
		{
			throw new NotImplementedException();
		}

		public Task<Dictionary<string, object>?> SearchStocks(string? stockNameToSearch)
		{
			throw new NotImplementedException();
		}
	}
}