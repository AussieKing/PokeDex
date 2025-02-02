using Microsoft.EntityFrameworkCore;
using PokedexApp.Models;

namespace PokedexApp.Data
{
    public class PokedexContext : DbContext
    {
        public PokedexContext(DbContextOptions<PokedexContext> options)
            : base(options) { }

        public DbSet<Pokemon> Pokemons { get; set; }
        public DbSet<PokemonType> PokemonTypes { get; set; }
        public DbSet<TypeInfo> TypeInfos { get; set; }
        public DbSet<PokemonStat> PokemonStats { get; set; }
        public DbSet<StatInfo> StatInfos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<PokemonType>()
                .HasOne(pt => pt.Type)
                .WithMany()
                .HasForeignKey(pt => pt.TypeId);

            modelBuilder
                .Entity<PokemonStat>()
                .HasOne(ps => ps.Stat)
                .WithMany()
                .HasForeignKey(ps => ps.StatId);
        }
    }
}
