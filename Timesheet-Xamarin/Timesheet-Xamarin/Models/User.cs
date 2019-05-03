using System;
using System.Collections.Generic;
using System.Text;

namespace Timesheet_Xamarin.Models
{
    class User
    {
        public int ID { get; }
        public string Token { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Address Address { get; set; }
        public List<Log> Logs { get; set; }
    }
}
