using Service1.Infrastructure.Data.DbContexts;

namespace Service1.Infrastructure.Data
{
    public interface IDbContextFactory
    {
        CustomerDbContext CustomerDbContext{ get; }

        ProductDbContext ProductDbContext{ get; }
        
        CameraDbContext CameraDbContext{ get; }

        void ApplyMigrations();
    }
}