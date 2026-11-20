namespace LifeManagementApp.Models;

public static class TodoStore
{
    // This list holds the to-dos while the app is running
    public static List<TodoItem> Items { get; set; } = new List<TodoItem>
    {
        new TodoItem { Id = 1, Title = "Do homework", Priority = 1, DueDate = DateTime.Today.AddDays(1) },
        new TodoItem { Id = 2, Title = "Clean room", Priority = 3, DueDate = DateTime.Today.AddDays(5) },
        new TodoItem { Id = 3, Title = "Buy milk", Priority = 2, DueDate = DateTime.Today.AddDays(2) }
    };
}