using Service1.Application.IRepositories;
using Service1.Application.IServices;
using Service1.Application.Services;
using Service1.Application.Mappings;
using Service1.Infrastructure.Data;
using Service1.Infrastructure.Data.DbContexts;
using Service1.Infrastructure.Repositories;
using AutoMapper;

namespace Service1.API
{
    internal class Program
    {
        const string MyAllowSpecificOrigins = "MyPolicy";

        private static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            ConfigureServices(builder);

            var app = builder.Build();

            ConfigureMiddleware(app);

            app.Run();
        }

        private static void ConfigureServices(WebApplicationBuilder builder)
        {
            // Add controllers to the dependency injection (DI) container.
            builder.Services.AddControllers();

            if (builder.Environment.IsDevelopment())
            {
                // Provides automatic generation of API documentation, making it easier to understand and use the API.
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();
            }
            
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins, builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            #region Add DbContextFactory

            string postgresqlDbConnectionString = builder.Configuration.GetConnectionString("PostgreSQLDbConnectionString") ?? string.Empty;
            string mysqlDbConnectionString = builder.Configuration.GetConnectionString("MySQLDbConnectionString") ?? string.Empty;
            string sqlServerDbConnectionString = builder.Configuration.GetConnectionString("SQLServerDbConnectionString") ?? string.Empty;

            if (string.IsNullOrEmpty(postgresqlDbConnectionString) || string.IsNullOrEmpty(mysqlDbConnectionString) || string.IsNullOrEmpty(sqlServerDbConnectionString))
            {
                throw new InvalidOperationException("Database connection strings are not valid.");
            }

            AddDatabaseServices(builder.Services, postgresqlDbConnectionString, mysqlDbConnectionString, sqlServerDbConnectionString);

            #endregion

            // Scoped services in the DI container. Scoped services are created once per request.
            // Scoped Lifetime: Ensures that each request gets its own instance, which is suitable for services that manage state specific to a request
            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<ICameraRepository, CameraRepository>();

            builder.Services.AddScoped<ICustomerService, CustomerService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<ICameraService, CameraService>();

            // Register AutoMapper with mapping profile in dependency injection (DI) container.
            builder.Services.AddAutoMapper(typeof(MappingProfile));
        }

        private static void AddDatabaseServices(IServiceCollection services, string postgresqlDbConnectionString, string mysqlDbConnectionString, string sqlServerDbConnectionString)
        {
            // Create logger instance
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole(); // Add console logging
            });

            var logger = loggerFactory.CreateLogger<DbContextFactory>();

            DbContextFactory dbContextFactory = new(postgresqlDbConnectionString, mysqlDbConnectionString, sqlServerDbConnectionString, logger);

            // Singleton service in the DI container, ensuring only one instance is used throughout the application's lifetime.
            // Singleton Pattern: Ensures a single instance, promoting consistency and resource management.
            services.AddSingleton<IDbContextFactory>(provider => dbContextFactory);

            // Applies any pending database migrations at application startup.
            dbContextFactory.ApplyMigrations();
        }

        private static void ConfigureMiddleware(WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(MyAllowSpecificOrigins);

            app.UseHttpsRedirection();  // Redirects HTTP requests to HTTPS.
            app.UseRouting();  // Enables routing.
            app.MapControllers();  // Maps controller endpoints.
        }

    }

}

