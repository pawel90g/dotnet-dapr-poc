using Api;
using Dapr.Client;


string[] Summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

async Task PublishForecast(Forecast forecast)
{
    const string DAPR_MESSAGE_BROKER_NAME = "service-bus";
    const string TOPIC_NAME = "test";

    using var client = new DaprClientBuilder().Build();
    await client.PublishEventAsync(DAPR_MESSAGE_BROKER_NAME, TOPIC_NAME, forecast);
}


var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/weatherforecast", async () =>
{
    var forecast = Enumerable
                .Range(1, 5)
                .Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                })
                .ToList();

    await PublishForecast(new Forecast { WeatherForecasts = forecast });

    return forecast;
});

await app.RunAsync();