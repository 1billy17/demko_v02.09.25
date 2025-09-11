using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System;
using demkobibl.Models;
using System.Linq;

namespace demkobibl;

public partial class AddPublisherWindow : Window
{
    public AddPublisherWindow()
    {
        InitializeComponent();
    }
    
    public void SavePublisherButton_OnClick(object? sender, RoutedEventArgs e)
    {
        using var ctx = new DemkoBiblContext();

        if (string.IsNullOrEmpty(PublisherTitleTextBox.Text))
        {
            return;
        }

        var publisher = new Publisher
        {
            Title = PublisherTitleTextBox.Text
        };
        
        ctx.Publishers.Add(publisher);
        ctx.SaveChanges();
        
        Close();
    }
}