using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace A2.Model
{
	public class Order
	{
        [Key]
   		public int OrderID { get; set; }
		public int CustomerID { get; set; }
		public string ProductID_Quantity { get; set; }
		public DateOnly Date { get; set; } = new DateOnly();
		public double TotalPrice { get; set; }
        public string ShoppingStatus { get; set; }
		public string DeliverMethod { get; set; }
/*		public bool RefundStatus { get; set; } = false;*/
	}
}

