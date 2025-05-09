﻿using AviationTaskManager.Models;
using System.Windows;

namespace AviationTaskManager.Views
    {
    /// <summary>
    /// Interaction logic for CreateUser.xaml
    /// </summary>
    public partial class CreateUser : Window
        {
        public CreateUser()
            {
            InitializeComponent();
            }

        private async void CreateUserButton_Click(object sender, RoutedEventArgs e)
            {
            string username = UsernameInput.Text.Trim();
            string password = PasswordInput.Password.Trim();
            string role = AdminRole.IsChecked == true ? "Admin" : MechRole.IsChecked == true ? "Mech" : "";

            // Validate username
            if (string.IsNullOrEmpty(username) || username.Length < 4 || !char.IsLetter(username[0]))
                {
                tb_info.Text = "Username must start with a letter and be at least 4 characters!";
                return;
                }

            // Validate role selection
            if (string.IsNullOrEmpty(role))
                {
                tb_info.Text = "Please select a role!";
                return;
                }

            // If validation passes, move forward
            tb_info.Text = "Validation successful! Ready for database insertion.";
            
            // Call DB Manager methods to handle creation or password update
            tb_info.Text = DatabaseManager.CreateUser(username, password, role);
            await Task.Delay(2000);
            this.Close();


            }




        private void CancelUserCreation(object sender, RoutedEventArgs e)
            {
            this.Close();
            }
        
        
        } // END OF CLASS
    
    } // END OF FILE
