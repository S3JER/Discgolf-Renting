using DiscGolfRental.Models;
using Microsoft.EntityFrameworkCore;

namespace DiscGolfRental.Db
{
    public class DiscDatabaseContext : DbContext
    {
        public DiscDatabaseContext(DbContextOptions<DiscDatabaseContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Disc> Discs { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data for Discs
            modelBuilder.Entity<Disc>().HasData(
                new Disc
                {
                    Id = 1,
                    Name = "Driver",
                    Type = "Distance",
                    Condition = "New"
                },
                new Disc
                {
                    Id = 2,
                    Name = "Midrange",
                    Type = "Control",
                    Condition = "Used"
                },
                new Disc
                {
                    Id = 3,
                    Name = "Putter",
                    Type = "Putting",
                    Condition = "Good"
                }
            );
        }
    }
}
