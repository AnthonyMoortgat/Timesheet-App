using System;
using System.Collections.Generic;
using System.Text;

namespace Timesheet_Library.Dto
{
    public class CompanyToCreateDto
    {
        public string Name { get; set; }
        public AddressToCreateDto Address { get; set; }
    }
}
