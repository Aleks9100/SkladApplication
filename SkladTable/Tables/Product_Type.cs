using System;
using System.Collections.Generic;
using System.Text;

namespace SkladTable.Tables
{
    class Product_Type
    {
        public int Product_TypeID { get; set; }
        public string Title { get; set; }
        public virtual ICollection<Product> Product { get; set; }
    }
}
