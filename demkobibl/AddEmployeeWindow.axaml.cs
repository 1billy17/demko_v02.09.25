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

public partial class AddEmployeeWindow : Window
{
    
    public string SelectedImagePath = string.Empty;
    
    public AddEmployeeWindow()
    {
        InitializeComponent();
        LoadData();
        this.Activated += (_, _) => LoadData();
    }

    private void LoadData()
    {
        using var ctx = new DemkoBiblContext();
        
        LibraryComboBox.ItemsSource = ctx.Libraries.Select(x => x.Name).ToList();
    }

    public void SaveEmployeeButton_OnClick(object? sender, RoutedEventArgs e)
    {
        using var ctx = new DemkoBiblContext();

        if (string.IsNullOrWhiteSpace(FirstnameEmployeeTextBox.Text) ||
            string.IsNullOrWhiteSpace(LastnameEmployeeTextBox.Text) ||
            LibraryComboBox.SelectedItem == null ||
            string.IsNullOrWhiteSpace(EmailTextBox.Text) ||
            string.IsNullOrWhiteSpace(PasswordTextBox.Text))
        {
            return;
        }

        var library = ctx.Libraries.FirstOrDefault(l => l.Name == LibraryComboBox.SelectedItem.ToString());

        if (library == null)
        {
            return;
        }

        if (!string.IsNullOrEmpty(SelectedImagePath))
        {
            var employee = new Employee
            {
                Firstname = FirstnameEmployeeTextBox.Text,
                Lastname = LastnameEmployeeTextBox.Text,
                Patronymic = string.IsNullOrWhiteSpace(PatronymicEmployeeTextBox.Text) ? null : PatronymicEmployeeTextBox.Text,
                Password = PasswordTextBox.Text,
                Email = EmailTextBox.Text,
                LibraryId = library.Id,
                PositionId = 1,
                Image = SelectedImagePath
            };
            
            ctx.Employees.Add(employee);
            ctx.SaveChanges();
            
            Close();
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
            var imageDir = Path.Combine(baseDir, "employee");

            var imagePath = Path.Combine(imageDir, $"{guid}.jpg");
            bmp.Save(imagePath);

            SelectedImagePath = $"employee/{guid}.jpg";

            var fullPath = Path.GetFullPath(imagePath);
            Console.WriteLine("Saving image to: " + fullPath);

            return bmp;
        }
        catch
        {
            return null;
        }
    }
    
    public void AddLibraryButton_OnClick(object? sender, RoutedEventArgs e)
    {
        AddLibraryWindow addLibraryWindow = new AddLibraryWindow();
        addLibraryWindow.ShowDialog(this);
    }
}