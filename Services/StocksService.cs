using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO;

namespace Services
{
	public class StocksService : IStocksService
	{
		private readonly IStocksRepository _stocksRepository;

        public StocksService(
				IStocksRepository stocksRepository
			)
        {
            _stocksRepository = stocksRepository;
        }

        public Task<BuyOrderResponse> CreateBuyOrder(BuyOrderRequest? buyOrder)
		{
			throw new NotImplementedException();
		}

		public Task<SellOrderResponse> CreateSellOrder(SellOrderRequest? sellOrder)
		{
			throw new NotImplementedException();
		}

		public Task<List<BuyOrderResponse>> GetBuyOrders()
		{
			throw new NotImplementedException();
		}

		public Task<List<SellOrderResponse>> GetSellOrders()
		{
			throw new NotImplementedException();
		}
	}
}
