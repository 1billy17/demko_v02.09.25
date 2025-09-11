using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System;
using demkobibl.Models;
using System.Linq;

namespace demkobibl;

public partial class AddClientWindow : Window
{
    public AddClientWindow()
    {
        InitializeComponent();
    }
    
    public void SaveClientButton_OnClick(object? sender, RoutedEventArgs e)
    {
        using var ctx = new DemkoBiblContext();
        
        if (string.IsNullOrEmpty(FirstnameClientTextBox.Text) ||
            string.IsNullOrEmpty(LastnameClientTextBox.Text) ||
            string.IsNullOrEmpty(PhoneTextBox.Text) ||
            string.IsNullOrEmpty(EmailTextBox.Text) ||
            string.IsNullOrEmpty(PasswordTextBox.Text) ||
            DatePicker.SelectedDate == null)
        {
            return;
        }

        var client = new Client
        {
            Firstname = FirstnameClientTextBox.Text,
            Lastname = LastnameClientTextBox.Text,
            Patronymic = string.IsNullOrWhiteSpace(PatronymicClientTextBox.Text) ? null : PatronymicClientTextBox.Text,
            Phone = PhoneTextBox.Text,
            Email = EmailTextBox.Text,
            Password = PasswordTextBox.Text,
            Birthday = DateOnly.FromDateTime(DatePicker.SelectedDate?.DateTime ?? DateTime.Now)
        };
        
        ctx.Clients.Add(client);
        ctx.SaveChanges();
        
        Close();
    }
}