using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using A2.Model;
using A2.Data;
using A2.Dtos;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace A2.Controllers
{
    [Route("api")]
    [ApiController]
    public class A2Controller_Order : Controller
    {
        private readonly IA2Repo_Order a2Repo;
        public A2Controller_Order(IA2Repo_Order repo)
        {
            a2Repo = repo;
        }
        [HttpPost("AddOrderToCart")]
        public ActionResult<string> AddOrderToCart(Product_QuantityInDtos AddedOrder)
        {
            return Ok(a2Repo.AddOrderToCart(AddedOrder));

        }

        [HttpGet("GetOrderByOrderID")]
        public ActionResult<Order> GetOrderByOrderID(int id)
        {
            return Ok(a2Repo.GetOrderByOrderID(id));
        }

        [HttpGet("GetAllCustomersOrdersByCustomerID")]
        public ActionResult<Order> GetAllCustomersOrdersByCustomerID(int id)
        {
            return Ok(a2Repo.GetAllCustomersOrdersByCustomerID(id));
        }

        [HttpGet("GetAllCustomersCurrentOrdersByCustomerID")]
        public ActionResult<Order> GetAllCustomersCurrentOrdersByCustomerID(int id)
        {
            return Ok(a2Repo.GetAllCustomersCurrentOrdersByCustomerID(id));
        }

        [HttpGet("GetAllCustomersPastOrdersByCustomerID")]
        public ActionResult<Order> GetAllCustomersPastOrdersByCustomerID(int id)
        {
            return Ok(a2Repo.GetAllCustomersPastOrdersByCustomerID(id));
        }

        [HttpPost("CheckOut")]
        public ActionResult<Order> CheckOut(CheckOutInDtos ChechOutData)
        {
            return Ok(a2Repo.CheckOut(ChechOutData));
        }

        [HttpGet("GetCustomerIDByOrder{OrderID}")]
        public ActionResult<int>GetCustomerIDByOrder(int OrderID){
            int customerID = a2Repo.GetCustomerIDByOrder(OrderID);
            return customerID;
        }

        [HttpGet("CheckOrder")]

        public ActionResult<string>CheckOrder(int OrderID, int CustomerID){
            if (a2Repo.CheckValidOrder(OrderID, CustomerID)){
                return Ok("true");
            }
            return Ok("false");
        }

    }
}
