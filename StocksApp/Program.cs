using Entities;
using Microsoft.EntityFrameworkCore;
using Options;

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

			var app = builder.Build();


			app.MapGet("/", () => "Hello World!");

			app.Run();
		}
	}
}