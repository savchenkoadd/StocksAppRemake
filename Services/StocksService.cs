using Entities;
using Microsoft.IdentityModel.Tokens;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO;
using Services.Helpers;

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

		public async Task<BuyOrderResponse> CreateBuyOrder(BuyOrderRequest? buyOrderRequest)
		{
			ValidationHelper.Validate(buyOrderRequest);

			var response = await _stocksRepository.CreateBuyOrder(buyOrderRequest!.ToBuyOrder());

			return ToBuyOrderResponse(response);
		}

		public async Task<SellOrderResponse> CreateSellOrder(SellOrderRequest? sellOrderRequest)
		{
			ValidationHelper.Validate(sellOrderRequest);

			var response = await _stocksRepository.CreateSellOrder(sellOrderRequest!.ToSellOrder());

			return ToSellOrderResponse(response);
		}

		public async Task<List<BuyOrderResponse>> GetBuyOrders()
		{
			return (await _stocksRepository.GetBuyOrders()).Select(x => ToBuyOrderResponse(x)).ToList();
		}

		public async Task<List<SellOrderResponse>> GetSellOrders()
		{
			return (await _stocksRepository.GetSellOrders()).Select(x => ToSellOrderResponse(x)).ToList();
		}

		private BuyOrderResponse ToBuyOrderResponse(BuyOrder buyOrder)
		{
			return new BuyOrderResponse()
			{
				OrderDateAndTime = buyOrder.OrderDateAndTime,
				OrderId = buyOrder.OrderId,
				Price = buyOrder.Price,
				Quantity = buyOrder.Quantity,
				StockName = buyOrder.StockName,
				StockSymbol = buyOrder.StockSymbol,
				TradeAmount = buyOrder.Price * buyOrder.Quantity
			};
		}

		private SellOrderResponse ToSellOrderResponse(SellOrder sellOrder)
		{
			return new SellOrderResponse()
			{
				OrderDateAndTime = sellOrder.OrderDateAndTime,
				OrderId = sellOrder.OrderId,
				Price = sellOrder.Price,
				Quantity = sellOrder.Quantity,
				StockName = sellOrder.StockName,
				StockSymbol = sellOrder.StockSymbol,
				TradeAmount = sellOrder.Price * sellOrder.Quantity
			};
		}
	}
}
