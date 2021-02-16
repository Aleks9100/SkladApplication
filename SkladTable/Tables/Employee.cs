using System;
using System.Collections.Generic;
using System.Text;

namespace SkladTable.Tables
{
   public class Employee
    {
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public virtual ICollection<Operation> Operation { get; set; }
    }
}
