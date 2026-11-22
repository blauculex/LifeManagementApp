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
                Id = (int)(DateTime.Now.Ticks % int.MaxValue), 
                Title = result, 
                Priority = 3, 
                DueDate = DateTime.Today.AddDays(1) 
            });

            TodoStore.Save(); 
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

    private async void OnCheckBoxChanged(object sender, CheckedChangedEventArgs e)
    {
        TodoStore.Save();
        await Task.Delay(100); // Fixes the Mac UI glitch
        RefreshList(); 
    }

    private void OnDeleteClicked(object sender, EventArgs e)
    {
        var swipeItem = sender as SwipeItem;
        var todoItem = swipeItem?.BindingContext as TodoItem;

        if (todoItem != null)
        {
            TodoStore.Items.Remove(todoItem);
            TodoStore.Save();
            RefreshList();
        }
    }
}