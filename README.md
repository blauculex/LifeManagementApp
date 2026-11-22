# LifeManagementApp

## Description 
his is a .NET MAUI application built for the Life Management course. It combines a to-do list with real-time weather data from Open-Meteo. If the wind speed in Vaasa is over 5 m/s, it displays a kitesurfing alert.

## Features
- **Home Page:** Shows current weather (temperature and wind) and the Top 3 highest priority unfinished to-dos.
- **To-Do Page:** Allows adding new to-dos, sorting by priority or due date, and saves data locally to a JSON file.

## How to Run
1. Clone this repository.
2. Open the terminal in the project folder.
3. Run the app using Mac Catalyst:
   ```bash
   dotnet build -t:Run -f net9.0-maccatalyst

   