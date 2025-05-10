using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.IO;
using System.Data;
using A2.Model;
using A2.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using Microsoft.AspNetCore.Http;
// using Google.Apis.Auth;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;

namespace A2.Data
{
    public class A2Repo_User : IA2Repo_User
    {
        private readonly A2_DBContext DbContext;

        public A2Repo_User(A2_DBContext dbContext)
        {
            DbContext = dbContext;
        }
        public void SaveChanges()
        {
            DbContext.SaveChanges();
        }

        // User
        public bool ValidEmail(string email){
            string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(email);
        }
        public bool CustomerEmailAvailability(string email){
            Customer c = DbContext.Customers.FirstOrDefault(e => e.Email == email);
            if (c == null){
                return false;
            }
            return true;
        }

        public void RegisterCustomer(Customer customer){
            Customer c = new Customer();
            c.CustomerID = customer.CustomerID;
            c.FirstName = customer.FirstName;
            c.LastName = customer.LastName;
            c.Email = customer.Email;
            c.password = customer.password;
            c.address = customer.address;
            c.PhoneNumber = customer.PhoneNumber;
            DbContext.Customers.Add(c);
            DbContext.SaveChanges();
        }
        public bool LoginCustomer(string email, string password){
            Customer c = DbContext.Customers.FirstOrDefault(e =>e.Email == email && e.password == password);
            if (c != null){
                return true;
            }
            else{
                return false;
            }
        }
        public bool LoginAdmin(string email, string password){
            Admin a = DbContext.Admins.FirstOrDefault(e =>e.Email == email && e.password == password);
            if (a != null){
                return true;
            }
            else{
                return false;
            }
        }

         public int GetCustomerIDByEmail(string email){
            Customer c = DbContext.Customers.FirstOrDefault(e => e.Email == email);
            return c.CustomerID;

         }

        

        

    }
}