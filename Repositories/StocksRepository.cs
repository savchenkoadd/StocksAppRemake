﻿using Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;

namespace Repositories
{
	public class StocksRepository : IStocksRepository
	{
		private readonly StockMarketDbContext _dbContext;

		public StocksRepository(StockMarketDbContext stockMarketDbContext)
		{
			_dbContext = stockMarketDbContext;
		}

		public async Task<BuyOrder> CreateBuyOrder(BuyOrder buyOrder)
		{
			await _dbContext.BuyOrders.AddAsync(buyOrder);
			await _dbContext.SaveChangesAsync();

			return buyOrder;
		}

		public async Task<SellOrder> CreateSellOrder(SellOrder sellOrder)
		{
			await _dbContext.SellOrders.AddAsync(sellOrder);
			await _dbContext.SaveChangesAsync();

			return sellOrder;
		}

		public async Task<List<BuyOrder>> GetBuyOrders()
		{
			return await _dbContext.BuyOrders.ToListAsync();
		}

		public async Task<List<SellOrder>> GetSellOrders()
		{
			return await _dbContext.SellOrders.ToListAsync();
		}
	}
}
