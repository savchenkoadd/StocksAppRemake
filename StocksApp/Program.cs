using Entities;
using Microsoft.EntityFrameworkCore;
using Options;
using Repositories;
using RepositoryContracts;
using Serilog;
using ServiceContracts;
using Services;

namespace StocksApp
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Host.UseSerilog((HostBuilderContext context, IServiceProvider services, LoggerConfiguration loggerConfiguration) =>
			{
				loggerConfiguration.ReadFrom.Configuration(context.Configuration)
				.ReadFrom.Services(services);
			});

			builder.Services.AddDbContext<StockMarketDbContext>(
					options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")
				));

			builder.Services.Configure<FinnhubOptions>(
					builder.Configuration.GetSection("FinnhubOptions")
				);

			builder.Services.Configure<TradingOptions>(
					builder.Configuration.GetSection("TradingOptions")
				);

			builder.Services.AddControllersWithViews();

			builder.Services.AddHttpClient();
			builder.Services.AddScoped<IFinnhubService, FinnhubService>();
			builder.Services.AddScoped<IStocksService, StocksService>();
			builder.Services.AddScoped<IFinnhubRepository, FinnhubRepository>();
			builder.Services.AddScoped<IStocksRepository, StocksRepository>();

			var app = builder.Build();

			if (app.Environment.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseStaticFiles();
			app.UseRouting();
			app.MapControllers();

			app.Run();
		}
	}
}