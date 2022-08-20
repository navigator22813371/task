using Microsoft.EntityFrameworkCore;
using Rick_and_Morty.Application.Interfaces;
using Rick_and_Morty.Domain;

namespace Rick_and_Morty.Infrastructure.Persistence.Context
{
    public class RickAndMortyContext : DbContext, IRickAndMortyContext
    {
        public RickAndMortyContext(DbContextOptions<RickAndMortyContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EpisodeCharacters>().HasKey(ec => new { ec.EpisodeId, ec.CharacterId });
        }

        public DbSet<Character> Characters { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Episode> Episodes { get; set; }
        public DbSet<EpisodeCharacters> EpisodeCharacters { get; set; }

    }
}
