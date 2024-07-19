using Microsoft.EntityFrameworkCore;
using Service1.Domain.Entities;

namespace Service1.Infrastructure.Data.DbContexts
{
    public class CustomerDbContext(DbContextOptions<CustomerDbContext> options) : DbContext(options)
    {
        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().ToTable("Customers");
            modelBuilder.Entity<Customer>().HasKey(m => m.Id);
            modelBuilder.Entity<Customer>().Property(m => m.Id).ValueGeneratedOnAdd(); // This specifies that the Id should be generated on add (auto-increment)
        }
    }
}