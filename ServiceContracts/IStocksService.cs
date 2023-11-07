using ServiceContracts.DTO;

namespace ServiceContracts
{
	public interface IStocksService
	{
		Task<BuyOrderResponse> CreateBuyOrder(BuyOrderRequest? buyOrder);

		Task<SellOrderResponse> CreateSellOrder(SellOrderRequest? sellOrder);

		Task<List<BuyOrderResponse>> GetBuyOrders();

		Task<List<SellOrderResponse>> GetSellOrders();
	}
}
