using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Rick_and_Morty.Infrastructure.Persistence.Context;
using Rick_and_Morty.Infrastructure.Persistence.Seeds;
using System.Threading.Tasks;

namespace Rick_and_Morty.WebApi.Extensions
{
    public static class SeedData
    {
        public static async Task InitializeBaseData(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                using var context = scope.ServiceProvider.GetRequiredService<RickAndMortyContext>();

                await DefaultData.SeedLocationsAsync(context);
                await DefaultData.SeedCharactersAsync(context);
                await DefaultData.SeedEpisodesAsync(context);
            }
        }
    }

}
