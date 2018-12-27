using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StockMarketRegister.API.Entities;
using StockMarketRegister.API.Models;
using StockMarketRegister.API.Services;

namespace StockMarketRegister.API
{
    public class Startup
    {
        public static IConfiguration Configuration;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            var connectionString = Configuration["connectionStrings:StockMarketDBConnectionString"];
            services.AddDbContext<StockMarketContext>(o => o.UseSqlServer(connectionString));

            services.AddScoped<IStockMarketRepository, StockMarketRepository>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
                        if (exceptionHandlerFeature != null)
                        {
                            var logger = loggerFactory.CreateLogger("Global exception logger");
                            logger.LogError(500,
                                exceptionHandlerFeature.Error,
                                exceptionHandlerFeature.Error.Message);
                        }

                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("An unexpected fault happened. Try again later.");
                    });
                });
            }

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Client, ClientDto>();
                cfg.CreateMap<ClientForCreationDto, Client>();
                cfg.CreateMap<ClientForUpdateDto, Client>();

                cfg.CreateMap<Stock, StockDto>();
                cfg.CreateMap<StockForCreationDto, Stock>();
                cfg.CreateMap<StockForUpdateDto, Stock>();

                cfg.CreateMap<Order, OrderDto>();
                cfg.CreateMap<OrderDto, Order>();
                cfg.CreateMap<OrderForCreationDto, Order>();

            });

            app.UseMvc();
        }
    }
}
