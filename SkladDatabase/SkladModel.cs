using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using SkladTable.Tables;

namespace SkladDatabase
{
    class SkladModel:DbContext
    {

        public SkladModel() 
        {
            if (Database.EnsureCreated())
            {
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            base.OnModelCreating(modelBuilder);

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {
            var builder = new NpgsqlConnectionStringBuilder()
            {
                Host = "ec2-52-209-134-160.eu-west-1.compute.amazonaws.com",
                Port = 5432,
                Username = "lhicitahhnsdal",
                Password = "bde6b2bd7dcca614a576a259c22ee9457ba652ad86723d0e12e240fedf8c4d04",
                Database = "de3mqr6btc02n",
                SslMode = SslMode.Require,
                TrustServerCertificate = true
            };
            optionsBuilder.UseNpgsql(builder.ToString());
        }

        #region AddTables
        #endregion
        #region EditTables
        #endregion
        #region RemoveTables
        #endregion
        public DbSet<User> Users { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Product_Type> Product_Types { get; set; }
        public DbSet<Unit_Of_Measurement> Unit_Of_Measurements { get; set; }
    }
}
