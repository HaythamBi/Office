using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using OfficeReservation.Application.BusinessLogic;
using OfficeReservation.Application.Interfaces;
using OfficeReservation.Infrastructure.Configurations;
using OfficeReservation.Infrastructure.DataSources;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace OfficeReservation.WebUI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddScoped<IConfigurations, Configurations>();
            builder.Services.AddScoped<IDataRepository, DataFromLocalFile>();
            builder.Services.AddScoped<IConfigurations, Configurations>();
            builder.Services.AddScoped<ICalculateRevenueStrategy, CalculateRevenueBehavior>();
            builder.Services.AddScoped<IRevenuesAndCapacityByMonth, RevenuesAndCapacityByMonth>();

            await builder.Build().RunAsync();
        }
    }
}
