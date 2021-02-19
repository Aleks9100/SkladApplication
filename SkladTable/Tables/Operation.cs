﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SkladTable.Tables
{
   public class Operation
    {
        public int OperationID { get; set; }
        public OperationStatus OperationStatus { get; set; }
        public string Document { get; set; }
        public int Number_Document { get; set; }
        public decimal Result { get; set; }
        public int Quantity { get; set; }
        public int? EmplloyeeID { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual ICollection<Product> Product { get; set; }
   
    }
        public enum OperationStatus
        {
            Sale,
            Purchase
        }
}
