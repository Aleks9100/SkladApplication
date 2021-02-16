using System;
using System.Collections.Generic;
using System.Text;

namespace SkladTable.Tables
{
    public class Unit_Of_Measurement
    {
        public int Unit_Of_MeasurementID { get; set; }
        public string Full_Title { get; set; }
        public string Short_Title { get; set; }
        public virtual ICollection<Product> Product { get; set; }
    }
}
