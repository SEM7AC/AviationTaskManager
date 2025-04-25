using AviationTaskManager.Models;
using AviationTaskManager.ViewModels;
using Microsoft.Data.Sqlite;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AviationTaskManager.Views
    {
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
        {
        public Login()
            {
            InitializeComponent();
            }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
            {
            // Retrieve input from the UI
            string username = UsernameInput.Text.Trim();
            string password = PasswordInput.Password; // Secure input
            string role;

            // Call AuthenticateUser to get the role
            if (DatabaseManager.AuthenticateUser(username, password, out role))
                {
                tb_info.Foreground = Brushes.Green;
                tb_info.Text = "Successful login.";

                // Set the role in ViewModel
                MainWindowViewModel viewModel = new MainWindowViewModel();
                viewModel.CurrentUserRole = role; // Assign fetched role

                // Pass ViewModel to MainWindow
                MainWindow mainWindow = new MainWindow
                    {
                    DataContext = viewModel // Ensuring DataContext contains updated role
                    };

                Task.Delay(1000);
                mainWindow.Show();
                this.Close(); // Close login window
                }
            else
                {
                tb_info.Foreground = Brushes.Red;
                tb_info.Text = "Invalid username or password.";
                 await ResetStatusText();
                }
            }

        private async Task GotoMainWindow() 
            {
            await Task.Delay(3000);
            MainWindow main = new MainWindow();
            main.Show();
            this.Close(); // Close the login window
            }
        private async Task ResetStatusText()
            {
            await Task.Delay(5000);
            tb_info.Foreground = Brushes.Green;
            tb_info.Text = "Status: ";

            }

        } /// END OF CLASS

    } /// END OF FILE

