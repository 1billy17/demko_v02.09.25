using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System;
using demkobibl.Models;
using System.Linq;

namespace demkobibl;

public partial class AddAuthorWindow : Window
{
    public AddAuthorWindow()
    {
        InitializeComponent();
    }
    
    public void SaveAuthorButton_OnClick(object? sender, RoutedEventArgs e)
    {
        using var ctx = new DemkoBiblContext();
        
        if (string.IsNullOrEmpty(FirstnameAuthorTextBox.Text) ||
            string.IsNullOrEmpty(LastnameAuthorTextBox.Text))
        {
            return;
        }

        var author = new Author
        {
            Firstname = FirstnameAuthorTextBox.Text,
            Lastname = LastnameAuthorTextBox.Text,
            Patronymic = string.IsNullOrWhiteSpace(PatronymicAuthorTextBox.Text) ? null : PatronymicAuthorTextBox.Text
        };
        
        ctx.Authors.Add(author);
        ctx.SaveChanges();
        
        Close();
    }
}