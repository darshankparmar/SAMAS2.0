using Service1.Data.DbContexts;

namespace Service1.Data
{
    public interface IDbContextFactory
    {
        CustomerDbContext CustomerDbContext{ get; }

        ProductDbContext ProductDbContext{ get; }

        void ApplyMigrations();
    }
}