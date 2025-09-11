using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System;
using demkobibl.Models;
using System.Linq;

namespace demkobibl;

public partial class GetBookWindow : Window
{
    public GetBookWindow()
    {
        InitializeComponent();
        
        using var ctx = new DemkoBiblContext();
        
        BookCatalogComboBox.ItemsSource = ctx.BooksCatalogs.Select(b => b.Title).ToList();
        LibraryComboBox.ItemsSource = ctx.Libraries.Select(l => l.Name).ToList();
    }
    
    public void SaveBookCopyButton_OnClick(object? sender, RoutedEventArgs e)
    {
        using var ctx = new DemkoBiblContext();
        
        if (string.IsNullOrEmpty(CodeBookTextBox.Text) ||
            DatePicker.SelectedDate == null ||
            BookCatalogComboBox.SelectedItem == null ||
            LibraryComboBox.SelectedItem == null)
        {
            return;
        }

        var bookTitle = BookCatalogComboBox.SelectedItem.ToString();
        var libraryName = LibraryComboBox.SelectedItem.ToString();
        
        var catalogBook = ctx.BooksCatalogs.FirstOrDefault(b => b.Title == bookTitle);
        var library = ctx.Libraries.FirstOrDefault(l => l.Name == libraryName);
        
        if (catalogBook == null || library == null)
            return;

        var bookCopy = new BooksCopy
        {
            BookCatalogId = catalogBook.Id,
            LibraryId = library.Id,
            Code = CodeBookTextBox.Text,
            DateReceipt = DateOnly.FromDateTime(DatePicker.SelectedDate?.DateTime ?? DateTime.Now),
            Status = "Доступна"
        };
        
        ctx.BooksCopies.Add(bookCopy);
        ctx.SaveChanges();
    }
}