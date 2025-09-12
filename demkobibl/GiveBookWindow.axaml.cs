using System;
using System.IO;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using demkobibl.Models;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using Avalonia.Platform.Storage;

namespace demkobibl;

public partial class GiveBookWindow : Window
{
    
    private Employee _employee;
    
    public GiveBookWindow(Employee employee)
    {
        InitializeComponent();
        
        _employee = employee;
        
        using var ctx = new DemkoBiblContext();
        
        ClientComboBox.ItemsSource = ctx.Clients.Select(c => c.Email).ToList();
        BookCopyComboBox.ItemsSource = ctx.BooksCopies.Select(b => b.Code).ToList();
    }

    private void GiveBookClientButton_OnClick(object? sender, RoutedEventArgs e)
    {
        using var ctx = new DemkoBiblContext();

        if (ClientComboBox.SelectedItem == null ||
            BookCopyComboBox.SelectedItem == null ||
            IssueDatePicker.SelectedDate == null ||
            EndDatePicker == null)
        {
            return;
        }

        var client = ctx.Clients.FirstOrDefault(c => c.Email == ClientComboBox.SelectedItem.ToString());
        var bookCopy = ctx.BooksCopies.FirstOrDefault(b => b.Code == BookCopyComboBox.SelectedItem.ToString());

        if (client == null || bookCopy == null)
        {
            return;
        }

        var giveBook = new ClientsBooksCopy
        {
            ClientId = client.Id,
            BookCopiesId = bookCopy.Id,
            IssueDate = DateOnly.FromDateTime(IssueDatePicker.SelectedDate?.DateTime ?? DateTime.Now),
            EndDate = DateOnly.FromDateTime(EndDatePicker.SelectedDate?.DateTime ?? DateTime.Now),
            ReturnDate = null,
            EmployeeId = _employee.Id,
        };
        
        bookCopy.Status = "Выдана";
        
        ctx.ClientsBooksCopies.Add(giveBook);
        ctx.SaveChanges();
        
        Close();
    }
}