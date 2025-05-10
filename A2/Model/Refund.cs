using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace A2.Model
{
    public class Refund{
        [Key]

        public int RefundID {get; set;}

        public int CustomerID {get; set;}

        public int OrderID {get; set;}

        public string RefundReason {get; set;}

        public string status {get; set;}
    }
}