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

public partial class MainWindow : Window
{
    ObservableCollection<BookPresenter> books = new ObservableCollection<BookPresenter>();
    List<BookPresenter> dataSourseBooks;
    
    public MainWindow()
    {
        InitializeComponent();
        using var ctx = new DemkoBiblContext();
        dataSourseBooks = ctx.BooksCatalogs
            .Include(i => i.Author)
            .Include(i => i.BooksGenres)
            .ThenInclude(ig => ig.Genre)
            .Select(i => new BookPresenter
            {
                Id = i.Id,
                Title = i.Title,
                Author = i.Author != null 
                    ? i.Author.Firstname + " " + i.Author.Lastname 
                    : "Неизвестен",
                PublishYear = i.PublishYear ?? 0,
                Genres = i.BooksGenres
                    .Select(ig => ig.Genre.Title)
                    .ToList(),
                ImageBook = Path.Combine(AppContext.BaseDirectory, i.Image ?? string.Empty)
            }).ToList();
        
        AuthorComboBox.ItemsSource = new List<string> { "Все" }.Concat(ctx.Authors.Select(a => a.Firstname + " " + a.Lastname)).ToList();
        GenreComboBox.ItemsSource = new List<string> { "Все" }.Concat(ctx.Genres.Select(g => g.Title)).ToList();
        
        DisplayServices();
    }
    
    public class BookPresenter : BooksCatalog
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int PublishYear { get; set; }
        public List<string> Genres { get; set; }
        
        public string GenresDisplay => string.Join(", ", Genres);
        public string? ImageBook { get; set; }
        
        Bitmap? Image
        {
            get
            {
                try
                {
                    string absolutePath = Path.Combine(AppContext.BaseDirectory, ImageBook);
                    return new Bitmap(absolutePath);
                }
                catch
                {
                    return null;
                }
            }
        }
    }

    public void DisplayServices()
    {
        var temp = dataSourseBooks;
        books.Clear();

        if (AuthorComboBox.SelectedItem is string selectedAuthor && selectedAuthor != "Все")
        {
            temp = temp.Where(b => b.Author == selectedAuthor).ToList();
        }

        if (GenreComboBox.SelectedItem is string selectedGenre && selectedGenre != "Все")
        {
            temp = temp.Where(b => b.Genres.Contains(selectedGenre)).ToList();
        }

        
        if (!string.IsNullOrEmpty(SearchTextBox.Text))
        {
            var search = SearchTextBox.Text;
            temp = temp.Where(it => IsContains(it.Title, it.Author, it.GenresDisplay, search)).ToList();
        }
        
        foreach (var item in temp)
        {
            books.Add(item);
        }
        
        BooksListBox.ItemsSource = books;
    }
    
    public bool IsContains(string title, string author, string genre, string search)
    {
        string message = (title + author + genre).ToLower();
        search = search.ToLower();
        return message.Contains(search);
    }
    
    public void AuthorComboBox_OnSelectionChanged(object? sender, RoutedEventArgs e)
    {
        DisplayServices();
    }
    
    public void GenreComboBox_OnSelectionChanged(object? sender, RoutedEventArgs e)
    {
        DisplayServices();
    }
    
    private void SearchTextBox_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        DisplayServices();
    }
    
    public void AuthWindow_OnClick(object? sender, RoutedEventArgs e)
    {
        AuthWindow authWindowButton = new AuthWindow();
        authWindowButton.ShowDialog(this);
    }
}