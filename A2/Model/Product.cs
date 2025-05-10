using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace A2.Model
{
	public class Product
	{
		[Key]
		public int ProductID { get; set; }
		public string ProductName { get; set; }
		public string Description { get; set; }
		public string Category { get; set; }
		public float Price { get; set; }
		public int quantity { get; set; }
		public string color { get; set; }
		public string size { get; set; }
        
		//Whether the product is with the discount or not
		public float discount {get; set;}
		
	}
}

