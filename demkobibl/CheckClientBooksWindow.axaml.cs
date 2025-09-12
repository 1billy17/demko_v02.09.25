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

public partial class CheckClientBooksWindow : Window
{
    
    ObservableCollection<ClientsBooksCopyPresenter> clientBooksCopies = new ObservableCollection<ClientsBooksCopyPresenter>();
    List<ClientsBooksCopyPresenter> dataSourseClientsBooksCopies;
    
    private Client _client;
    
    public CheckClientBooksWindow()
    {
        InitializeComponent();
    }
    
    public CheckClientBooksWindow(Client client)
    {
        InitializeComponent();
        
        _client = client;
        
        LoadData();
    }

    public class ClientsBooksCopyPresenter : ClientsBooksCopy
    {
        public string ClientName { get; set; }
        public string BookCopyName { get; set; }
        public DateOnly? IssueDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public DateOnly? ReturnDate { get; set; }
    }
    
    private void LoadData()
    {
        if (_client == null) return;
        
        using var ctx = new DemkoBiblContext();
        
        dataSourseClientsBooksCopies = ctx.ClientsBooksCopies
            .Include(it => it.Client)
            .Include(it => it.BookCopies).ThenInclude(it => it.BookCatalog)
            .Where(it => it.ClientId == _client.Id)
            .Select(it => new ClientsBooksCopyPresenter
            {
                Id = it.Id,
                ClientName = it.Client.Firstname + " " + it.Client.Lastname,
                BookCopyName = it.BookCopies.BookCatalog.Title,
                IssueDate = it.IssueDate,
                EndDate = it.EndDate,
                ReturnDate = it.ReturnDate
            })
            .ToList();

        DisplayClientsBooksCopy();
    }

    private void DisplayClientsBooksCopy()
    {
        var temp = dataSourseClientsBooksCopies;
        clientBooksCopies.Clear();
        
        foreach (var item in temp)
        {
            clientBooksCopies.Add(item);
        }
        
        BooksCopiesClientsListBox.ItemsSource = clientBooksCopies;
    }
    
    private void BackButton_OnClick(object? sender, RoutedEventArgs e)
    {
        this.Close();
    }
}