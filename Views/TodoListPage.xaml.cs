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
        // Bind the CollectionView to our shared list
        TodoCollection.ItemsSource = null; // Reset
        TodoCollection.ItemsSource = TodoStore.Items;
    }

    private async void OnAddClicked(object sender, EventArgs e)
    {
        // Prompt the user for a title
        string result = await DisplayPromptAsync("New To-Do", "Enter the title:");

        // If they typed something and didn't click Cancel
        if (!string.IsNullOrWhiteSpace(result))
        {
            // Add a new item to the shared list
            TodoStore.Items.Add(new TodoItem 
            { 
                Id = TodoStore.Items.Count + 1, 
                Title = result, 
                Priority = 3, 
                DueDate = DateTime.Today.AddDays(1) 
            });

            // Refresh the screen to show the new item
            RefreshList();
        }
    }
}