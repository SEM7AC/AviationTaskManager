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
        }
}