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
    public interface IA2Repo_Product{
        public Product AddProduct(Product product);
        public IEnumerable<Product> GetProducts();
        public IEnumerable<Product> GetProductByName(string productName);
        public Product GetProductByProductName(string productName);

        public IEnumerable<Product> GetProductByCategory(string productCategory);
        public void DeleteProduct(int productID);
        public void UpdateProduct(Product p, ProductInDtos pi);
        public Product GetProductByProductID(int id);
        public string Leave_a_review(ReviewInDtos Review);
        public IEnumerable<Review> GetReviewsByProductName (string productName);
        public int GetProductIDByColorSizeName(string color, string size, string name);

        }
}