using Entities;
using Microsoft.EntityFrameworkCore;

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

			var app = builder.Build();


			app.MapGet("/", () => "Hello World!");

			app.Run();
		}
	}
}