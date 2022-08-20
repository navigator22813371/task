using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Rick_and_Morty.WebApi.Extensions;
using System.Threading.Tasks;

namespace Rick_and_Morty.WebApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            await host.MigrateContexts();
            await host.InitializeBaseData();

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
