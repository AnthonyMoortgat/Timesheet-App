using System;
using System.Collections.Generic;
using System.Text;

namespace Timesheet_Xamarin.Models
{
    class Company
    {
        private int ID { get; set; }
        private string Name { get; set; }
        private List<Project> Projects { get; set; }
    }
}
