using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using demkobibl.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.IO;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using demkobibl.Models;
using Microsoft.EntityFrameworkCore;

namespace demkobibl;

public partial class ReturnBookWindow : Window
{
    
    ObservableCollection<ClientsBooksCopyPresenter> clientBooksCopies = new ObservableCollection<ClientsBooksCopyPresenter>();
    List<ClientsBooksCopyPresenter> dataSourseClientsBooksCopies;
    
    private Employee? _employee;
    
    public ReturnBookWindow()
    {
        InitializeComponent();
    }
    
    public ReturnBookWindow(Employee employee)
    {
        InitializeComponent();
        
        _employee = employee;
        
        LoadData();
    }
    
    public class ClientsBooksCopyPresenter : ClientsBooksCopy
    {
        public string ClientEmail { get; set; }
        public string BookCopyCode { get; set; }
        public DateOnly? IssueDate { get; set; }
        public DateOnly? EndDate { get; set; }
    }
    
    private void LoadData()
    { 
        if (_employee == null) return;
        
        using var ctx = new DemkoBiblContext();
        
        dataSourseClientsBooksCopies = ctx.ClientsBooksCopies
            .Include(it => it.Client)
            .Include(it => it.BookCopies)
            .Where(it => it.EmployeeId == _employee.Id)
            .Where(it => it.BookCopies.Status == "Выдана")
            .Select(it => new ClientsBooksCopyPresenter
            {
                Id = it.Id,
                ClientEmail = it.Client.Email,
                BookCopyCode = it.BookCopies.Code,
                IssueDate = it.IssueDate,
                EndDate = it.EndDate
            })
            .ToList();

        DisplayClientsBooksCopy();
    }
    
    public void DisplayClientsBooksCopy()
    {
        var temp = dataSourseClientsBooksCopies;
        clientBooksCopies.Clear();
        
        foreach (var item in temp)
        {
            clientBooksCopies.Add(item);
        }
        
        BooksCopiesClientsListBox.ItemsSource = clientBooksCopies;
    }

    private void ReturnBookCopy_OnClick(object? sender, RoutedEventArgs e)
    {
        using var ctx = new DemkoBiblContext();

        var selectedItem = BooksCopiesClientsListBox.SelectedItem as ClientsBooksCopyPresenter;
        if (selectedItem == null) return;
        
        var returnBookCopy = ctx.ClientsBooksCopies
            .Include(c => c.BookCopies)
            .FirstOrDefault(c => c.Id == selectedItem.Id);

        if (returnBookCopy == null) return;
        
        returnBookCopy.ReturnDate = DateOnly.FromDateTime(DateTime.Now);
        if (returnBookCopy.BookCopies != null)
        {
            returnBookCopy.BookCopies.Status = "Доступна";
        }

        ctx.SaveChanges();
        
        clientBooksCopies.Remove(selectedItem);
    }
}