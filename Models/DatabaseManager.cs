using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AviationTaskManager.Models
{
    public class DatabaseManager
    {
        private const string ConnectionString = "Data Source=C:\\Projects\\AviationTaskManager\\AviationTaskManager.db";

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

            string sql = "CREATE TABLE IF NOT EXISTS TaskGroups (TaskGroupId INTEGER PRIMARY KEY, AircraftTailNumber TEXT NOT NULL, GroupName TEXT NOT NULL);";

            using (var command = new SqliteCommand(sql, _connection))
            {
                command.ExecuteNonQuery(); // Executes the SQL command
                Console.WriteLine("TaskGroups table created successfully!");
            }

            Disconnect(); // Close the connection when done
        }

        public void CreateUserTable()
            {
            EnsureConnection();
            string sql = "CREATE TABLE IF NOT EXISTS Users (UserId INTEGER PRIMARY KEY, AUTOINCREMENT, UserName TEXT NOT NULL UNIQUE, PasswordHash TEXT NOT NULL, Role TEXT NOT NULL, CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP, UpdatedAt DATETIME;";
            
            using (var command = new SqliteCommand(sql, _connection))
                {
                command.ExecuteNonQuery();
                Console.WriteLine("User table created successfully!");
                }
            
                Disconnect();

            }

            
    }
}
