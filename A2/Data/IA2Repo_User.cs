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
    public interface IA2Repo_User{

    // User
    public bool ValidEmail(string email);

    public bool CustomerEmailAvailability(string email);


    public void RegisterCustomer(Customer customer);
    

    public bool LoginCustomer(string email, string password);
    public bool LoginAdmin(string email, string password);

    public int GetCustomerIDByEmail(string email);

    

    //Product
    }



    



    //Order
}