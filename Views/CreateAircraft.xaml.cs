using AviationTaskManager.Models;
using System.Windows;
using System.Windows.Controls;

namespace AviationTaskManager.Views
    {
    /// <summary>
    /// Interaction logic for CreateAircraft.xaml
    /// </summary>
    public partial class CreateAircraft : Window
        {
        public CreateAircraft()
            {
            InitializeComponent();
            }

        private void Save_Aircraft_Click(object sender, RoutedEventArgs e)
            {

            // Create a new aircraft instance
            Aircraft aircraft = new Aircraft();
                
                ComboBoxItem selectedItem = ModelDropdown.SelectedItem as ComboBoxItem;
                aircraft.Model = selectedItem?.Content.ToString(); 
                aircraft.TailNumber = TailNumberBox.Text.Trim();
               
                

            // Validate required fields
            if (string.IsNullOrWhiteSpace(aircraft.TailNumber) || string.IsNullOrWhiteSpace(aircraft.Model))
                {
                MessageBox.Show("Please fill in all required fields before saving.", "Missing Data", MessageBoxButton.OK, MessageBoxImage.Warning);
                return; // Stop execution if validation fails
                }

            // Validate and set ACTT (Aircraft Total Time)
            if (double.TryParse(ActtBox.Text, out double totalTime))
                {
                aircraft.TotalTime = totalTime;
                }
            else
                {
                aircraft.TotalTime = 0.0; // Default value for invalid or empty input
                }

            // Save aircraft data to the database
            DatabaseManager.CreateAircraft(aircraft);

            // Show success message
            MessageBox.Show("Aircraft saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
            }

        private void Cancel_Click(object sender, RoutedEventArgs e)
            {
            this.Close();
            }
        }
    }
