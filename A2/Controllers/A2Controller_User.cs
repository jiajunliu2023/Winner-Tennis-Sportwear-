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
// using Google.Apis.Auth;

namespace A2.Controllers
{
    [Route("api")]
    [ApiController]
    public class A2Controller_User : Controller
    {
        private readonly IA2Repo_User a2Repo;
        public A2Controller_User (IA2Repo_User repo){
            a2Repo = repo;
        }
        [HttpPost("CustomerRegister")]
        public ActionResult<string> CustomerRegister(Customer customer){
            if (a2Repo.ValidEmail(customer.Email)){
                if (a2Repo.CustomerEmailAvailability(customer.Email)){
                    return Ok("This email has been registered, please register a new email");
                }
                else{
                    a2Repo.RegisterCustomer(customer);
                    return Ok("Successfully register as a customer");
                }
            }
            else{
                return Ok("This is not a valid email");
            }
        }


            [Authorize(AuthenticationSchemes = "MyAuthentication")]
            
            [Authorize(Policy = "CustomerOnly")]
            
            [HttpGet("GetCustomerVersion")]
            
            public ActionResult<string> GetCustomerVersion()
            {
                return Ok("1.0.0 (customer)");
            }
            
            [Authorize(AuthenticationSchemes = "MyAuthentication")]
            
            [Authorize(Policy = "AdminOnly")]
            
            [HttpGet("GetAdminVersion")]
            
            public ActionResult<string> GetAdminVersion()
            {
                return Ok("1.0.0 (admin)");
            }

            [HttpGet("GetCustomerIDByEmail{Email}")]
            public ActionResult<int> GetCustomerIDByEmail(string Email){
                int CustomerID = a2Repo.GetCustomerIDByEmail(Email);
                return CustomerID;
            }
        
        
    }
}
