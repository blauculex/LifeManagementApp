using System.Text.Json;
using LifeManagementApp.Models;

namespace LifeManagementApp;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // --- 1. WEATHER API ---
        try
        {
            using var client = new HttpClient();
            string url = "https://api.open-meteo.com/v1/forecast?latitude=63.10&longitude=21.62&current=temperature_2m,wind_speed_10m&wind_speed_unit=ms";
            string jsonResponse = await client.GetStringAsync(url);

            using JsonDocument doc = JsonDocument.Parse(jsonResponse);
            var current = doc.RootElement.GetProperty("current");
            
            double temp = current.GetProperty("temperature_2m").GetDouble();
            double wind = current.GetProperty("wind_speed_10m").GetDouble();

            TemperatureLabel.Text = $"{temp} °C";
            WindLabel.Text = $"Wind: {wind} m/s";

            if (wind > 5.0)
            {
                KiteLabel.IsVisible = true;
            }
        }
        catch (Exception)
        {
            TemperatureLabel.Text = "No internet";
            WindLabel.Text = "";
        }

        // --- 2. TOP 3 TO-DOS ---
        // Create some fake to-dos
         var top3 = TodoStore.Items
            .Where(t => !t.IsDone)
            .OrderBy(t => t.Priority)
            .ThenBy(t => t.DueDate)
            .Take(3)
            .ToList();

        // Show them on the screen
        if (top3.Count > 0) Todo1Label.Text = $"{top3[0].Priority}. {top3[0].Title} (Due: {top3[0].DueDate:MM/dd})";
        else Todo1Label.Text = "";
        
        if (top3.Count > 1) Todo2Label.Text = $"{top3[1].Priority}. {top3[1].Title} (Due: {top3[1].DueDate:MM/dd})";
        else Todo2Label.Text = "";
        
        if (top3.Count > 2) Todo3Label.Text = $"{top3[2].Priority}. {top3[2].Title} (Due: {top3[2].DueDate:MM/dd})";
        else Todo3Label.Text = "";
    }
}