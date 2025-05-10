using A2.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace A2.Dtos
{
    public class RefundInDtos{
         public int CustomerID {get; set;}

        public int OrderID {get; set;}
        
        public DateOnly Date { get; set; } = new DateOnly();
        public string RefundReason {get; set;}
    }
}