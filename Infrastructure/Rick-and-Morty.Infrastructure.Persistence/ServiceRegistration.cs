using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rick_and_Morty.Application.Interfaces;
using Rick_and_Morty.Infrastructure.Persistence.Context;

namespace Rick_and_Morty.Infrastructure.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RickAndMortyContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("RickAndMortyConnectionString"),
                    b => b.MigrationsAssembly(typeof(RickAndMortyContext).Assembly.FullName)));

            services.AddScoped<IRickAndMortyContext, RickAndMortyContext>();
        }
    }
}
