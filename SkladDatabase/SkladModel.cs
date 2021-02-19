using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using SkladTable;
using SkladTable.Tables;

namespace SkladDatabase
{
    public class SkladModel : DbContext
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
        public string AddUser(string login, string passsword, Status status)
        {
            User user = null;
            if (status == Status.Admin)
            {
                user = new User
                {
                    Login = login,
                    Password = passsword,
                    Status = Status.Admin
                };
            }
            else if (status == Status.User)
            {
                user = new User
                {
                    Login = login,
                    Password = passsword,
                    Status = (SkladTable.Status)Status.User
                };
            }
            Users.Add(user);
            SaveChanges();
            return "Запись успешно добавлена";
        }
        public string AddUnit_Of_Measurement(string full_name, string short_name)
        {
            Unit_Of_Measurement unit_Of_ = new Unit_Of_Measurement
            {
                Full_Title = full_name,
                Short_Title = short_name
            };
            Unit_Of_Measurements.Add(unit_Of_);
            SaveChanges();
            return "Запись успешно добавлена";
        }
        public string AddProduct_Type(string title)
        {
            Product_Type product_Type = new Product_Type
            {
                Title = title
            };
            Product_Types.Add(product_Type);
            SaveChanges();
            return "Запись успешно добавлена";
        }
        public string AddProduct(string title, decimal price, int typeID, int unit, int quantity)
        {
            Product product = new Product
            {
                Title = title,
                Price = price,
                Product_TypeID = typeID,
                Unit_Of_MeasurementID = unit,
                Quantity = quantity
            };
            Products.Add(product);
            SaveChanges();
            return "Запись успешно добавлена";
        }
        public string AddEmployee(string firstName, string lastname, string middleName)
        {
            Employee employee = new Employee
            {
                FirstName = firstName,
                LastName = lastname,
                MiddleName = middleName
            };
            Employees.Add(employee);
            SaveChanges();
            return "Запись успешно добавлена";
        }
        public string AddOperation(OperationStatus operations, string document, int number, int quantity, int employeeID, int productID)
        {
            if (count(quantity))
            {
                Operation operation = null;
                if (operations == OperationStatus.Purchase)
                {
                    operation = new Operation
                    {
                        OperationStatus = operations,
                        Document = document,
                        Number_Document = number,
                        Quantity = quantity,
                        Result = Price_Count(Products.FirstOrDefault(x => x.ProductID == productID).Price, quantity),
                        EmplloyeeID = employeeID,
                        Product = (ICollection<Product>)Products.Where(x => x.ProductID == productID)
                    };
                    Products.FirstOrDefault(x => x.ProductID == productID).Quantity =+ quantity;
                    SaveChanges();   
                }
                else if (operations == OperationStatus.Sale)
                {
                    if (Products.FirstOrDefault(x => x.ProductID == productID).Quantity >= quantity)
                    {
                        operation = new Operation
                        {
                            OperationStatus = operations,
                            Document = document,
                            Number_Document = number,
                            Quantity = quantity,
                            Result = Price_Count(Products.FirstOrDefault(x => x.ProductID == productID).Price, quantity),
                            EmplloyeeID = employeeID,
                            Product = (ICollection<Product>)Products.Where(x => x.ProductID == productID)
                        };
                        Products.FirstOrDefault(x => x.ProductID == productID).Quantity =- quantity;
                        SaveChanges();
                    }
                    else return $"На складе недостаточно товара. На складе осталось товара {Products.FirstOrDefault(x => x.ProductID == productID).Quantity}";
                }
                Operations.Add(operation);
                SaveChanges();
                return "Запись успешно добавлена";
            }
            else return "Количество равно 0 или меньше";
        }
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
        public decimal Price_Count(decimal price, int count)
        {
            return price * count;
        }

        public bool count(int count) 
        {
            return count > 0;
        }     
    }
}
