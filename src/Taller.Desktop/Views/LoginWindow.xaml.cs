using System.Windows;
using Taller.Desktop.Services;

namespace Taller.Desktop.Views;

public partial class LoginWindow : Window
{
    private readonly AuthApiClient _authApiClient = new();

    public LoginWindow()
    {
        InitializeComponent();
    }

    private async void OnLoginClick(object sender, RoutedEventArgs e)
    {
        var token = await _authApiClient.LoginAsync(UsernameTextBox.Text, PasswordTextBox.Password);
        if (string.IsNullOrWhiteSpace(token))
        {
            MessageBox.Show("Credenciales inválidas", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        var dashboard = new DashboardWindow(token);
        dashboard.Show();
        Close();
    }
}
