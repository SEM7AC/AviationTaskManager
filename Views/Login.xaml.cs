using AviationTaskManager.Models;
using Microsoft.Data.Sqlite;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

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
			string username = UsernameInput.Text; // Get username from a textbox
			string password = PasswordInput.Password; // Get password from a password box
			string role;

			// Call AuthenticateUser method
			if (AuthenticateUser(username, password, out role))
				{
				MessageBox.Show($"Login successful! Your role is: {role}", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

				// Navigate to the next screen based on role (optional)
				if (role == "Admin")
					{
					MainWindow admin = new MainWindow();
					admin.Show();
					this.Close();
					}
				else if (role == "User")
					{
					MainWindow user = new MainWindow();
					user.Show();
					this.Close();
					}
				}
			else
				{
				MessageBox.Show("Invalid username or password. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				}

			}



		public bool AuthenticateUser(string username, string password, out string role)
			{

			role = string.Empty; // Initialize the role variable

			using (SqliteConnection connection = new SqliteConnection(DatabaseManager.ConnectionString))
				{
				string query = "SELECT Role FROM Users WHERE Username = @Username AND Password = @Password";

				SqliteCommand command = new SqliteCommand(query, connection);
				command.Parameters.AddWithValue("@Username", username);
				command.Parameters.AddWithValue("@Password", password);

				connection.Open();
				var result = command.ExecuteScalar();

				if (result != null)
					{
					role = result.ToString();
					return true; // Authentication successful
					}
                else
					{
					return false; // Authentication failed
					}
				}
            }
		}


    }

