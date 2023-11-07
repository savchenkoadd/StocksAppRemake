using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Options;
using Rotativa.AspNetCore;
using ServiceContracts;
using ServiceContracts.DTO;
using StocksApp.ViewModels;

namespace StocksApp.Controllers
{
	[Route("[controller]")]
	public class TradeController : Controller
	{
		private readonly IFinnhubService _finnhubService;
		private readonly IStocksService _stocksService;
		private readonly TradingOptions _tradingOptions;
		private readonly FinnhubOptions _finnhubOptions;

		public TradeController(
				IOptions<TradingOptions> tradingOptions,
				IOptions<FinnhubOptions> finnhubOptions,
				IFinnhubService finnhubService,
				IStocksService stocksService
			)
		{
			_tradingOptions = tradingOptions.Value;
			_finnhubOptions = finnhubOptions.Value;
			_finnhubService = finnhubService;
			_stocksService = stocksService;
		}

		[HttpGet]
		[Route("[action]/{ticker?}")]
		public async Task<IActionResult> Index(string? ticker)
		{
			if (string.IsNullOrEmpty(ticker))
			{
				ticker = _finnhubOptions.DefaultStockSymbol;
			}

			var companyProfile = await _finnhubService.GetCompanyProfile(ticker);
			var stockPriceQuote = await _finnhubService.GetStockPriceQuote(ticker);

			StockTrade viewModel = new StockTrade()
			{
				Price = (int)stockPriceQuote["c"],
				Quantity = _tradingOptions.DefaultOrderQuantity,
				StockName = companyProfile["name"].ToString(),
				StockSymbol = companyProfile["ticker"].ToString(),
			};

			return View(viewModel);
		}

		[HttpGet]
		[Route("[action]")]
		public async Task<IActionResult> Orders()
		{
			Orders orders = new Orders()
			{
				BuyOrders = await _stocksService.GetBuyOrders(),
				SellOrders = await _stocksService.GetSellOrders()
			};

			return View(orders);
		}

		[HttpGet]
		[Route("[action]")]
		public async Task<IActionResult> OrdersPDF()
		{
			Orders orders = new Orders()
			{
				BuyOrders = await _stocksService.GetBuyOrders(),
				SellOrders = await _stocksService.GetSellOrders()
			};

			return new ViewAsPdf(viewName: "PersonsPDF", orders, ViewData);
		}

		[HttpPost]
		[Route("[action]")]
		public async Task<IActionResult> BuyOrder(BuyOrderRequest? buyOrderRequest)
		{
			if (buyOrderRequest is null)
			{
				return RedirectToAction("Index");
			}

			await _stocksService.CreateBuyOrder(buyOrderRequest);

			return RedirectToAction("Index");
		}

		[HttpPost]
		[Route("[action]")]
		public async Task<IActionResult> SellOrder(SellOrderRequest? sellOrderRequest)
		{
			if (sellOrderRequest is null)
			{
				return RedirectToAction("Index");
			}

			await _stocksService.CreateSellOrder(sellOrderRequest);

			return RedirectToAction("Thanks");
		}
	}
}
