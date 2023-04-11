using BingoDAL.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;

namespace BingoDAL.EntityFramework
{
    public class BingoDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName:"bingoDb");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Player>()
                .HasIndex(p => p.Email)
                .IsUnique();

            modelBuilder
                .Entity<PlayerCard>()
                .HasKey(pc => new { pc.PlayerId, pc.CardId });
        }

        public DbSet<Player> Players { get; set; }
        public DbSet<PlayerCard> PlayerCards { get; set; }
        public DbSet<Card> Cards { get; set; }

    }
}
