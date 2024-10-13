using Microsoft.EntityFrameworkCore;
using Models.gameModels;
using Models.playersModels;
using Models.roundGroupModels;
using Models.roundModels;
using Models.roundVoteModels;

namespace Contaminados.DB
{
    public sealed class DbContextClass : DbContext
    {
        private readonly IConfiguration Configuration;

        public DbContextClass(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>()
                .HasIndex(g => g.Name)
                .IsUnique();
        }

        public DbSet<Players> Players { get; set; }
        public DbSet<Round> Round { get; set; }
        public DbSet<RoundVote> RoundVote { get; set; }
        public DbSet<Game> Game { get; set; }
        public DbSet<RoundGroup> RoundGroup { get; set; }

    }
}