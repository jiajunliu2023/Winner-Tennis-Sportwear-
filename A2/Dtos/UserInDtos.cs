using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace A2.Dtos
{
	public class UserInDtos
	{
		
			public string FirstName { get; set; }
		    public string LastName { get; set; }
			public string Email { get; set; }
			public string password { get; set; }
			public string address { get; set; }
			public string PhoneNumber { get; set; }
		
	}
}

