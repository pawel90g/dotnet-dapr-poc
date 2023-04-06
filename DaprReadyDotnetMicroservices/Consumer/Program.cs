using Consumer;
using Dapr;
using Dapr.Client;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.UseCloudEvents();
app.MapSubscribeHandler();

app.MapPost("/test", [Topic("service-bus", "test")] async (Forecast forecast) =>
{
    const string DAPR_STORE_NAME = "redis";

    using var client = new DaprClientBuilder().Build();
    await client.SaveStateAsync(DAPR_STORE_NAME, DateTime.UtcNow.ToString("yyyyMMddHHmmss"), forecast);

    return Results.Ok();
});


await app.RunAsync();