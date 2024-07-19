using Microsoft.EntityFrameworkCore;
using Service1.Domain.Entities;

namespace Service1.Infrastructure.Data.DbContexts
{
    public class CameraDbContext(DbContextOptions<CameraDbContext> options) : DbContext(options)
    {
        public DbSet<Camera> Cameras { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Camera>().ToTable(Camera.TableName);
            modelBuilder.Entity<Camera>().HasKey(m => m.Id);
            modelBuilder.Entity<Camera>().Property(m => m.Id).ValueGeneratedOnAdd(); // This specifies that the Id should be generated on add (auto-increment)
        }
    }
}