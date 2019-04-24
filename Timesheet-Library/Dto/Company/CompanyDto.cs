using System;
using System.Collections.Generic;
using System.Text;

namespace Timesheet_Library.Dto
{
    class CompanyDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public AddressDto Address { get; set; }
    }
}
