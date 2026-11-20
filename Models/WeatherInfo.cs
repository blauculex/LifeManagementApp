namespace LifeManagementApp.Models;

public class WeatherInfo
{
    public double Temperature { get; set; }
    public double WindSpeed { get; set; }
    public bool IsKitesurfingWeather => WindSpeed > 5.0;
}