using System;
using System.IO;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using demkobibl.Models;

namespace demkobibl;

public partial class LibrarianWindow : Window
{
    public LibrarianWindow(Employee employee)
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
    
    private void GetBookButton_OnClick(object? sender, RoutedEventArgs e)
    {
        GetBookWindow getBookWindow = new GetBookWindow();
        getBookWindow.ShowDialog(this);
    }
    
    private void GiveBookButton_OnClick(object? sender, RoutedEventArgs e)
    {
        GiveBookWindow giveBookWindow = new GiveBookWindow();
        giveBookWindow.ShowDialog(this);
    }
    
    private void AddCatalogBookButton_OnClick(object? sender, RoutedEventArgs e)
    {
        AddCatalogBook addCatalogBook = new AddCatalogBook();
        addCatalogBook.ShowDialog(this);
    }
}