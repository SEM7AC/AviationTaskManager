using Microsoft.Data.Sqlite;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Xml.Linq;

namespace AviationTaskManager.Models
    {
    public class DatabaseManager
        {
        private static readonly string DbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../AviationTaskManager.db"); //Root project directory
        public static readonly string ConnectionString = $"Data Source={DbPath};";


        // Methods //

        // Check if the database exists
        public static bool DatabaseExists()
            {
            return File.Exists(DbPath);
            }

        // Create database file
        public static void CreateFile()
            {
            SQLiteConnection.CreateFile(DbPath);
            Debug.WriteLine("Database file created successfully.");
            }

        // Execute SQL commands
        public static void ExecuteNonQuery(string sql)
            {
            using (var connection = new SqliteConnection(ConnectionString)) // Create a new connection
                {
                connection.Open(); // Open the connection
                using (var command = new SqliteCommand(sql, connection)) // Execute the SQL command
                    {
                    command.ExecuteNonQuery();
                    }
                }
            }

        public static void CreateSubTaskTable()
            {
            //Create Subtasks Table
            string sql = @"
            CREATE TABLE IF NOT EXISTS SubTasks (
            SubtaskId INTEGER PRIMARY KEY AUTOINCREMENT,
            TaskGroupId INTEGER NOT NULL,
            TaskName TEXT NOT NULL,
            Status TEXT DEFAULT 'Incomplete',
            Comment TEXT,
            FOREIGN KEY (TaskGroupId) REFERENCES TaskGroups (TaskGroupId) ON DELETE CASCADE
            );";
            ExecuteNonQuery(sql);
            Debug.WriteLine("SubTasks table created successfully");
            }

        // Create TaskGroup table
        public static void CreateTaskGroupsTable()
            {
            string sql = @"
            CREATE TABLE IF NOT EXISTS TaskGroups (
            TaskGroupId INTEGER PRIMARY KEY, 
            GroupName TEXT NOT NULL
            );";
            ExecuteNonQuery(sql);
            Debug.WriteLine("TaskGroups table created successfully.");
            }

        // Create User table
        public static void CreateUserTable()
            {
            string sql = @"
            CREATE TABLE IF NOT EXISTS Users (
            UserId INTEGER PRIMARY KEY AUTOINCREMENT, 
            UserName TEXT NOT NULL UNIQUE, 
            PasswordHash TEXT NOT NULL, 
            Role TEXT NOT NULL, 
            CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP, 
            UpdatedAt DATETIME DEFAULT CURRENT_TIMESTAMP);";
            ExecuteNonQuery(sql);
            Debug.WriteLine("Users table created successfully.");
            }

        // Create Aircraft Table
        public static void CreateAircraftTable()
            {
            string sql = @"
            CREATE TABLE IF NOT EXISTS Aircraft (
            AircraftId INTEGER PRIMARY KEY,
            TailNumber TEXT UNIQUE,
            Model TEXT,
            TotalTime REAL
            );";
            ExecuteNonQuery(sql);
            Debug.WriteLine("Aircraft table created successfully.");
            }

        public static void CreateTaskGroupAssignment()
            {
            string sql = @"
            CREATE TABLE IF NOT EXISTS TaskGroupAssignment (
            AssignmentId INTEGER PRIMARY KEY,
            AircraftId INTEGER NOT NULL,
            TaskGroupId INTEGER NOT NULL,
            FOREIGN KEY (AircraftId) REFERENCES Aircraft(AircraftId),
            FOREIGN KEY (TaskGroupId) REFERENCES TaskGroups(TaskGroupId)
            );";  
            ExecuteNonQuery(sql);
            Debug.WriteLine("TaskGroupAssignment table created successfully. ");
            }

        // Create Update trigger
        public static void CreateUpdateTrigger()
            {
            string sql = @"
            CREATE TRIGGER IF NOT EXISTS UpdateUsersTimestamp
            AFTER UPDATE ON Users
            FOR EACH ROW
            BEGIN
                UPDATE Users
                SET UpdatedAt = CURRENT_TIMESTAMP
                WHERE UserId = OLD.UserId;
            END;";
            ExecuteNonQuery(sql);
            Debug.WriteLine("UpdateUsersTimestamp trigger created successfully.");
            }

        // Seed Initial Admin User
        public static void SeedUsers()
            {
            using (var connection = new SqliteConnection(ConnectionString))
                {
                connection.Open();

                // Check if the admin user exists
                string checkQuery = "SELECT COUNT(*) FROM Users WHERE UserName = @UserName";
                using (var checkCommand = new SqliteCommand(checkQuery, connection))
                    {
                    checkCommand.Parameters.AddWithValue("@UserName", "admin");
                    int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                    // If the user doesn't exist, insert it
                    if (count == 0)
                        {
                        string insertQuery = "INSERT INTO Users (UserName, PasswordHash, Role) VALUES (@UserName, @PasswordHash, @Role)";
                        using (var insertCommand = new SqliteCommand(insertQuery, connection))
                            {
                            insertCommand.Parameters.AddWithValue("@UserName", "admin");
                            insertCommand.Parameters.AddWithValue("@PasswordHash", "JAvlGPq9JyTdtvBO6x2llnRI1+gxwIyPqCKAn3THIKk="); // Hashed with IRON HASHER 
                            insertCommand.Parameters.AddWithValue("@Role", "Admin");

                            insertCommand.ExecuteNonQuery();
                            Debug.WriteLine("Admin user created successfully.");
                            }
                        }
                    else
                        {
                        Debug.WriteLine("Admin user already exists. Skipping creation.");
                        }
                    }
                }
            }

        // Hasher for passwords
        private static string HashPassword(string password)
            {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
                {
                var bytes = System.Text.Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
                }
            }

        // Create a new user
        public static string CreateUser(string username, string plainPassword, string role)
            {
            string hashedPassword = HashPassword(plainPassword);
            string query;
            string resultMessage;

            using (var connection = new SqliteConnection(ConnectionString))
                {
                connection.Open();

                using (var checkCmd = new SqliteCommand("SELECT COUNT(*) FROM Users WHERE UserName = @UserName", connection))
                    {
                    checkCmd.Parameters.AddWithValue("@UserName", username);
                    int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (count > 0)
                        {
                        query = "UPDATE Users SET PasswordHash = @PasswordHash WHERE UserName = @UserName";
                        resultMessage = "Password updated successfully!";
                        }
                    else
                        {
                        query = "INSERT INTO Users (UserName, PasswordHash, Role) VALUES (@UserName, @PasswordHash, @Role)";
                        resultMessage = "User created successfully!";
                        }
                    }

                using (var command = new SqliteCommand(query, connection))
                    {
                    command.Parameters.AddWithValue("@UserName", username);
                    command.Parameters.AddWithValue("@PasswordHash", hashedPassword);

                    if (query.Contains("INSERT"))
                        command.Parameters.AddWithValue("@Role", role);

                    command.ExecuteNonQuery();
                    }
                }

            return resultMessage;
            }

        // Create a new Aircraft

        public static string CreateAircraft(Aircraft aircraft)
            {
            using (var connection = new SQLiteConnection("Data Source=your_database.db;Version=3;"))
                {
                connection.Open();

                // Check if the aircraft already exists by TailNumber
                string checkQuery = "SELECT COUNT(*) FROM Aircraft WHERE TailNumber = @TailNumber";
                using (var checkCommand = new SQLiteCommand(checkQuery, connection))
                    {
                    checkCommand.Parameters.AddWithValue("@TailNumber", aircraft.TailNumber);
                    int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                    if (count > 0) // Aircraft exists, update ACTT
                        {
                        string updateQuery = "UPDATE Aircraft SET TotalTime = @TotalTime WHERE TailNumber = @TailNumber";
                        using (var updateCommand = new SQLiteCommand(updateQuery, connection))
                            {
                            updateCommand.Parameters.AddWithValue("@TailNumber", aircraft.TailNumber);
                            updateCommand.Parameters.AddWithValue("@TotalTime", aircraft.TotalTime);
                            updateCommand.ExecuteNonQuery();
                            string result = "Updated aircraft successfully.";
                            return result;
                            }
                        }
                    else // New aircraft, insert it
                        {
                        string insertQuery = "INSERT INTO Aircraft (TailNumber, Model, TotalTime) VALUES (@TailNumber, @Model, @TotalTime)";
                        using (var insertCommand = new SQLiteCommand(insertQuery, connection))
                            {
                            insertCommand.Parameters.AddWithValue("@TailNumber", aircraft.TailNumber);
                            insertCommand.Parameters.AddWithValue("@Model", aircraft.Model);
                            insertCommand.Parameters.AddWithValue("@TotalTime", aircraft.TotalTime);
                            insertCommand.ExecuteNonQuery();
                            string result = "Created aircraft successfully.";
                            return result;
                            }
                        }
                    }
                }
            }

        // Authenticate Users
        public static bool AuthenticateUser(string username, string password, out string role)
            {
            role = string.Empty;

            using (var connection = new SqliteConnection(ConnectionString))
                {
                string query = "SELECT PasswordHash, Role FROM Users WHERE UserName = @Username";
                using (var command = new SqliteCommand(query, connection))
                    {
                    command.Parameters.AddWithValue("@Username", username);

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                        {
                        if (reader.Read())
                            {
                            string storedHash = reader.GetString(0); // Get stored hash
                            role = reader.GetString(1); // Get user role

                            string inputHash = HashPassword(password); // Hash input password
                            if (storedHash == inputHash)
                                {
                                return true; // Authentication successful
                                }
                            }
                        }
                    }
                }

            return false; // Authentication failed
            }

        public static List<TaskGroup> GetAllTaskGroups()
            {
            var taskGroups = new List<TaskGroup>();

            using (var connection = new SQLiteConnection(ConnectionString))
                {
                connection.Open();
                string query = "SELECT TaskGroupId, GroupName FROM TaskGroups";

                using (var command = new SQLiteCommand(query, connection))
                using (var reader = command.ExecuteReader())
                    {
                    while (reader.Read())
                        {
                        taskGroups.Add(new TaskGroup
                            {
                            TaskGroupId = reader.GetInt32(0),
                            GroupName = reader.GetString(1)
                            });
                        }
                    }
                }

            return taskGroups;
            }


        } ///END OF CLASS

    } ///END OF FILE

