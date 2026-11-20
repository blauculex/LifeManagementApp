using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LifeManagementApp.Services;
using System.Threading.Tasks;

namespace LifeManagementApp.ViewModels;

public partial class HomeViewModel : ObservableObject
{
    private readonly IWeatherService _weatherService;

    [ObservableProperty]
    private string _temperatureText = "Loading...";

    [ObservableProperty]
    private string _windText = "Loading...";

    [ObservableProperty]
    private bool _isKitesurfingWeather;

    public HomeViewModel(IWeatherService weatherService)
    {
        _weatherService = weatherService;
    }

    [RelayCommand]
    private async Task LoadWeatherAsync()
    {
        await Task.Delay(500); 
        TemperatureText = "12.5 °C";
        WindText = "Wind: 7.2 m/s";
        IsKitesurfingWeather = true; 
    }
}