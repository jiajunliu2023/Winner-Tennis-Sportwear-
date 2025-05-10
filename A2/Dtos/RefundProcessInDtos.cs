using A2.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace A2.Dtos
{
    public class RefundProcessInDtos{

        public int RefundID {get; set;}
        
        public string status {get; set;}
    }
}