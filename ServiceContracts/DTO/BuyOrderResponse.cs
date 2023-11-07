namespace ServiceContracts.DTO
{
	public class BuyOrderResponse
	{
		public Guid OrderId { get; set; }

		public string StockSymbol { get; set; }

		public string StockName { get; set; }

		public DateTime OrderDateAndTime { get; set; }

		public uint Quantity { get; set; }

		public double Price { get; set; }

		public double TradeAmount { get; set; }
	}
}
