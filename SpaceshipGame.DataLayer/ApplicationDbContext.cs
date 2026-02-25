using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SpaceshipGame.DataLayer.Entity;

namespace SpaceshipGame.DataLayer
{
    public class ApplicationDbContext : DbContext
    {
        public virtual DbSet<Player> Players { get; set; } = null!;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options) 
        { 
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //configure player entity
            modelBuilder.Entity<Player>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(entity => entity.PlayerName).IsRequired();
                entity.Property(entity => entity.Score).IsRequired();
            });
        }

        private void SeedData(Player player)
        {
            var players = new List<Player>
            {
                new Player { Id = 1, PlayerName = "Skywalker", Score = 1000 },
            };
        }
    }
}
