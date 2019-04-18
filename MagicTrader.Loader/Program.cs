using MagicTrader.Core.Context;
using MagicTrader.Core.DataLoad;
using MagicTrader.Core.Scryfall;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Net.Http;

namespace MagicTrader.Loader
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            IServiceCollection services = new ServiceCollection();
            services.AddDbContext<MagicTraderContext>(options => options.UseSqlServer(configuration.GetValue<string>("ConnectionString")), ServiceLifetime.Transient, ServiceLifetime.Transient);
            services.AddSingleton<HttpClient>();
            services.AddTransient<IScryfallContext, ScryfallContext>();
            services.AddTransient<IMagicSetContext, MagicSetContext>();
            services.AddTransient<IMagicCardContext, MagicCardContext>();
            services.AddTransient<IMagicCardContext, MagicCardContext>();
            services.AddTransient<DataRefresher>();
            var serviceProvider = services.BuildServiceProvider();

            var refresher = serviceProvider.GetService<DataRefresher>();
            await refresher.LoadFromScryfall(configuration.GetValue<string>("ConnectionString"));
        }
    }
}
