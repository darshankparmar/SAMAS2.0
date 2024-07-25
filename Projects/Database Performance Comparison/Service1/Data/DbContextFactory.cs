using Microsoft.EntityFrameworkCore;
using Service1.Data.DbContexts;

namespace Service1.Data
{
    public class DbContextFactory : IDbContextFactory, IDisposable
    {
        private readonly string _postgresqlDbConnectionString;
        private readonly string _mysqlDbConnectionString;
        private readonly ILogger<DbContextFactory> _logger;

        private CustomerDbContext? _customerDbContext;
        private ProductDbContext? _productDbContext;

        public DbContextFactory(string postgresqlDbConnectionString, string mysqlDbConnectionString, ILogger<DbContextFactory> logger)
        {
            _postgresqlDbConnectionString = postgresqlDbConnectionString ?? throw new ArgumentNullException(nameof(postgresqlDbConnectionString));
            _mysqlDbConnectionString = mysqlDbConnectionString ?? throw new ArgumentNullException(nameof(mysqlDbConnectionString));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public CustomerDbContext CustomerDbContext
        {
            get
            {
                if (_customerDbContext == null)
                {
                    try
                    {
                        var optionsBuilder = new DbContextOptionsBuilder<CustomerDbContext>();
                        optionsBuilder.UseNpgsql(_postgresqlDbConnectionString);
                        _customerDbContext = new CustomerDbContext(optionsBuilder.Options);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Failed to create CustomerDbContext");
                        throw;
                    }
                }
                return _customerDbContext;
            }
        }

        public ProductDbContext ProductDbContext
        {
            get
            {
                if (_productDbContext == null)
                {
                    try
                    {
                        var optionsBuilder = new DbContextOptionsBuilder<ProductDbContext>();
                        optionsBuilder.UseMySql(_mysqlDbConnectionString, ServerVersion.AutoDetect(_mysqlDbConnectionString));
                        _productDbContext = new ProductDbContext(optionsBuilder.Options);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Failed to create ProductDbContext");
                        throw;
                    }
                }
                return _productDbContext;
            }
        }

        public void ApplyMigrations()
        {
            try
            {
                ApplyMigrations(CustomerDbContext);
                ApplyMigrations(ProductDbContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to apply migrations");
                throw;
            }
        }

        private void ApplyMigrations(DbContext? dbContext)
        {
            if (dbContext == null)
            {
                _logger.LogInformation($"DBContext is null.");
                return;
            }

            // Ensure the database is created and apply migrations if needed
            if (dbContext.Database.EnsureCreated())
            {
                _logger.LogInformation($"{dbContext.GetType().Name} database created.");
            }
            else
            {
                var pendingMigrations = dbContext.Database.GetPendingMigrations();
                if (pendingMigrations.Any())
                {
                    _logger.LogInformation($"Applying {pendingMigrations.Count()} pending migrations for {dbContext.GetType().Name} database:");
                    foreach (var migration in pendingMigrations)
                    {
                        _logger.LogInformation($"- {migration}");
                    }
                    dbContext.Database.Migrate();
                    _logger.LogInformation($"Migrations applied for {dbContext.GetType().Name} database.");
                }
                else
                {
                    _logger.LogInformation($"No pending migrations for {dbContext.GetType().Name} database.");
                }
            }
        }

        private static void DisposeContext(DbContext? context)
        {
            context?.Dispose();
        }

        public void Dispose()
        {
            DisposeContext(_customerDbContext);
            DisposeContext(_productDbContext);
        }
    }
}
