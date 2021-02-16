using System;
using System.Collections.Generic;
using System.Text;

namespace SkladTable.Tables
{
    class User
    {
        public int UserID { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public Status Status { get; set; }
    }
    public enum Status 
    {
        Admin,
        User
    }
}
