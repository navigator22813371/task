using Microsoft.EntityFrameworkCore;
using Rick_and_Morty.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace Rick_and_Morty.Application.Interfaces
{
    public interface IRickAndMortyContext
    {
        DbSet<Character> Characters { get; set; }
        DbSet<Location> Locations { get; set; }
        DbSet<Episode> Episodes { get; set; }
        DbSet<EpisodeCharacters> EpisodeCharacters { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
