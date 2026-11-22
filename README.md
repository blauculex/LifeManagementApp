# LifeManagementApp

## Description
A .NET MAUI application built for the Life Management course. It combines a real-time weather dashboard (Open-Meteo API) with a personalized to-do list. If the wind speed in Vaasa is over 5 m/s, it alerts the user that it is kitesurfing weather.

## Features
- **Home Page:** Fetches live weather, checks for kitesurfing conditions, and displays the Top 3 highest priority unfinished to-dos.
- **To-Do Page:** Allows adding, editing (via clicking the title), deleting, and sorting tasks. Checkboxes allow marking tasks as done, which automatically strikes through the text and updates the Home page.
- **Storage:** Saves all to-dos locally to a JSON file so data persists across app restarts.

## How to Run
1. Clone this repository.
2. Open the terminal in the project folder.
3. Run the app using Mac Catalyst:
   ```bash
   dotnet build -t:Run -f net9.0-maccatalyst