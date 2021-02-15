using System;
using System.Collections.Generic;
using System.Text;

namespace SkladTable.Tables
{
    class Operation
    {
        public int OperationID { get; set; }
        public Operations Operations { get; set; }
        public string Document { get; set; }
        public int Number_Document{get;set;}   
        public decimal Result { get; set; }
        public decimal Price_Count (decimal price,int count ) 
        {
            return price * count;
        }
        //Связь с сотруд ником настрой.

}
        public enum Operations
        {
            Sale,
            Purchase
        }
}
