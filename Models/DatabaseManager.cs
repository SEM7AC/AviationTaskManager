using Microsoft.Data.Sqlite;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;

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



        // Create specific tables
        public static void CreateTaskGroupsTable()
            {
            string sql = @"
            CREATE TABLE IF NOT EXISTS TaskGroups (
            TaskGroupId INTEGER PRIMARY KEY, 
            AircraftTailNumber TEXT NOT NULL, 
            GroupName TEXT NOT NULL);";
            ExecuteNonQuery(sql);
            Debug.WriteLine("TaskGroups table created successfully.");
            }

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
                            insertCommand.Parameters.AddWithValue("@PasswordHash", "admin123"); // Hash this in production!
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


        // Initialize Database
        public void InitializeDatabase()
            {
            if (DatabaseExists())
                {
                Debug.WriteLine("Database file already exists. Skipping creation.");
                return;
                }

            CreateFile(); // Create new database file
            CreateUserTable(); // Initialize tables
            CreateTaskGroupsTable();
            CreateUpdateTrigger();
            SeedUsers();
            }
        }
    } ///END OF FILE

