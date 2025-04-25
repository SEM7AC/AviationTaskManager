using AviationTaskManager.Models;
using AviationTaskManager.ViewModels;
using System.Windows;
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

            var loginVM = DataContext as LoginViewModel; // Reference ViewModel
            if (loginVM == null) return; // Ensure ViewModel exists
            // Retrieve input from the UI
            string username = UsernameInput.Text.Trim();
            string password = PasswordInput.Password; // Secure input
            string role;

            // Call AuthenticateUser to get the role
            if (DatabaseManager.AuthenticateUser(username, password, out role))
                {
                tb_info.Foreground = Brushes.Green;
                loginVM.StatusMessage = "Successful login.";  // ✅ UI updates via binding
                await Task.Delay(1000);

                // Set the role in ViewModel
                MainWindowViewModel viewModel = new MainWindowViewModel();
                viewModel.CurrentUserRole = role; // Assign fetched role

                // Pass ViewModel to MainWindow
                MainWindow mainWindow = new MainWindow
                    {
                    DataContext = viewModel // Ensuring DataContext contains updated role
                    };

                await Task.Delay(1000);
                mainWindow.Show();
                this.Close(); // Close login window
                }
            else
                {
                tb_info.Foreground = Brushes.Red;
                loginVM.StatusMessage = "Invalid username or password.";  // ✅ UI updates via binding
                await ResetStatusText(); // Reset message
                
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

