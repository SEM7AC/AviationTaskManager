using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AviationTaskManager.Models
    {
    public class Aircraft
        {

        public int AircraftId { get; set; } // Primary key
        public string TailNumber { get; set; } // Unique identifier
        public string Model { get; set; } // Aircraft type
        public double TotalTime { get; set; } // ACTT (Aircraft Total Time)
        }

    }
    
