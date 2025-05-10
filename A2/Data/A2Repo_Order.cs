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
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace A2.Data
{
    public class A2Repo_Order : IA2Repo_Order
    {
        private readonly A2_DBContext DbContext;

        public A2Repo_Order(A2_DBContext dbContext)
        {
            DbContext = dbContext;
        }
        public void SaveChanges()
        {
            DbContext.SaveChanges();
        }

        public string AddOrderToCart(Product_QuantityInDtos AddedOrder)
        {
            /*            string json = "{\"1\":2,\"2\":3}";
                        return JsonConvert.DeserializeObject<Dictionary<int, int>>(json);*/
            Dictionary<int, int> Add_ProductID_Quantity = JsonConvert.DeserializeObject<Dictionary<int, int>>(AddedOrder.ProductID_Quantity);
            Product p = DbContext.Products.FirstOrDefault(e => e.ProductID == Add_ProductID_Quantity.Keys.First());
            if (p != null)
            {
                double price = p.Price; 
                if(p.discount != 0)
                {
                    price = Math.Round(p.Price * (1 - p.discount), 2);
                }
                if (p.quantity >= Add_ProductID_Quantity.Values.First())
                {
                    Order o = DbContext.Orders.FirstOrDefault(e => e.CustomerID == AddedOrder.CustomerID && e.ShoppingStatus == "Pending");
                    if (o == null)
                    {
                        Order new_order = new Order();
                        new_order.CustomerID = AddedOrder.CustomerID;
                        new_order.DeliverMethod = "Pending";
                        new_order.ShoppingStatus = "Pending";
                        new_order.ProductID_Quantity = JsonConvert.SerializeObject(Add_ProductID_Quantity);
                        new_order.TotalPrice = Math.Round(price * Add_ProductID_Quantity.Values.First(), 2);
                        DbContext.Orders.Add(new_order);
                        DbContext.SaveChanges();
                        return "Added successfully!";
                    }
                    else
                    {
                        Dictionary<int, int> Old_ProductID_Quantity = JsonConvert.DeserializeObject<Dictionary<int, int>>(o.ProductID_Quantity);
                        foreach (var key in Add_ProductID_Quantity.Keys)
                        {
                            if (Old_ProductID_Quantity.ContainsKey(key))
                            {
                                Old_ProductID_Quantity[key] += Add_ProductID_Quantity[key];
                            }
                            else
                            {
                                Old_ProductID_Quantity[key] = Add_ProductID_Quantity[key];
                            }
                            o.TotalPrice += Math.Round(price * Add_ProductID_Quantity.Values.First(), 2);
                        }
                        string jsonString = JsonConvert.SerializeObject(Old_ProductID_Quantity);
                        o.ProductID_Quantity = jsonString;
                        DbContext.SaveChanges();
                        return "Added successfully!";
                    }
                }
                else
                {
                    return "Sorry, not enough quantity available!";
                }
            }
            else
            {
                return "Sorry, can not find such product!";
            }
        }
        public IEnumerable<Order> GetAllCustomersOrdersByCustomerID(int ID)
        {
            IEnumerable<Order> orders = DbContext.Orders.Where(p => p.CustomerID == ID).ToList();
            return orders;
        }
        public IEnumerable<Order> GetAllCustomersCurrentOrdersByCustomerID(int ID)
        {
            IEnumerable<Order> orders = DbContext.Orders.Where(p => p.CustomerID == ID && p.ShoppingStatus == "Pending").ToList();
            return orders;
        }
        public IEnumerable<Order> GetAllCustomersPastOrdersByCustomerID(int ID)
        {
            IEnumerable<Order> orders = DbContext.Orders.Where(p => p.CustomerID == ID && p.ShoppingStatus == "Finished").ToList();
            return orders;
        }

        public Order GetOrderByOrderID(int id)
        {
            Order o = DbContext.Orders.FirstOrDefault(e => e.OrderID == id);
            return o;
        }
        public string CheckOut(CheckOutInDtos CheckOutData)
        {
            Order o = DbContext.Orders.FirstOrDefault(e => e.CustomerID == CheckOutData.CustomerID && e.ShoppingStatus == "Pending");
            if(o != null)
            {
                Dictionary<int, int> retrievedDictionary = JsonConvert.DeserializeObject<Dictionary<int, int>>(o.ProductID_Quantity);
                foreach (var kvp in retrievedDictionary)
                {
                    Product p = DbContext.Products.FirstOrDefault(e => e.ProductID == kvp.Key);
                    if (p != null)
                    {
                        p.quantity -= kvp.Value;
                    }
                }
                o.ShoppingStatus = "Finished";
                o.DeliverMethod = CheckOutData.DeliverMethod;
                o.Date = CheckOutData.Date;
                if(CheckOutData.DeliverMethod == "express")
                {
                    o.TotalPrice += 5;
                }
                DbContext.SaveChanges();
                return "Thank you for your shopping!";
            }
            return "Sorry, your shopping cart is empty!";
        }

        public int GetCustomerIDByOrder(int OrderID){
            Order o = DbContext.Orders.FirstOrDefault(e=> e.OrderID == OrderID);
            return o.CustomerID;

        }
        public bool CheckValidOrder(int OrderId, int CustomerID){
            Order o = DbContext.Orders.FirstOrDefault(e=>e.OrderID == OrderId && e.CustomerID == CustomerID);
            if (o == null){
                return false;
            }
            
                return true;
            
            
        }


    }
}
