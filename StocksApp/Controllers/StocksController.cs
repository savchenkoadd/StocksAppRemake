using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Options;
using ServiceContracts;
using StocksApp.ViewModels;

namespace StocksApp.Controllers
{
	[Route("[controller]")]
	public class StocksController : Controller
	{
		private readonly IFinnhubService _finnhubService;
		private readonly IStocksService _stocksService;
		private readonly TradingOptions _tradeOptions;
		private readonly ILogger<StocksController> _logger;	

		public StocksController(
				IFinnhubService finnhubService,
				IStocksService stocksService,
				ILogger<StocksController> logger,
				IOptions<TradingOptions> tradeOptions
			)
		{
			_finnhubService = finnhubService;
			_stocksService = stocksService;
			_logger = logger;
			_tradeOptions = tradeOptions.Value;
		}

		[HttpGet]
		[Route("/")]
		[Route("[action]/{stock?}")]
		[Route("~/[action]/{stock?}")]
		public async Task<IActionResult> Explore(string? stock, bool showAll = false)
		{
			List<Dictionary<string, string>>? stocksDictionary = await _finnhubService.GetStocks();

			List<ViewModels.Stock> stocks = new List<ViewModels.Stock>();

			if (stocksDictionary is not null)
			{
				//filter stocks
				if (!showAll && _tradeOptions.Top25PopularStocks != null)
				{
					string[]? Top25PopularStocksList = await ParseStocks(_tradeOptions.Top25PopularStocks);
					if (Top25PopularStocksList is not null)
					{
						stocksDictionary = stocksDictionary
						 .Where(temp => Top25PopularStocksList.Contains(Convert.ToString(temp["symbol"])))
						 .ToList();
					}
				}

				//convert dictionary objects into Stock objects
				stocks = stocksDictionary
				 .Select(temp => new ViewModels.Stock() { StockName = Convert.ToString(temp["description"]), StockSymbol = Convert.ToString(temp["symbol"]) })
				.ToList();
			}

			ViewBag.stock = stock;
			return View(stocks);
		}

		private async Task<string[]> ParseStocks(string stocks)
		{
			return await Task.Run(() =>
			{
				return stocks.Split(',');
			});
		}
	}
}
