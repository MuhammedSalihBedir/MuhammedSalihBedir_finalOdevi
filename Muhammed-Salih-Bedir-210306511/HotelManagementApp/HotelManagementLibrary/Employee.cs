using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementLibrary
{
    public class Employee : Person
    {
        public string EmployeeId { get; set; }
        public string Position { get; set; }

        public Employee(string name, string contact, string employeeId, string position)
            : base(name, contact)
        {
            EmployeeId = employeeId;
            Position = position;
        }
    }
}
