using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.IO;
using System.Data;
using A2.Model;
using A2.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using Microsoft.AspNetCore.Http;
// using Google.Apis.Auth;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;

namespace A2.Data
{
    public class A2Repo_Product : IA2Repo_Product
    {
        private readonly A2_DBContext DbContext;

        public A2Repo_Product(A2_DBContext dbContext)
        {
            DbContext = dbContext;
        }
        public void SaveChanges()
        {
            DbContext.SaveChanges();
        }
        public Product AddProduct(Product product){
            Product p = new Product();
            p.ProductID = product.ProductID;
            p.ProductName = product.ProductName;
            p.Category = product.Category;
            p.color = product.color;
            p.quantity = product.quantity;
            p.Description = product.Description;
            p.size =product.size;
            if (product.discount != 0){
                p.Price = product.Price * (1-product.discount);
            }
            else {
                p.Price = product.Price;
            }
            p.discount = product.discount;
            
            DbContext.Products.Add(p);
            DbContext.SaveChanges(); 
            return p;

        }

        public Product GetProductByProductID(int id){
            Product p = DbContext.Products.FirstOrDefault(e=> e.ProductID == id);
            return p;
        }

        public float GetProductPriceByProductID(int id){
            Product p = DbContext.Products.FirstOrDefault(e=> e.ProductID == id);
            return p.Price;
        }
        
        public IEnumerable<Product> GetProducts(){
               IEnumerable<Product> products = DbContext.Products.ToList<Product>();
               return products; 
        }
        public IEnumerable<Product> GetProductByName(string productName){
              List<Product> products = new List<Product>();
              
              foreach (Product p in DbContext.Products.ToList<Product>()){
                
                string name = p.ProductName;
                
                if (name.ToLower().Contains(productName.ToLower())){
                    products.Add(p);
                }

              }
              return products;
        }

        public IEnumerable<Product> GetProductByCategory(string productCategory){
             List<Product> products = new List<Product>();

             foreach (Product p in DbContext.Products.ToList<Product>()){
                string category = p.Category;
                if (category == productCategory){
                    products.Add(p);
                }
             }
             return products;
             
        }
        public void DeleteProduct(int ProductID){
            IEnumerable<Product> products = DbContext.Products.ToList<Product>();
            Product product = products.FirstOrDefault(e => e.ProductID == ProductID);

            if (product != null){
                DbContext.Products.Remove(product);
                DbContext.SaveChanges();
                
            }
        }
        public void UpdateProduct(Product p, ProductInDtos pi){
             if (!string.IsNullOrEmpty(pi.ProductName))
            {
                p.ProductName = pi.ProductName;
            }
            else if (!string.IsNullOrEmpty(pi.Category))
            {
                p.Category = pi.Category;
            }
            else if (!string.IsNullOrEmpty(pi.Description))
            {
                p.Description = pi.Description;
            }
            else if (pi.Price != -1)
            {
                p.Price = pi.Price;
            }
            else if (!string.IsNullOrEmpty(pi.size))
            {
                p.size= pi.size;
            }
            else if (pi.quantity != -1)
            {
                p.quantity = pi.quantity;
            }
            else if (!string.IsNullOrEmpty(pi.color))
            {
                p.color = pi.color;
            }
            else if (pi.discount != -1)
            {
                p.discount = pi.discount;
            }

            DbContext.SaveChanges();

        }

        public Product GetProductByProductName(string productName){
            Product p = DbContext.Products.FirstOrDefault(e => e.ProductName == productName);
            return p;
        }

        public IEnumerable<Review> GetReviewsByProductName (string productName){
            List<Review> reviews = new List<Review>();

             foreach (Review r in DbContext.Reviews.ToList<Review>()){
                string name  = r.ProductName;
                if (name == productName){
                    reviews.Add(r);
                }
             }
             return reviews;
        }
        public string Leave_a_review(ReviewInDtos Review)
        {
            Review review = new Review();
            Customer cus = DbContext.Customers.FirstOrDefault(e => e.CustomerID == Review.customerID);
            Product product = DbContext.Products.FirstOrDefault(e => e.ProductName == Review.ProductName);
            if (cus != null && product != null)
            {
                review.ProductName = Review.ProductName;
                review.Comment = Review.comment;
                if (string.IsNullOrEmpty(Review.Username)){
                    review.Username = "Anonymous";
                }
                else{
                    review.Username = Review.Username;
                }
                review.rating = Review.rating;
                review.CustomerID = Review.customerID;
            }
            DbContext.Reviews.Add(review);
            DbContext.SaveChanges();
            return "Successfully leave your product review";
        }
        public int GetProductIDByColorSizeName(string color, string size, string name)
        {
            return DbContext.Products.FirstOrDefault(e => e.ProductName == name && e.color == color &&  e.size == size).ProductID;
        }

    }
}