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
    public class A2Repo_Refund : IA2Repo_Refund
    {
        private readonly A2_DBContext DbContext;

        public A2Repo_Refund(A2_DBContext dbContext)
        {
            DbContext = dbContext;
        }
        public void SaveChanges()
        {
            DbContext.SaveChanges();
        }
        public string AddRefund(int CustomerID, int OrderID, DateOnly Date, string reason){
               Order order= DbContext.Orders.FirstOrDefault(e=> e.OrderID == OrderID && e.DeliverMethod != "Pending");
               if (order == null){
                return "The order is not identified.";
               }
               else {
                
                DateOnly date = order.Date; 
                DateTime d1 = DateTime.Parse(date.ToString());;
                DateTime d2 = DateTime.Parse(Date.ToString());
                int Difference = Math.Abs((d2 - d1).Days);

                if (Difference <= 14){
                    Refund refund = new Refund();
                    refund.OrderID = OrderID;
                    refund.CustomerID = CustomerID;
                    refund.status = "pending";
                    refund.RefundReason = reason;
/*                    order.RefundStatus = "Refunding";*/
                    DbContext.Refunds.Add(refund);
                    DbContext.SaveChanges();
                    return "The refund request has been sumbitted.";
                }
                else{
                return "The order is over 14 days, and cannot be refunded.";
                }

                 
               }
        }
        //If the status of refuns is not pending, the response is still not identified.
        public string ProccesRefund (int RefundID, string status){
            Refund refund = DbContext.Refunds.FirstOrDefault(e=> e.RefundID == RefundID && e.status == "pending");
            if (refund == null){
                return "The refund request can not be identified.";
            }
            else{
                if (status == "accept"){
                    refund.status = "accept";
                    refund.RefundReason = "Refunded";
                    DbContext.SaveChanges();
                    
                    return "The refund request has been accepted.";
                }
                else if (status == "reject"){
                    refund.status = "reject";
                    DbContext.SaveChanges();
                    return "The refund request has been rejected.";

                }
                else{
                    return "Please input the valid status.";
                }
                
            }
            
            
        }

        public IEnumerable<Refund> GetRefunds(){
               IEnumerable<Refund> refunds = DbContext.Refunds.ToList<Refund>();;
               return refunds; 
        }

        public Refund GetRefundByRefundID(int id){
            Refund refund = DbContext.Refunds.FirstOrDefault(e=>e.RefundID == id);
            return refund;
        }
        public string GetRefundByOrderID(int id)
        {
            Refund refund = DbContext.Refunds.FirstOrDefault(e => e.OrderID == id);
            if(refund != null)
            {
                return refund.status;
            }
            return "No refund for this order";
        }
        public void DeleteRefund(int ID){
            IEnumerable<Refund> products = DbContext.Refunds.ToList<Refund>();
            Refund refund = products.FirstOrDefault(e => e.RefundID == ID);

            if (refund != null){
                DbContext.Refunds.Remove(refund);
                DbContext.SaveChanges();
                
            }
        }
    }
}