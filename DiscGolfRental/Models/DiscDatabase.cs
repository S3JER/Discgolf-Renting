using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DiscGolfRental.Models
{
        public class ApplicationDbContext : DbContext
        {
            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

            public DbSet<User> Users { get; set; }
            public DbSet<Disc> Discs { get; set; }
            public DbSet<Rental> Rentals { get; set; }
        }
}
