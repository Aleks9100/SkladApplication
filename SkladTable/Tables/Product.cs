using System;
using System.Collections.Generic;
using System.Text;

namespace SkladTable.Tables
{
    class Product
    {
        public int ProductID { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public int? Product_TypeID { get; set; }
        public virtual Product_Type Product_Type { get; set; }
        public int? Unit_Of_MeasurementID { get; set; }
        public virtual Unit_Of_Measurement Unit_Of_Measurement { get; set; }
    }
}
