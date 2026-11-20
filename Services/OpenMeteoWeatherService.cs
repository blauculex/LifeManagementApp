using System.Text.Json;
using LifeManagementApp.Models;

namespace LifeManagementApp.Services;

public class OpenMeteoWeatherService : IWeatherService
{
    private readonly HttpClient _httpClient;

    public OpenMeteoWeatherService()
    {
        _httpClient = new HttpClient();
    }

    public async Task<WeatherInfo> GetCurrentWeatherAsync()
    {
        try
        {
            string url = "https://api.open-meteo.com/v1/forecast?latitude=63.10&longitude=21.62&current=temperature_2m,wind_speed_10m&wind_speed_unit=ms";
            
            var response = await _httpClient.GetStringAsync(url);
            
            using JsonDocument doc = JsonDocument.Parse(response);
            var current = doc.RootElement.GetProperty("current");
            
            double temp = current.GetProperty("temperature_2m").GetDouble();
            double wind = current.GetProperty("wind_speed_10m").GetDouble();

            return new WeatherInfo
            {
                Temperature = temp,
                WindSpeed = wind
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Weather API Error: {ex.Message}");
            return new WeatherInfo { Temperature = 0, WindSpeed = 0 };
        }
    }
}