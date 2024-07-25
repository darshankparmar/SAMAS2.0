using Service1.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("MyPolicy", builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});


#region  Database

string postgresqlDbConnectionString = builder.Configuration.GetConnectionString("PostgreSQLDbConnectionString") ?? string.Empty;
string mysqlDbConnectionString = builder.Configuration.GetConnectionString("MySQLDbConnectionString") ?? string.Empty;

if (string.IsNullOrEmpty(postgresqlDbConnectionString) || string.IsNullOrEmpty(mysqlDbConnectionString))
{
    throw new InvalidOperationException("Database connection strings are not valid.");
}

var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddConsole();
});
var logger = loggerFactory.CreateLogger<DbContextFactory>();
DbContextFactory dbContextFactory = new(postgresqlDbConnectionString, mysqlDbConnectionString, logger);

builder.Services.AddSingleton<IDbContextFactory>(provider => dbContextFactory);

dbContextFactory.ApplyMigrations();

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("MyPolicy");

app.UseHttpsRedirection();  // Redirects HTTP requests to HTTPS.
app.UseRouting();  // Enables routing.
app.MapControllers();  // Maps controller endpoints.

app.Run();
