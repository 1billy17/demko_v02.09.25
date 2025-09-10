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

public partial class AddCatalogBook : Window
{
    public string SelectedImagePath = string.Empty;
    
    public AddCatalogBook()
    {
        InitializeComponent();
        
        using var ctx = new DemkoBiblContext();
        
        AuthorComboBox.ItemsSource = ctx.Authors.Select(a => a.Firstname + " " + a.Lastname).ToList();
        PublisherComboBox.ItemsSource = ctx.Publishers.Select(p => p.Title).ToList();
        GenresListBox.ItemsSource = ctx.Genres.Select(g => g.Title).ToList();
    }

    public void SaveBookButton_OnClick(object? sender, RoutedEventArgs e)
    {
        using var ctx = new DemkoBiblContext();

        if (string.IsNullOrWhiteSpace(TitleTextBox.Text) ||
            string.IsNullOrWhiteSpace(PublishYearTextBox.Text) ||
            AuthorComboBox.SelectedItem == null ||
            PublisherComboBox.SelectedItem == null)
        {
            return;
        }

        var authorName = AuthorComboBox.SelectedItem.ToString();
        var names = authorName.Split(' ');
        var firstname = names[0];
        var lastname = names.Length > 1 ? names[1] : "";

        var author = ctx.Authors.FirstOrDefault(a => a.Firstname == firstname && a.Lastname == lastname);
        var publisher = ctx.Publishers.FirstOrDefault(p => p.Title == PublisherComboBox.SelectedItem.ToString());

        if (author == null || publisher == null)
            return;

        if (!string.IsNullOrEmpty(SelectedImagePath))
        {
            var book = new BooksCatalog
            {
                Title = TitleTextBox.Text,
                PublishYear = int.TryParse(PublishYearTextBox.Text, out int year) ? year : null,
                Pages = int.TryParse(PagesTextBox.Text, out int pages) ? pages : null,
                Description = DescriptionTextBox.Text,
                AuthorId = author.Id,
                PublisherId = publisher.Id,
                Image = SelectedImagePath
            };

            ctx.BooksCatalogs.Add(book);
            ctx.SaveChanges();

            var selectedGenres = GenresListBox.SelectedItems.Cast<string>().ToList();

            foreach (var genreTitle in selectedGenres)
            {
                var genre = ctx.Genres.FirstOrDefault(g => g.Title == genreTitle);
                if (genre != null)
                {
                    var bookGenre = new BooksGenre
                    {
                        BookCatalogId = book.Id,
                        GenreId = genre.Id
                    };
                    ctx.BooksGenres.Add(bookGenre);
                }
            }

            ctx.SaveChanges();
        }
    }

    private async void SelectImageButton_OnClick(object? sender, RoutedEventArgs e)
    {
        var bitmap = await SelectAndSaveImage();
        if (bitmap != null)
        {
            BookImage.Source = bitmap;
        }
    }

    private async Task<Bitmap?> SelectAndSaveImage()
    {
        var showDialog = StorageProvider.OpenFilePickerAsync(
            options: new Avalonia.Platform.Storage.FilePickerOpenOptions()
            {
                Title = "Select an image",
                FileTypeFilter = new[] { FilePickerFileTypes.ImageAll }
            });
        var storageFile = await showDialog;
        try
        {
            var bmp = new Bitmap(storageFile.First().TryGetLocalPath());
            var guid = Guid.NewGuid();

            var baseDir = AppContext.BaseDirectory!;
            var imageDir = Path.Combine(baseDir, "books");

            var imagePath = Path.Combine(imageDir, $"{guid}.jpg");
            bmp.Save(imagePath);

            SelectedImagePath = $"books/{guid}.jpg";

            var fullPath = Path.GetFullPath(imagePath);
            Console.WriteLine("Saving image to: " + fullPath);

            return bmp;
        }
        catch
        {
            return null;
        }
    }

    public void AddAuthorButton_OnClick(object? sender, RoutedEventArgs e)
    {
        BlankWindow blankWindow = new BlankWindow();
        blankWindow.ShowDialog(this);
    }
    
    public void AddGenreButton_OnClick(object? sender, RoutedEventArgs e)
    {
        BlankWindow blankWindow = new BlankWindow();
        blankWindow.ShowDialog(this);
    }
}