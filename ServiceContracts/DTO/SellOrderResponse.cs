using Entities;

namespace ServiceContracts.DTO
{
	public class SellOrderResponse
	{
		public Guid OrderId { get; set; }

		public string StockSymbol { get; set; }

		public string StockName { get; set; }

		public DateTime OrderDateAndTime { get; set; }

		public uint Quantity { get; set; }

		public double Price { get; set; }

		public double TradeAmount { get; set; }
	}

	public static partial class OrderExtensions
	{
		public static SellOrder ToSellOrder(this SellOrderResponse response)
		{
			return new SellOrder()
			{
				OrderDateAndTime = response.OrderDateAndTime,
				Quantity = response.Quantity,
				Price = response.Price,
				OrderId = response.OrderId,
				StockSymbol = response.StockSymbol,
				StockName = response.StockName,
			};
		}
	}
}
