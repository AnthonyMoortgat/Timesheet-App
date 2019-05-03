using System;
using System.Collections.Generic;
using System.Text;

namespace Timesheet_Xamarin.Models
{
    public class Address
    {
        public int ID { get; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string BoxNumber { get; set; }
    }
}
