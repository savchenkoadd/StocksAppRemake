using Moq;
using RepositoryContracts;
using ServiceContracts;
using Services;
using FluentAssertions;
using AutoFixture;
using ServiceContracts.DTO;
using Entities;

namespace Tests
{
	public class StocksServiceTests
	{
		private readonly IStocksService _stockService;

        private readonly IStocksRepository _stocksRepository;
        private readonly Mock<IStocksRepository> _stocksRepositoryMock;

		private readonly IFixture _fixture;

        public StocksServiceTests()
        {
            _stocksRepositoryMock = new Mock<IStocksRepository>();
            _stocksRepository = _stocksRepositoryMock.Object;

            _stockService = new StocksService(_stocksRepository);

			_fixture = new Fixture();
        }


		#region CreateBuyOrder

		[Fact]
        public async Task CreateBuyOrder_NullRequest_ToBeArgumentNullException()
        {
            Func<Task> action = async () =>
			{
				await _stockService.CreateBuyOrder(null);
			};

            await action.Should().ThrowAsync<ArgumentNullException>();
        }

		[Fact]
		public async Task CreateBuyOrder_InvalidQuantity_ToBeArgumentException()
		{
			var request1 = _fixture.Build<BuyOrderRequest>()
				.With(temp => temp.Quantity, (uint) 0)
				.Create();
			var request2 = _fixture.Build<BuyOrderRequest>()
				.With(temp => temp.Quantity, (uint) 100001)
				.Create();

			Func<Task> action1 = async () =>
			{
				await _stockService.CreateBuyOrder(request1);
			};
			Func<Task> action2 = async () =>
			{
				await _stockService.CreateBuyOrder(request2);
			};

			await action1.Should().ThrowAsync<ArgumentException>();
			await action2.Should().ThrowAsync<ArgumentException>();
		}

		[Fact]
		public async Task CreateBuyOrder_InvalidPrice_ToBeArgumentException()
		{
			var request1 = _fixture.Build<BuyOrderRequest>()
				.With(temp => temp.Price, 0)
				.Create();
			var request2 = _fixture.Build<BuyOrderRequest>()
				.With(temp => temp.Price, 10001)
				.Create();

			Func<Task> action1 = async () =>
			{
				await _stockService.CreateBuyOrder(request1);
			};
			Func<Task> action2 = async () =>
			{
				await _stockService.CreateBuyOrder(request2);
			};

			await action1.Should().ThrowAsync<ArgumentException>();
			await action2.Should().ThrowAsync<ArgumentException>();
		}

		[Fact]
		public async Task CreateBuyOrder_NullStockSymbol_ToBeArgumentException()
		{
			var request = _fixture.Build<BuyOrderRequest>()
				.With(temp => temp.StockSymbol, null as string)
				.Create();

			Func<Task> action = async () =>
			{
				await _stockService.CreateBuyOrder(request);
			};

			await action.Should().ThrowAsync<ArgumentException>();
		}

		[Fact]
		public async Task CreateBuyOrder_InvalidDateTime_ToBeArgumentException()
		{
			var request = _fixture.Build<BuyOrderRequest>()
				.With(temp => temp.OrderDateTime, new DateTime(1999, 1, 1))
				.Create();

			Func<Task> action = async () =>
			{
				await _stockService.CreateBuyOrder(request);
			};

			await action.Should().ThrowAsync<ArgumentException>();
		}

		[Fact]
		public async Task CreateBuyOrder_Valid_ToBeSuccesful()
		{
			var request = _fixture.Build<BuyOrderRequest>()
				.With(temp => temp.OrderDateTime, new DateTime(2001, 1, 1))
				.With(temp => temp.Quantity, (uint)20)
				.With(temp => temp.Price, 2000)
				.Create();

			var expected = request.ToBuyOrder();

			_stocksRepositoryMock
				.Setup(temp => temp.CreateBuyOrder(It.IsAny<BuyOrder>()))
				.ReturnsAsync(expected);

			var actual = (await _stockService.CreateBuyOrder(request)).ToBuyOrder();

			actual.OrderId = expected.OrderId;

			actual.Should().BeEquivalentTo(expected);
		}

