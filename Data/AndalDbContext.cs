using Microsoft.EntityFrameworkCore;
using TestAndal.API.Models;

namespace TestAndal.API.Data
{
    public class AndalDbContext: DbContext
    {
        public AndalDbContext(DbContextOptions<AndalDbContext> options): base(options) 
        {
            
        }

        public DbSet<Title> Titles { get; set; }
        public DbSet<Position> Positions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Title>()
                .HasMany(a => a.positions)
                .WithOne(a => a.Title);
        }



    }
}
