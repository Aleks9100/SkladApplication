using System;
using System.Collections.Generic;
using System.Text;

namespace SkladTable.Tables
{
    public class User
    {
        public int UserID { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public Status Status { get; set; }
    }
   
}
