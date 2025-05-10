using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace A2.Dtos
{
    public class ProductOutDtos
    {
        public int ProductID {get; set;}
		public string ProductName { get; set; }
		public string Description { get; set; }
		public string Category { get; set; }
		public float Price { get; set; }
		public int quantity { get; set; }
		public string color { get; set; }
		public string size { get; set; }

        public float discount {get; set;}
        
		

    }
}