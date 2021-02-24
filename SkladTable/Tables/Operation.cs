using System;
using System.Collections.Generic;
using System.Text;

namespace SkladTable.Tables
{
   public class Operation
    {
        public int OperationID { get; set; }
        public OperationStatus OperationStatus { get; set; }
        public DateTime? Date_Of_Completion { get; set; }
        public string Document { get; set; }
        public int Number_Document { get; set; }
        public decimal Result { get; set; }
        public int Quantity { get; set; }
        public int? EmployeeID { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual ICollection<Product> Product { get; set; }
   
    }     
}
