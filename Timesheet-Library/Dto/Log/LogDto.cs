using System;
using System.Collections.Generic;
using System.Text;

namespace Timesheet_Library.Dto.Log
{
    public class LogDto
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int ProjectID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime StopTime { get; set; }
        public string Description { get; set; }
    }
}