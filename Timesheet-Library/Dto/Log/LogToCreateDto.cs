using System;
using System.Collections.Generic;
using System.Text;

namespace Timesheet_Library.Dto.Log
{
    class LogToCreateDto
    {
        public int UserID { get; set; }
        public int ProjectID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime StopTime { get; set; }
        public string Description { get; set; }
    }
}