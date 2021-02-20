﻿using System;
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
            try
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
                        Status = Status.User
                    };
                }
                Users.Add(user);
                SaveChanges();
                return "Запись успешно добавлена";
            }
            catch (Exception ex) { return ex.Message; }
        }
        public string AddUnit_Of_Measurement(string full_name, string short_name)
        {
            try 
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
            catch (Exception ex) { return ex.Message; }
        }
        public string AddProduct_Type(string title)
        {
            try
            {
                Product_Type product_Type = new Product_Type
                {
                    Title = title
                };
                Product_Types.Add(product_Type);
                SaveChanges();
                return "Запись успешно добавлена";
            }
            catch (Exception ex) { return ex.Message; }
        }
        public string AddProduct(string title, decimal price, int typeID, int unit, int quantity)
        {
            try
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
            catch (Exception ex) { return ex.Message; }
        }
        public string AddEmployee(string firstName, string lastname, string middleName)
        {
            try
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
            catch (Exception ex) { return ex.Message; }
        }
        public string AddOperation(OperationStatus operations, string document, int number, int quantity, DateTime? date,int employeeID, int productID)
        {
            try
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
                            Date_Of_Completion = date,
                            Result = Price_Count(Products.FirstOrDefault(x => x.ProductID == productID).Price, quantity),
                            EmplloyeeID = employeeID,
                            Product = (ICollection<Product>)Products.Where(x => x.ProductID == productID)
                        };
                        Products.FirstOrDefault(x => x.ProductID == productID).Quantity = +quantity;
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
                            Products.FirstOrDefault(x => x.ProductID == productID).Quantity = -quantity;
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
            catch (Exception ex) { return ex.Message; }
        }
        #endregion
        #region EditTables
        public string EditUser(int id,string login, string passsword, Status status)
        {
            try
            {
                var item = Users.FirstOrDefault(x => x.UserID == id);
                if (item != null)
                {
                    item.Login = login;
                    item.Password = passsword;
                    item.Status = status;
                }
                SaveChanges();
                return "Запись успешно отредактирована";
            }
            catch (Exception ex) { return ex.Message; }
}
        public string EditUnit_Of_Measurement(int id,string full_name, string short_name)
        {
            try
            {

                var item = Unit_Of_Measurements.FirstOrDefault(x => x.Unit_Of_MeasurementID == id);
                if (item != null)
                {
                    item.Full_Title = full_name;
                    item.Short_Title = short_name;
                }
                SaveChanges();
                return "Запись успешно отредактирована";
            }
            catch (Exception ex) { return ex.Message; }
        }
        public string EditProduct_Type(int id,string title)
        {
            try
            {
                var item = Product_Types.FirstOrDefault(x => x.Product_TypeID == id);
                if (item != null)
                {
                    item.Title = title;
                }
                SaveChanges();
                return "Запись успешно отредактирована";
            }
            catch (Exception ex) { return ex.Message; }
        }
        public string EditProduct(int id,string title, decimal price, int typeID, int unit, int quantity)
        {
            try
            {
                var item = Products.FirstOrDefault(x => x.ProductID == id);
                if (item != null)
                {
                    item.Title = title;
                    item.Price = price;
                    item.Product_TypeID = typeID;
                    item.Unit_Of_MeasurementID = unit;
                    item.Quantity = quantity;
                }
                SaveChanges();
                return "Запись успешно отредактирована";
            }
            catch (Exception ex) { return ex.Message; }
        }
        public string EditEmployee(int id,string firstName, string lastname, string middleName)
        {
            try
            {
                var item = Employees.FirstOrDefault(x => x.EmployeeID == id);
                if (item != null)
                {
                    item.FirstName = firstName;
                    item.LastName = lastname;
                    item.MiddleName = middleName;
                }
                SaveChanges();
                return "Запись успешно отредактирована";
            }
            catch (Exception ex) { return ex.Message; }
        }
        public string EditOperation(int id, string document, int number, int quantity,DateTime? date, int employeeID, int productID)
        {
            try
            {
                var item = Operations.FirstOrDefault(x => x.OperationID == id);
                if (item != null)
                {
                    item.Document = document;
                    item.Number_Document = number;
                    item.Quantity = quantity;
                    item.Date_Of_Completion = date;
                    item.EmplloyeeID = id;
                    item.Product = (ICollection<Product>)Products.Where(x => x.ProductID == productID);
                }
                SaveChanges();
                return "Запись успешно отредактирована";
            }
            catch (Exception ex) { return ex.Message; }
        }
        #endregion
        #region RemoveTables
        public string RemoveUser(int id)
        {
            try
            {
                int count = 0;
                foreach (var Id in Users.ToList()) 
                {
                    if (Id.Status == Status.Admin)
                        count++;
                }
                var item = Users.FirstOrDefault(x => x.UserID == id);
                if (item.Status == Status.Admin && count == 1)
                    return "Нельзя удалить последнего админа";
                Users.Remove(item);
                SaveChanges();
                return "Запись успешно удалена";
            }
            catch (Exception ex) { return ex.Message; }
        }
        public string RemoveUnit_Of_Measurement(int id)
        {
            try
            {
                var item = Unit_Of_Measurements.FirstOrDefault(x => x.Unit_Of_MeasurementID == id);
                Unit_Of_Measurements.Remove(item);
                SaveChanges();
                return "Запись успешно удалена";
            }
            catch (Exception ex) { return ex.Message; }
        }
        public string RemoveProduct_Type(int id)
        {
            try
            {
                var item = Product_Types.FirstOrDefault(x => x.Product_TypeID == id);
                Product_Types.Remove(item);
                SaveChanges();
                return "Запись успешно удалена";
            }
            catch (Exception ex) { return ex.Message; }
        }
        public string RemoveProduct(int id)
        {
            try
            {
                var item = Products.FirstOrDefault(x => x.ProductID == id);
                Products.Remove(item);
                SaveChanges();
                return "Запись успешно отредактирована";
            }
            catch (Exception ex) { return ex.Message; }
        }
        public string RemoveEmployee(int id)
        {
            try
            {
                var item = Employees.FirstOrDefault(x => x.EmployeeID == id);
                Employees.Remove(item);
                SaveChanges();
                return "Запись успешно отредактирована";
            }
            catch (Exception ex) { return ex.Message; }
        }
        public string RemoveOperation(int id)
        {
            try
            {
                var item = Operations.FirstOrDefault(x => x.OperationID == id);
                Operations.Remove(item);
                SaveChanges();
                return "Запись успешно отредактирована";
            }
            catch (Exception ex) { return ex.Message; }
        }
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

        public decimal ResultCount(int id, int count)
        {
            return Products.FirstOrDefault(i=>i.ProductID == id).Price * count;
        }
        public bool count(int count) 
        {
            return count > 0;
        }     
    }
}
