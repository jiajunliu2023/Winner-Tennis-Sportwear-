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
// using Google.Apis.Auth;

namespace A2.Data
{
    public interface IA2Repo_Refund{
        public string AddRefund(int CustomerID, int OrderID, DateOnly Date, string reason);
        public string ProccesRefund (int RefundID, string status);
        public IEnumerable<Refund> GetRefunds();
        public Refund GetRefundByRefundID(int id);
        public string GetRefundByOrderID(int id);
        public void DeleteRefund(int ID);
    }
}