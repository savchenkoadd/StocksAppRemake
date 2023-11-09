using System.ComponentModel.DataAnnotations;

namespace Entities
{
	public class Stock
	{
		[Key]
		public Guid Id { get; set; }
		[StringLength(15)]
		public string StockName { get; set; }
		[StringLength(10)]
		public string StockSymbol { get; set; }
	}
}
