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
    
    private Employee _employee;
    
    public LibrarianWindow(Employee employee)
    {
        InitializeComponent();
        
        _employee = employee;
        
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
        GiveBookWindow giveBookWindow = new GiveBookWindow(_employee);
        giveBookWindow.ShowDialog(this);
    }
    
    private void AddCatalogBookButton_OnClick(object? sender, RoutedEventArgs e)
    {
        AddCatalogBookWindow addCatalogBook = new AddCatalogBookWindow();
        addCatalogBook.ShowDialog(this);
    }
    
    private void AddClientButton_OnClick(object? sender, RoutedEventArgs e)
    {
        AddClientWindow addClientWindow = new AddClientWindow();
        addClientWindow.ShowDialog(this);
    }
    
    private void ReturnBookButton_OnClick(object? sender, RoutedEventArgs e)
    {
        ReturnBookWindow returnBookWindow = new ReturnBookWindow(_employee);
        returnBookWindow.ShowDialog(this);
    }
}