using AddToCartService.Services;
using Grpc.Net.Client;
using InventoryService.Protos;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();
builder.Services.AddGrpcClient<Inventory.InventoryClient>(options =>
{
    options.Address = new Uri("https://localhost:5001"); // URL for Inventory Service
});
builder.Services.AddScoped<GrpcServiceClient>(sp =>
{
    var channel = GrpcChannel.ForAddress("https://localhost:5001");
    return new GrpcServiceClient(channel);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.Run();
