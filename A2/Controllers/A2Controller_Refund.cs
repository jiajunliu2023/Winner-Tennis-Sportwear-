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
    public class A2Controller_Refund : Controller
    {
        private readonly IA2Repo_Refund a2Repo;
        public A2Controller_Refund (IA2Repo_Refund repo){
            a2Repo = repo;
        }
        [HttpPost("AddRefund")]
        public ActionResult<string> AddRefund(RefundInDtos refundInDtos){
            string response = a2Repo.AddRefund(refundInDtos.CustomerID, refundInDtos.OrderID, refundInDtos.Date, refundInDtos.RefundReason);

            return Ok(response);
        }
        [HttpPut("ProcessRefund")]

        public ActionResult<string> ProcessRefund (RefundProcessInDtos refundProcessInDtos){
             string response = a2Repo.ProccesRefund(refundProcessInDtos.RefundID, refundProcessInDtos.status);
             return Ok(response);
        }
        [HttpGet("ListRefund")]
        public ActionResult<IEnumerable<Refund>> ListRefund(){
            IEnumerable<Refund> refunds = a2Repo.GetRefunds();
            return Ok(refunds);
        }
        [HttpGet("GetRefundByOrderID")]
        public ActionResult<string> GetRefundByOrderID(int OrderID)
        {
            return Ok(a2Repo.GetRefundByOrderID(OrderID));
        }

        [HttpDelete("DeleteRefund")]
        
        public ActionResult<string> DeleteRefund(int RefundID)
        {
            Refund refund = a2Repo.GetRefundByRefundID(RefundID);
            
            
            if (refund != null)
            {
                a2Repo.DeleteRefund(RefundID);
                return Ok("delete successfully");

            }
            return Ok("Not found");

        }


    }
}
