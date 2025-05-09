﻿using AviationTaskManager.Models;
using AviationTaskManager.ViewModels;
using AviationTaskManager.Views;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace AviationTaskManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel(); // Correct ViewModel
        }


        private void AircraftSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {
            if (AircraftSelector.SelectedItem is Aircraft selectedAircraft)
                {
                AircraftModel.Content = selectedAircraft.Model;
                AircraftACTT.Content = selectedAircraft.TotalTime.ToString();
                }
            else
                {
                AircraftModel.Content = "N/A";
                AircraftACTT.Content = "N/A";
                }
            }

        private void Create_User_Click(object sender, RoutedEventArgs e)
            {
            Debug.WriteLine("Create User action triggered!");
            CreateUser createUser = new CreateUser();
            createUser.ShowDialog();
            }

        private void Create_Aircraft_Click(object sender, RoutedEventArgs e)
            {
            Debug.WriteLine("Create Aircraft action triggered!");
            CreateAircraft createAircraft = new CreateAircraft();
            createAircraft.ShowDialog();
            // Open user creation form here
            }

        private void Create_TaskGroup_Click(object sender, RoutedEventArgs e)
            {
            Debug.WriteLine("Create TaskGroup action triggered!");
            // Open user creation form here
            }

        private void Create_Subtask_Click(object sender, RoutedEventArgs e)
            {
            Debug.WriteLine("Create Subtask action triggered!");
            // Open user creation form here
            }

       
        private void Logout_Click(object sender, RoutedEventArgs e)
            {
            Debug.WriteLine("Logout action triggered!");
            this.Hide();
            Login login = new Login();
            login.ShowDialog();
            this.Close();
            }
        }
}