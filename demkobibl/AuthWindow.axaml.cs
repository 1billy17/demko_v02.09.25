using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using demkobibl.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace demkobibl;

public partial class AuthWindow : Window
{
    public AuthWindow()
    {
        InitializeComponent();
        PasswordTextBox.PasswordChar = '•';
    }
    
    public void AuthButton_OnClick(object? sender, RoutedEventArgs e)
    {
        using var ctx = new DemkoBiblContext();
        
        var loginText = LoginTextBox?.Text?.Trim();
        var password = PasswordTextBox?.Text;
        
        if (string.IsNullOrEmpty(loginText) || string.IsNullOrEmpty(password))
        {
            return;
        }
        
        var employee = ctx.Employees
            .Include(e => e.Position)
            .FirstOrDefault(e => e.Email == loginText);

        if (employee != null)
        {
            if (employee.Password == password)
            {
                switch (employee.PositionId)
                {
                    case 1:
                        new LibrarianWindow(employee).ShowDialog(this);
                        break;
                    case 2:
                        new AdminWindow(employee).ShowDialog(this);
                        break;
                    default:
                        new BlankWindow().ShowDialog(this);
                        break;
                }
            }
        }
        
        var client = ctx.Clients.FirstOrDefault(c => c.Email == loginText);
        if (client != null)
        {
            if (client.Password == password)
            {
                Close(client);
            }
        }
    }
    
    private void ShowPasswordCheckBox_OnCheck(object sender, RoutedEventArgs e)
    {
        PasswordTextBox.PasswordChar = '\0';
    }

    private void ShowPasswordCheckBox_OnUncheck(object sender, RoutedEventArgs e)
    {
        PasswordTextBox.PasswordChar = '•';
    }
}