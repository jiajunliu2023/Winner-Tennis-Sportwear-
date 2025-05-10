using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A2.Model
{
    public class Review
    {
        [Key]
        public int ReviewID {get; set;}
        public string ProductName { get; set; }
        public int CustomerID { get; set; }
        public string? Username { get; set; }
        public int rating { get; set; }
        public string Comment { get; set; }
    }
}
