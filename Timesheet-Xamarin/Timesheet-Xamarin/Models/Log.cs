using System;
using System.Collections.Generic;
using System.Text;

namespace Timesheet_Xamarin.Models
{
    class Log
    {
        public int ID { get; }
        public DateTime StartTime { get; set; }
        public DateTime StopTime { get; set; }
        public string Description { get; set; }
        public Project Project { get; set; }
    }
}
