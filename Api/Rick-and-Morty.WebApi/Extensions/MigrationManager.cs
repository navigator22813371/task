using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Rick_and_Morty.Infrastructure.Identity.Context;
using Rick_and_Morty.Infrastructure.Persistence.Context;
using System.Threading.Tasks;

namespace Rick_and_Morty.WebApi.Extensions
{
    public static class MigrationManager
    {
        public static async Task<IHost> MigrateContexts(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                using (var authContext = scope.ServiceProvider.GetRequiredService<RickAndMortyContext>())
                {
                    await authContext.Database.MigrateAsync();
                }
            }

            using (var scope = host.Services.CreateScope())
            {
                using (var authContext = scope.ServiceProvider.GetRequiredService<IdentityContext>())
                {
                    await authContext.Database.MigrateAsync();
                }
            }

            return host;
        }

    }
}
