using System.ComponentModel.DataAnnotations;

namespace Entities
{
	public class SellOrder
	{
		[Key]
		public Guid OrderId { get; set; }
		[StringLength(7)]
		public string StockSymbol { get; set; }
		[StringLength(20)]
		public DateTime OrderDateAndTime { get; set; }
		public uint Quantity { get; set; }
		public double Price { get; set; }
	}
}
