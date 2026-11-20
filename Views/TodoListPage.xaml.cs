using LifeManagementApp.Models;

namespace LifeManagementApp;

public partial class TodoListPage : ContentPage
{
    public TodoListPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        RefreshList();
    }

    private void RefreshList()
    {
        TodoCollection.ItemsSource = null; 
        TodoCollection.ItemsSource = TodoStore.Items;
    }

    private async void OnAddClicked(object sender, EventArgs e)
    {
        string result = await DisplayPromptAsync("New To-Do", "Enter the title:");

        if (!string.IsNullOrWhiteSpace(result))
        {
            TodoStore.Items.Add(new TodoItem 
            { 
                Id = TodoStore.Items.Count + 1, 
                Title = result, 
                Priority = 3, 
                DueDate = DateTime.Today.AddDays(1) 
            });

            // Save to the JSON file
            TodoStore.Save(); 
            
            // Refresh the list
            RefreshList();
        }
    }

    private void OnSortByPriorityClicked(object sender, EventArgs e)
    {
        var sortedList = TodoStore.Items.OrderBy(t => t.Priority).ToList();
        TodoCollection.ItemsSource = sortedList;
    }

    private void OnSortByDateClicked(object sender, EventArgs e)
    {
        var sortedList = TodoStore.Items.OrderBy(t => t.DueDate).ToList();
        TodoCollection.ItemsSource = sortedList;
    }
}