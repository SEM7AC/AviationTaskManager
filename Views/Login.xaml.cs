using AviationTaskManager.Models;
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

        private void LoginButton_Click(object sender, RoutedEventArgs e)
            {
            // Retrieve input from the UI
            string username = UsernameInput.Text.Trim();
            string password = PasswordInput.Password; // Use PasswordBox for secure input
            string role;

            // Call the AuthenticateUser method from DatabaseManager
            if (DatabaseManager.AuthenticateUser(username, password, out role))
                {
                tb_info.Foreground = Brushes.Green;
                tb_info.Text = "Successful login.";

                // Navigate to the main window
                GotoMainWindow();
                
                }
            else
                {
                tb_info.Foreground = Brushes.Red;
                tb_info.Text = "Invalid username or password.";
                ResetStatusText();
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

