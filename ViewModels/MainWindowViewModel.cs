using AviationTaskManager.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AviationTaskManager.ViewModels
    {


    public class MainWindowViewModel
        {
        // Collection of all task groups (for binding to ComboBox)
        public ObservableCollection<TaskGroup> TaskGroups { get; set; }

        // The currently selected task group (for tracking user selection)
        public TaskGroup SelectedTaskGroup { get; set; }

        public MainWindowViewModel()
            {
            // Initialize data (fetch from the database)
            TaskGroups = new ObservableCollection<TaskGroup>(DatabaseManager.GetAllTaskGroups());
            }
        }

    } /// END OF FILE
