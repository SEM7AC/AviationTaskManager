using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AviationTaskManager.Models
    {
    public class SubTask
        {
        public int SubtaskId { get; set; } // Primary key
        public int TaskGroupId { get; set; } // Foreign key linking to TaskGroup
        public string StepName { get; set; } // Subtask description
        public string Status { get; set; } // Subtask status (e.g., Incomplete, Complete)
        public string Comment { get; set; } // Additional notes or comments
        }
    }