		#endregion


		#region CreateSellOrder

		[Fact]
		public async Task CreateSellOrder_NullRequest_ToBeArgumentNullException()
		{
			Func<Task> action = async () =>
			{
				await _stockService.CreateSellOrder(null);
			};

			await action.Should().ThrowAsync<ArgumentNullException>();
		}

		[Fact]
		public async Task CreateSellOrder_InvalidQuantity_ToBeArgumentException()
		{
			var request1 = _fixture.Build<SellOrderRequest>()
				.With(temp => temp.Quantity, (uint)0)
				.Create();
			var request2 = _fixture.Build<SellOrderRequest>()
				.With(temp => temp.Quantity, (uint)100001)
				.Create();

			Func<Task> action1 = async () =>
			{
				await _stockService.CreateSellOrder(request1);
			};
			Func<Task> action2 = async () =>
			{
				await _stockService.CreateSellOrder(request2);
			};

			await action1.Should().ThrowAsync<ArgumentException>();
			await action2.Should().ThrowAsync<ArgumentException>();
		}

		[Fact]
		public async Task CreateSellOrder_InvalidPrice_ToBeArgumentException()
		{
			var request1 = _fixture.Build<SellOrderRequest>()
				.With(temp => temp.Price, 0)
				.Create();
			var request2 = _fixture.Build<SellOrderRequest>()
				.With(temp => temp.Price, 10001)
				.Create();

			Func<Task> action1 = async () =>
			{
				await _stockService.CreateSellOrder(request1);
			};
			Func<Task> action2 = async () =>
			{
				await _stockService.CreateSellOrder(request2);
			};

			await action1.Should().ThrowAsync<ArgumentException>();
			await action2.Should().ThrowAsync<ArgumentException>();
		}

		[Fact]
		public async Task CreateSellOrder_NullStockSymbol_ToBeArgumentException()
		{
			var request = _fixture.Build<SellOrderRequest>()
				.With(temp => temp.StockSymbol, null as string)
				.Create();

			Func<Task> action = async () =>
			{
				await _stockService.CreateSellOrder(request);
			};

			await action.Should().ThrowAsync<ArgumentException>();
		}

		[Fact]
		public async Task CreateSellOrder_InvalidDateTime_ToBeArgumentException()
		{
			var request = _fixture.Build<SellOrderRequest>()
				.With(temp => temp.OrderDateTime, new DateTime(1999, 1, 1))
				.Create();

			Func<Task> action = async () =>
			{
				await _stockService.CreateSellOrder(request);
			};

			await action.Should().ThrowAsync<ArgumentException>();
		}

		[Fact]
		public async Task CreateSellOrder_Valid_ToBeSuccesful()
		{
			var request = _fixture.Build<SellOrderRequest>()
				.With(temp => temp.OrderDateTime, new DateTime(2001, 1, 1))
				.With(temp => temp.Quantity, (uint)20)
				.With(temp => temp.Price, 2000)
				.Create();

			var expected = request.ToSellOrder();

			_stocksRepositoryMock
				.Setup(temp => temp.CreateSellOrder(It.IsAny<SellOrder>()))
				.ReturnsAsync(expected);

			var actual = (await _stockService.CreateSellOrder(request)).ToSellOrder();

			actual.OrderId = expected.OrderId;

			expected.Should().BeEquivalentTo(actual);
		}

		#endregion


		#region GetBuyOrders

		[Fact]
		public async Task GetBuyOrder_EmptyList_ToBeSuccesful()
		{
			_stocksRepositoryMock
				.Setup(temp => temp.GetBuyOrders())
				.ReturnsAsync(new List<BuyOrder>());

			var actual = await _stockService.GetBuyOrders();

			actual.Should().BeEmpty();
		}

