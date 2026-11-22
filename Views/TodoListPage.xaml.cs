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
        // 1. Clear the screen
        TodoListLayout.Children.Clear();

        // 2. Loop through every item in our data
        foreach (var item in TodoStore.Items)
        {
            // Create the row container
            var border = new Border
            {
                Stroke = Colors.Gray,
                StrokeThickness = 1,
                Padding = 10,
                Margin = new Thickness(0, 5)
            };

            var grid = new Grid
            {
                ColumnDefinitions = new ColumnDefinitionCollection
                {
                    new ColumnDefinition(GridLength.Auto), // Checkbox
                    new ColumnDefinition(GridLength.Star), // Title
                    new ColumnDefinition(GridLength.Auto), // Date
                    new ColumnDefinition(GridLength.Auto)  // Delete Button
                },
                ColumnSpacing = 10
            };

            // 1. Checkbox
            var checkbox = new CheckBox { IsChecked = item.IsDone, VerticalOptions = LayoutOptions.Center };
            checkbox.CheckedChanged += (s, e) =>
            {
                item.IsDone = e.Value;
                TodoStore.Save();
                // Small delay to let the Mac finish the click animation
                Task.Delay(100).ContinueWith(_ => MainThread.BeginInvokeOnMainThread(RefreshList));
            };
            Grid.SetColumn(checkbox, 0);
            grid.Children.Add(checkbox);

            // 2. Title Label (Clickable)
            var titleLabel = new Label { Text = item.Title, FontSize = 18, VerticalOptions = LayoutOptions.Center };
            if (item.IsDone)
            {
                titleLabel.TextDecorations = TextDecorations.Strikethrough;
                titleLabel.TextColor = Colors.Gray;
            }
            
            var tapGesture = new TapGestureRecognizer();
            tapGesture.Tapped += async (s, e) => 
            {
                string newTitle = await DisplayPromptAsync("Edit Title", "Enter new title:", initialValue: item.Title);
                if (newTitle == null) return;

                string newDate = await DisplayPromptAsync("Edit Date", "Enter new date (YYYY-MM-DD):", initialValue: item.DueDate.ToString("yyyy-MM-dd"));
                if (newDate == null) return;

                if (!string.IsNullOrWhiteSpace(newTitle)) item.Title = newTitle;
                if (DateTime.TryParse(newDate, out DateTime parsedDate)) item.DueDate = parsedDate;

                TodoStore.Save();
                RefreshList();
            };
            titleLabel.GestureRecognizers.Add(tapGesture);
            Grid.SetColumn(titleLabel, 1);
            grid.Children.Add(titleLabel);

            // 3. Date Label
            var dateLabel = new Label { Text = $"Due: {item.DueDate:MM/dd}", FontSize = 14, TextColor = Colors.Gray, VerticalOptions = LayoutOptions.Center };
            Grid.SetColumn(dateLabel, 2);
            grid.Children.Add(dateLabel);

            // 4. Delete Button
            var deleteBtn = new Button { Text = "🗑️", BackgroundColor = Colors.Transparent, FontSize = 20, Padding = 0, WidthRequest = 40 };
            deleteBtn.Clicked += (s, e) =>
            {
                TodoStore.Items.Remove(item);
                TodoStore.Save();
                RefreshList();
            };
            Grid.SetColumn(deleteBtn, 3);
            grid.Children.Add(deleteBtn);

            border.Content = grid;
            TodoListLayout.Children.Add(border);
        }
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
        TodoStore.Items = sortedList;
        RefreshList();
    }

    private void OnSortByDateClicked(object sender, EventArgs e)
    {
        var sortedList = TodoStore.Items.OrderBy(t => t.DueDate).ToList();
        TodoStore.Items = sortedList;
        RefreshList();
    }
}