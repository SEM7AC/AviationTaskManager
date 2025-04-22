using AviationTaskManager.Models;
using AviationTaskManager.ViewModels;
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
            MessageBox.Show("Create User action triggered!");
            // Open user creation form here
            }

        private void Create_Aircraft_Click(object sender, RoutedEventArgs e)
            {
            MessageBox.Show("Create Aircraft action triggered!");
            // Open user creation form here
            }

        private void Create_TaskGroup_Click(object sender, RoutedEventArgs e)
            {
            MessageBox.Show("Create TaskGroup action triggered!");
            // Open user creation form here
            }

        private void Create_Subtask_Click(object sender, RoutedEventArgs e)
            {
            MessageBox.Show("Create Subtask action triggered!");
            // Open user creation form here
            }
        }
}