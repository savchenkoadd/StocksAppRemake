using Entities;
using ServiceContracts.CustomValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO
{
	public class SellOrderRequest
	{
		[Required(ErrorMessage = "Stock symbol cannot be null or empty")]
		public string? StockSymbol { get; set; }

		[Required(ErrorMessage = "Stock name cannot be null or empty")]
		public string? StockName { get; set; }

		[ValidDate(ErrorMessage = "Order date and time cannot be older than 01.01.2000")]
		public DateTime OrderDateTime { get; set; }

		[Range(1, 100000, ErrorMessage = "Quantity should be in range of 1 to 100000")]
		public uint Quantity { get; set; }

		[Range(1, 10000, ErrorMessage = "Price should be in range of 1 to 10000")]
		public double Price { get; set; }
	}

	public static class SellOrderExtensions
	{
		public static SellOrder ToSellOrder(this SellOrderRequest sellOrderRequest)
		{
			return new SellOrder
			{
				OrderDateAndTime = sellOrderRequest.OrderDateTime,
				Price = sellOrderRequest.Price,
				Quantity = sellOrderRequest.Quantity,
				StockName = sellOrderRequest.StockName,
				StockSymbol = sellOrderRequest.StockSymbol,
				OrderId = Guid.NewGuid()
			};
		}
	}
}
