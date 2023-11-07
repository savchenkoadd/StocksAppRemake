using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Options;
using ServiceContracts;
using StocksApp.ViewModels;

namespace StocksApp.Controllers
{
	[Route("[controller]")]
	public class StocksController : Controller
	{
		private readonly IFinnhubService _finnhubService;
		private readonly TradingOptions _tradeOptions;

		private List<string> _topStocks = new List<string>();

		public StocksController(
				IFinnhubService finnhubService,
				IOptions<TradingOptions> tradeOptions
			)
		{
			_finnhubService = finnhubService;
			_tradeOptions = tradeOptions.Value;
		}
	}
}
