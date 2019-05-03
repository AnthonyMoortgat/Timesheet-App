using System;
using System.Collections.Generic;
using System.Text;

namespace Timesheet_Library.Dto.Project
{
    public class ProjectDto
    {
        public int ID { get; set; }
        public int CompanyID { get; set;}
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
