using ServiceContracts.CustomValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO
{
	internal class BuyOrderRequest
	{
		[Required]
		public string? StockSymbol { get; set; }
		[Required]
		public string? StockName { get; set; }
		[ValidDate]
		public DateTime OrderDateTime { get; set; }

	}
}
