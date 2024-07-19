var builder = WebApplication.CreateBuilder(args);
/*  WebApplication.CreateBuilder(args) creates a builder instance (builder) that helps with setting up the web application. 
    It initializes the application with the provided arguments (args), typically from the command line or environment. */

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/* These lines configure Swagger/OpenAPI support for the application:
    1. AddEndpointsApiExplorer() adds API explorer services, which are used by Swagger to generate Swagger/OpenAPI metadata.
    2. AddSwaggerGen() adds Swagger generation services, which are used to generate Swagger/OpenAPI documents for describing the API. */

var app = builder.Build();

/* builder.Build() creates the WebApplication instance (app) based on the configuration and services registered using the builder. */

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

/* This block checks if the application is running in the development environment (IsDevelopment()). If true, it enables Swagger and Swagger UI:
    1. UseSwagger() adds middleware to expose the generated Swagger JSON document.
    2. UseSwaggerUI() adds middleware to serve Swagger UI, which provides an interactive documentation interface. */

app.UseHttpsRedirection();

/* UseHttpsRedirection() is middleware that redirects HTTP requests to HTTPS. This helps enforce secure connections. */

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

/* summaries is an array of strings representing different weather summaries. */

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

/*  app.MapGet("/weatherforecast", () => { ... }) defines a route /weatherforecast that handles GET requests. Inside the lambda expression:
        It generates a forecast by creating an array of WeatherForecast objects. Each forecast entry includes a date (incremented by days), 
        a random temperature between -20 to 55 degrees Celsius, and a random weather summary from summaries.
    
    .WithName("GetWeatherForecast") assigns a name "GetWeatherForecast" to this endpoint.
    
    .WithOpenApi() configures this endpoint to be included in the generated Swagger/OpenAPI documentation. */

app.Run();

/* app.Run() starts the application and begins listening for incoming HTTP requests. */

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

/* WeatherForecast is a C# record representing a weather forecast entry:
    It has properties Date (of type DateOnly), TemperatureC (in Celsius), and Summary (a string describing the weather).
    TemperatureF is a computed property that converts TemperatureC to Fahrenheit using the formula (TemperatureC / 0.5556) + 32. */
