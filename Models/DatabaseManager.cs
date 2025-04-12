using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AviationTaskManager.Models
{
    public class DatabaseManager
    {
        private static readonly string DbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AviationTaskManager.db");
        private readonly string ConnectionString = $"Data Source={DbPath};";


        private SqliteConnection? _connection;

        

        // Ensures the connection is open
        private void EnsureConnection()
        {
            if (_connection == null)
            {
                _connection = new SqliteConnection(ConnectionString);
            }

            if (_connection.State != System.Data.ConnectionState.Open)
            {
                _connection.Open();
            }
        }

        // Disconnect method
        public void Disconnect()
        {
            if (_connection != null)
            {
                _connection.Close();
                _connection = null; // Release the reference
            }
        }

        // Creates the TaskGroups table
        public void CreateTaskGroupsTable()
        {
            EnsureConnection(); // Ensure the connection is open

            string sql = @"

                CREATE TABLE IF NOT EXISTS TaskGroups (
                TaskGroupId INTEGER PRIMARY KEY, 
                AircraftTailNumber TEXT NOT NULL, 
                GroupName TEXT NOT NULL
                );";

            using (var command = new SqliteCommand(sql, _connection))
            {
                command.ExecuteNonQuery(); // Executes the SQL command
                Debug.WriteLine("TaskGroups table created successfully.");
            }

            Disconnect(); // Close the connection when done
        }

        // Creates the Users table
        public void CreateUserTable()
            {
            EnsureConnection();
            string sql = @"
            
                CREATE TABLE IF NOT EXISTS Users (
                UserId INTEGER PRIMARY KEY AUTOINCREMENT, 
                UserName TEXT NOT NULL UNIQUE, 
                PasswordHash TEXT NOT NULL, 
                Role TEXT NOT NULL, 
                CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP, 
                UpdatedAt DATETIME DEFAULT CURRENT_TIMESTAMP
                );";
            
            using (var command = new SqliteCommand(sql, _connection))
                {
                command.ExecuteNonQuery();
                Debug.WriteLine("User table created successfully!");
                }
            
                Disconnect();

            }

        // Creates a TIME STAMP update trigger for Users Table
        public void CreateUpdateTrigger()
            {
            EnsureConnection();

            string sql = @"
                CREATE TRIGGER IF NOT EXISTS UpdateUsersTimestamp
                AFTER UPDATE ON Users
                FOR EACH ROW
                BEGIN
                    UPDATE Users
                    SET UpdatedAt = CURRENT_TIMESTAMP
                    WHERE UserId = OLD.UserId;
                END;";

            using (var command = new SqliteCommand(sql, _connection))
                {
                command.ExecuteNonQuery();
                Debug.WriteLine("UpdateUsersTimestamp trigger created successfully.");
                }
            }

        // Initializes the Database
        public void InitializeDatabase()
            {
            EnsureConnection();

            // Check if the Users table exists
            string checkUsersTable = "SELECT name FROM sqlite_master WHERE type='table' AND name='Users';";
            using (var command = new SqliteCommand(checkUsersTable, _connection))
            using (var reader = command.ExecuteReader())
                {
                if (!reader.HasRows) // If the table does NOT exist
                    {
                    Debug.WriteLine("Users table not found. Initializing database...");
                    CreateUserTable();
                    CreateTaskGroupsTable();
                    CreateUpdateTrigger();
                    Debug.WriteLine("Database initialization complete.");
                    }
                else
                    {
                    Debug.WriteLine("Database already initialized. Skipping setup.");
                    }
                }

            Disconnect();
            }







        }
    }
