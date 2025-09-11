using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System;
using demkobibl.Models;
using System.Linq;

namespace demkobibl;

public partial class AddGenreWindow : Window
{
    public AddGenreWindow()
    {
        InitializeComponent();
    }
        
    public void SaveGenreButton_OnClick(object? sender, RoutedEventArgs e)
    {
        using var ctx = new DemkoBiblContext();

        if (string.IsNullOrEmpty(GenreTitleTextBox.Text))
        {
            return;
        }

        var genre = new Genre
        {
            Title = GenreTitleTextBox.Text
        };
        
        ctx.Genres.Add(genre);
        ctx.SaveChanges();
        
        Close();
    }
}