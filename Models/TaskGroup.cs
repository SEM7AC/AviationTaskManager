using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AviationTaskManager.Models
    {
    public class TaskGroup
        {
        public int TaskGroupId { get; set; } // Primary key
        public string AircraftTailNumber { get; set; } // Aircraft identifier
        public string GroupName { get; set; } // Task group name
        }
    }