		[Fact]
		public async Task GetBuyOrder_Valid_ToBeSuccesful()
		{
			List<BuyOrder> requests = new List<BuyOrder>()
			{
				_fixture.Build<BuyOrder>()
				.With(temp => temp.Quantity, (uint) 50)
				.With(temp => temp.Price, 2500)
				.With(temp => temp.OrderDateAndTime, new DateTime(2012, 2, 3))
				.Create(),

				_fixture.Build<BuyOrder>()
				.With(temp => temp.Quantity, (uint) 55)
				.With(temp => temp.Price, 2502)
				.With(temp => temp.OrderDateAndTime, new DateTime(2013, 2, 5))
				.Create(),
			};

			var expected = new List<BuyOrderResponse>()
			{
				new BuyOrderResponse()
				{
					OrderId = requests[0].OrderId,
					Quantity = requests[0].Quantity,
					Price = requests[0].Price,
					OrderDateAndTime = requests[0].OrderDateAndTime,
					StockName = requests[0].StockName,
					StockSymbol = requests[0].StockSymbol,
					TradeAmount = requests[0].Quantity * requests[0].Price
				},
				new BuyOrderResponse()
				{
					OrderId = requests[1].OrderId,
					Quantity = requests[1].Quantity,
					Price = requests[1].Price,
					OrderDateAndTime = requests[1].OrderDateAndTime,
					StockName = requests[1].StockName,
					StockSymbol = requests[1].StockSymbol,
					TradeAmount = requests[1].Quantity * requests[1].Price
				}
			};

			_stocksRepositoryMock
				.Setup(temp => temp.GetBuyOrders())
				.ReturnsAsync(requests);

			var responses = await _stockService.GetBuyOrders();

			responses.Should().BeEquivalentTo(expected);
		}

		#endregion


		#region GetSellOrders

		[Fact]
		public async Task GetSellOrder_EmptyList_ToBeSuccesful()
		{
			_stocksRepositoryMock
				.Setup(temp => temp.GetSellOrders())
				.ReturnsAsync(new List<SellOrder>());

			var actual = await _stockService.GetSellOrders();

			actual.Should().BeEmpty();
		}

		[Fact]
		public async Task GetSellOrder_Valid_ToBeSuccesful()
		{
			List<SellOrder> requests = new List<SellOrder>()
			{
				_fixture.Build<SellOrder>()
				.With(temp => temp.Quantity, (uint) 50)
				.With(temp => temp.Price, 2500)
				.With(temp => temp.OrderDateAndTime, new DateTime(2012, 2, 3))
				.Create(),

				_fixture.Build<SellOrder>()
				.With(temp => temp.Quantity, (uint) 55)
				.With(temp => temp.Price, 2502)
				.With(temp => temp.OrderDateAndTime, new DateTime(2013, 2, 5))
				.Create(),
			};

			var expected = new List<SellOrderResponse>()
			{
				new SellOrderResponse()
				{
					OrderId = requests[0].OrderId,
					Quantity = requests[0].Quantity,
					Price = requests[0].Price,
					OrderDateAndTime = requests[0].OrderDateAndTime,
					StockName = requests[0].StockName,
					StockSymbol = requests[0].StockSymbol,
					TradeAmount = requests[0].Quantity * requests[0].Price
				},
				new SellOrderResponse()
				{
					OrderId = requests[1].OrderId,
					Quantity = requests[1].Quantity,
					Price = requests[1].Price,
					OrderDateAndTime = requests[1].OrderDateAndTime,
					StockName = requests[1].StockName,
					StockSymbol = requests[1].StockSymbol,
					TradeAmount = requests[1].Quantity * requests[1].Price
				}
			};

			_stocksRepositoryMock
				.Setup(temp => temp.GetSellOrders())
				.ReturnsAsync(requests);

			var responses = await _stockService.GetSellOrders();

			responses.Should().BeEquivalentTo(expected);
		}

		#endregion
	}
}
