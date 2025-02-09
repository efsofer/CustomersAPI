using Microsoft.EntityFrameworkCore;
using CustomersAPI.Models;
using NetTopologySuite;
using NetTopologySuite.Geometries;

namespace CustomersAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .Property(c => c.DeliveryLocation)
                .HasColumnType("geography");//  Correct EF Core mapping for `geography` field

            base.OnModelCreating(modelBuilder);
        }
    }
}
