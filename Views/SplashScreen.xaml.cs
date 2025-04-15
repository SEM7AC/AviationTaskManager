using AviationTaskManager.Models;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace AviationTaskManager.Views
    {
    public partial class SplashScreen : Window
        {
        public SplashScreen()
            {
            InitializeComponent();
            InitializeDatabaseAsync(); // Start the database initialization process
            }

        private async void InitializeDatabaseAsync()
            {
            try
                {
                // Step 1: Show initial loading message
                status_txt.Content = "Initializing...";
                await Task.Delay(750); // Simulate a brief delay

                // Check if database exists 
                if (!DatabaseManager.DatabaseExists())
                    {
                    status_txt.Content = "Creating database...";
                    await Task.Delay(750); // Simulate delay
                    DatabaseManager.CreateFile();
                    }
                else
                    {
                    status_txt.Content = "Loading existing Database";
                    await Task.Delay(750);
                    }

                // Step 2: Create tables
                status_txt.Content = "Creating tables...";
                await Task.Delay(500);
                DatabaseManager.CreateUserTable();
                DatabaseManager.CreateTaskGroupsTable();
                DatabaseManager.CreateUpdateTrigger(); //more tables if needed

                // Step 3: Seed preloaded data
                status_txt.Content = "Seeding data...";
                await Task.Delay(750);
                DatabaseManager.SeedUsers(); // Ensure admin has access

                // Step 4: Final step before starting the app
                status_txt.Content = "Initialization complete!";
                await Task.Delay(750);

                // Transition to the Login window
                var login = new Login();
                login.Show();
                this.Close();
                }
            catch (Exception ex)
                {
                MessageBox.Show($"An error occurred: {ex.Message}");
                this.Close(); // Close the splash screen in case of failure
                }
            }
        }
    }
