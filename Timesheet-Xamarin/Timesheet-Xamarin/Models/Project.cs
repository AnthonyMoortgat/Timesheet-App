using System;
using System.Collections.Generic;
using System.Text;

namespace Timesheet_Xamarin.Models
{
    class Project
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Company Company { get; set; }
        public string Description { get; set; }
        public List<User> Workers { get; set; }
    }
}
