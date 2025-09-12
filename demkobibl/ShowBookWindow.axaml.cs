using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using demkobibl.Models;

namespace demkobibl;

public partial class ShowBookWindow : Window
{
    
    private MainWindow.BookPresenter _book;
    
    public ShowBookWindow()
    {
        InitializeComponent();
    }
    
    public ShowBookWindow(MainWindow.BookPresenter book)
    {
        InitializeComponent();
        _book = book;
        
        TitleTextBox.Text = _book.Title;
        AuthorTextBox.Text = _book.Author;
        PublisherTextBox.Text = _book.Publisher;
        PublishYearTextBox.Text = _book.PublishYear.ToString();
        PagesTextBox.Text = _book.Pages.ToString();
        DescriptionTextBox.Text = _book.Description;
        if (_book.ImageBook != null)
        {
            BookImage.Source = new Bitmap(_book.ImageBook);
        }
    }
    
    private void BackButton_OnClick(object? sender, RoutedEventArgs e)
    {
        this.Close();
    }
}