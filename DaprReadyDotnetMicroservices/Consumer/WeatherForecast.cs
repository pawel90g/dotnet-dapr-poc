using System.Text.Json.Serialization;

namespace Consumer;

public class WeatherForecast
{
    [JsonPropertyName("date")]
    public DateTime Date { get; set; }

    [JsonPropertyName("temperatureC")]
    public int TemperatureC { get; set; }

    [JsonPropertyName("temperatureF")]
    public int TemperatureF { get; set; }

    [JsonPropertyName("summary")]
    public string? Summary { get; set; }
}

public class Forecast
{
    [JsonPropertyName("weatherForecasts")]
    public List<WeatherForecast> WeatherForecasts { get; set; } = new List<WeatherForecast>();
}