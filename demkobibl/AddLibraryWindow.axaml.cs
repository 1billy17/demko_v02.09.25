using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System;
using demkobibl.Models;
using System.Linq;

namespace demkobibl;

public partial class AddLibraryWindow : Window
{
    public AddLibraryWindow()
    {
        InitializeComponent();
    }
    
    public void SaveLibraryButton_OnClick(object? sender, RoutedEventArgs e)
    {
        using var ctx = new DemkoBiblContext();
        
        if (string.IsNullOrEmpty(NameLibraryTextBox.Text) ||
            string.IsNullOrEmpty(AddressLibraryTextBox.Text) ||
            string.IsNullOrEmpty(PhoneLibraryTextBox.Text) ||
            string.IsNullOrEmpty(EmailLibraryTextBox.Text) ||
            string.IsNullOrEmpty(TimeScheduleLibraryTextBox.Text))
        {
            return;
        }

        var library = new Library
        {
            Name = NameLibraryTextBox.Text,
            Address = AddressLibraryTextBox.Text,
            Phone = PhoneLibraryTextBox.Text,
            Email = EmailLibraryTextBox.Text,
            WorkSchedule = TimeScheduleLibraryTextBox.Text
        };
        
        ctx.Libraries.Add(library);
        ctx.SaveChanges();
        
        Close();
    }
}