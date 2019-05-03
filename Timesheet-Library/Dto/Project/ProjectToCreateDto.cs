using System;
using System.Collections.Generic;
using System.Text;

namespace Timesheet_Library.Dto.Project
{
    public class ProjectToCreateDto
    {
        public int CompanyID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}