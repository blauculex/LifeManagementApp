using System.Text.Json;

namespace LifeManagementApp.Models;

public static class TodoStore
{
    // 1. Get the path to a safe folder on the Mac to save the file
    private static string FilePath => Path.Combine(FileSystem.AppDataDirectory, "todos.json");

    // 2. The actual list
    public static List<TodoItem> Items { get; set; } = new List<TodoItem>();

    // 3. Load data from the file (or create fake data if it's the first time)
    public static void Load()
    {
        if (File.Exists(FilePath))
        {
            string json = File.ReadAllText(FilePath);
            Items = JsonSerializer.Deserialize<List<TodoItem>>(json) ?? new List<TodoItem>();
        }
        else
        {
            // First time app is opened: Add some default items
            Items = new List<TodoItem>
            {
                new TodoItem { Id = 1, Title = "Do homework", Priority = 1, DueDate = DateTime.Today.AddDays(1) },
                new TodoItem { Id = 2, Title = "Clean room", Priority = 3, DueDate = DateTime.Today.AddDays(5) },
                new TodoItem { Id = 3, Title = "Buy milk", Priority = 2, DueDate = DateTime.Today.AddDays(2) }
            };
            Save(); // Save the defaults so they exist next time
        }
    }

    // 4. Save the data to the file
    public static void Save()
    {
        string json = JsonSerializer.Serialize(Items);
        File.WriteAllText(FilePath, json);
    }
}