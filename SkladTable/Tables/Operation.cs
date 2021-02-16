using System;
using System.Collections.Generic;
using System.Text;

namespace SkladTable.Tables
{
   public class Operation
    {
        public int OperationID { get; set; }
        public Operations Operations { get; set; }
        public string Document { get; set; }
        public int Number_Document { get; set; }
        public decimal Result { get; set; }
        public int Quantity { get; set; }
        public decimal Price_Count(decimal price, int count)
        {
            return price * count;
        }
        public int? EmplloyeeID { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual ICollection<Product> Product { get; set; }
   
    }
        public enum Operations
        {
            Sale,
            Purchase
        }
}
