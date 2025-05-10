using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using A2.Model;
using A2.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;

namespace A2.Data
{
    public interface IA2Repo_Order
    {
        public string AddOrderToCart(Product_QuantityInDtos AddedOrder);
        public Order GetOrderByOrderID(int id);
        public IEnumerable<Order> GetAllCustomersOrdersByCustomerID(int ID);
        public IEnumerable<Order> GetAllCustomersCurrentOrdersByCustomerID(int ID);
        public IEnumerable<Order> GetAllCustomersPastOrdersByCustomerID(int ID);
        public string CheckOut(CheckOutInDtos ChechOutData);

        public int GetCustomerIDByOrder(int OrderID);

        public bool CheckValidOrder(int OrderId, int CustomerID);
    }
}
