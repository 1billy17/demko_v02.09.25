using System;
using System.IO;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using demkobibl.Models;

namespace demkobibl;

public partial class AdminWindow : Window
{
    public AdminWindow(Employee employee)
    {
        InitializeComponent();
        FioTextBlock.Text = employee.Lastname + " " + employee.Firstname + " " + employee.Patronymic;
        RoleTextBlock.Text = employee.Position.Title;

        try
        {
            string absolutePath = Path.Combine(AppContext.BaseDirectory, employee.Image);
            EmployeeImage.Source = new Bitmap(absolutePath);
        }
        catch
        {
            EmployeeImage.Source = null;
        }
    }
    
    private void ViewHistoryButton_OnClick(object? sender, RoutedEventArgs e)
    {
        HistoryWindow historyWindow = new HistoryWindow();
        historyWindow.ShowDialog(this);
    }
}