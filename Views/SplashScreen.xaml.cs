using AviationTaskManager.Models;
using System;
using System.Data.SQLite;
using System.Diagnostics;
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
                Debug.WriteLine("InitializeDatabaseAsync started");

                // Check if database exists 
                if (!DatabaseManager.DatabaseExists())
                    {
                    status_txt.Content = "Creating database...";
                    await Task.Delay(750); // Simulate delay
                    DatabaseManager.CreateFile();
                    Debug.WriteLine("No database creating one...");
                    }
                else
                    {
                    status_txt.Content = "Loading existing Database";
                    await Task.Delay(750);
                    Debug.WriteLine("Loading existing database");
                    }

                // Step 2: Create tables if they do not exist
                using (var connection = new SQLiteConnection(DatabaseManager.ConnectionString))
                    {
                    connection.Open();

                    // Queries to check existence
                    string sql0 = @"SELECT name FROM sqlite_master WHERE type='table' AND name='Users';";
                    string sql1 = @"SELECT name FROM sqlite_master WHERE type='table' AND name='TaskGroups';";
                    string sql2 = @"SELECT name FROM sqlite_master WHERE type='table' AND name='SubTasks';";
                    string sql3 = @"SELECT name FROM sqlite_master WHERE type='table' AND name='Aircraft';";
                    string sql4 = @"SELECT name FROM sqlite_master WHERE type='table' AND name='TaskGroupAssignment';";
                    string sql5 = @"SELECT name FROM sqlite_master WHERE type='trigger' AND name='UpdateUsersTimestamp';";

                    // Check existence of each
                    bool aircraftExist = false;
                    bool usersExist = false;
                    bool taskGroupsExist = false;
                    bool subTasksExist = false;
                    bool updateTriggerExist = false;
                    bool taskgroupAssignmentExist = false;

                    using (var cmd = new SQLiteCommand(sql0, connection))
                        {
                        usersExist = cmd.ExecuteScalar() != null;
                        }

                    using (var cmd = new SQLiteCommand(sql1, connection))
                        {
                        taskGroupsExist = cmd.ExecuteScalar() != null;
                        }

                    using (var cmd = new SQLiteCommand(sql2, connection))
                        {
                        subTasksExist = cmd.ExecuteScalar() != null;
                        }
                    using (var cmd = new SQLiteCommand(sql3, connection))
                        {
                        aircraftExist = cmd.ExecuteScalar() != null;
                        }
                    using (var cmd = new SQLiteCommand(sql4, connection))
                        { 
                        taskgroupAssignmentExist = cmd.ExecuteScalar() != null;
                        }
                    using (var cmd = new SQLiteCommand(sql5, connection))
                        {
                        updateTriggerExist = cmd.ExecuteScalar() != null;
                        }

                    if (!usersExist || !taskGroupsExist || !aircraftExist ||!subTasksExist ||!updateTriggerExist || !taskgroupAssignmentExist)
                        {
                        // Create tables and triggers
                        status_txt.Content = "Creating tables and triggers...";
                        await Task.Delay(500); // Optional delay for UI responsiveness

                        if (!usersExist)
                            {
                            DatabaseManager.CreateUserTable();
                            Debug.WriteLine("CreateUserTable called");
                            }
                        if (!taskGroupsExist)
                            {
                            DatabaseManager.CreateTaskGroupsTable();
                            Debug.WriteLine("CreateTaskGroupsTable called");
                            }
                        if (!subTasksExist)
                            {
                            DatabaseManager.CreateSubTaskTable();
                            Debug.WriteLine("CreateSubTaskTable called");
                            }
                        if (!updateTriggerExist)
                            {
                            DatabaseManager.CreateUpdateTrigger();
                            Debug.WriteLine("CreateUpdateTrigger called");
                            }
                        if (!aircraftExist)
                            {
                            DatabaseManager.CreateAircraftTable();
                            Debug.WriteLine("CreateAircraftTable called");
                            }
                        if (!taskgroupAssignmentExist)
                            {
                            DatabaseManager.CreateTaskGroupAssignment();
                            Debug.WriteLine("CreateTaskGroupsAssignment called");
                            }
                        }
                    else
                        {
                        // Tables and triggers already exist
                        status_txt.Content = "Loading existing tables and triggers...";
                        Debug.WriteLine("All tables and triggers are existing");
                        await Task.Delay(750);
                        }
                        }
                    

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
