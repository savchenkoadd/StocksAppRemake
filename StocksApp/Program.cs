using Entities;
using Microsoft.EntityFrameworkCore;
using Options;
using Repositories;
using RepositoryContracts;
using ServiceContracts;
using Services;

namespace StocksApp
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddDbContext<StockMarketDbContext>(
					options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")
				));

			builder.Services.Configure<FinnhubOptions>(
					builder.Configuration.GetSection("FinnhubOptions")
				);

			builder.Services.AddHttpClient();
			builder.Services.AddScoped<IFinnhubService, FinnhubService>();
			builder.Services.AddScoped<IStocksService, StocksService>();
			builder.Services.AddScoped<IFinnhubRepository, FinnhubRepository>();
			builder.Services.AddScoped<IStocksRepository, StocksRepository>();

			var app = builder.Build();


			app.MapGet("/", () => "Hello World!");

			app.Run();
		}
	}
}