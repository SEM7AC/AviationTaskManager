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

        // Connect method (calls EnsureConnection)
        public void Connect()
        {
            EnsureConnection();
            System.Console.WriteLine("Connection is ready!");
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

            string sql = "CREATE TABLE IF NOT EXISTS TaskGroups (" +
                         "TaskGroupId INTEGER PRIMARY KEY, " +
                         "AircraftTailNumber TEXT NOT NULL, " +
                         "GroupName TEXT NOT NULL);";

            using (var command = new SqliteCommand(sql, _connection))
            {
                command.ExecuteNonQuery(); // Executes the SQL command
                System.Console.WriteLine("TaskGroups table created successfully!");
            }

            Disconnect(); // Close the connection when done
        }
    }
}
