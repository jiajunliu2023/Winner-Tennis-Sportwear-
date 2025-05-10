using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using A2.Model;
using A2.Data;
using A2.Dtos;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
// using Google.Apis.Auth;

namespace A2.Controllers
{
    [Route("api")]
    [ApiController]
    public class A2Controller_Product : Controller
    {
        private readonly IA2Repo_Product a2Repo;
        public A2Controller_Product (IA2Repo_Product repo){
            a2Repo = repo;
        }
        [HttpPost("AddProducts")]
        public ActionResult<Product> AddProducts(ProductInDtos pi)
        {
            Product p = new() { ProductName = pi.ProductName,  Description = pi.Description, color = pi.color, Price = pi.Price, quantity = pi.quantity, discount = pi.discount, size = pi.size, Category = pi.Category };
            
            a2Repo.AddProduct(p);

            return Ok(p);
        }

        [HttpPost("LeaveReview")]
        public ActionResult<string> LeaveReview(ReviewInDtos Review)
        {

            return a2Repo.Leave_a_review(Review);
        }

        

        [HttpGet("ListProducts")]
        public ActionResult<IEnumerable<ProductOutDtos>> ListProducts()
        {
            IEnumerable<Product> products = a2Repo.GetProducts();

            IEnumerable<ProductOutDtos> output = products.Select(e => new ProductOutDtos {ProductID = e.ProductID, ProductName = e.ProductName,  Description = e.Description, color = e.color, Price = e.Price, quantity = e.quantity, discount = e.discount, size = e.size, Category = e.Category });
            return Ok(output);
        }

        [HttpGet("GetProductByID{productID}")]
        public ActionResult<ProductOutDtos> GetProductByID(int productID){
            Product p = a2Repo.GetProductByProductID(productID);
            if (p != null)
            {
            ProductOutDtos output = new ProductOutDtos  {ProductID = p.ProductID, ProductName = p.ProductName,  Description = p.Description, color = p.color, Price = p.Price, quantity = p.quantity, discount = p.discount, size = p.size, Category = p.Category };
            return Ok(output);
                
            }
            else
            {
             return NotFound();
            }
        }
        [HttpGet("GetProductsByName/{ProductName}")]
        
        public ActionResult<IEnumerable<ProductOutDtos>> GetProductsByName(string ProductName){
            IEnumerable<Product> p = a2Repo.GetProductByName(ProductName);
            if (p != null)
            {
            IEnumerable<ProductOutDtos> output = p.Select(e => new ProductOutDtos {ProductID = e.ProductID, ProductName = e.ProductName,  Description = e.Description, color = e.color, Price = e.Price, quantity = e.quantity, discount = e.discount, size = e.size, Category = e.Category });
            return Ok(output);
                
            }
            else
            {
             return NotFound();
            }
        }

        [HttpGet("GetProductsByCategory/{ProductCategory}")]
        
        public ActionResult<IEnumerable<ProductOutDtos>> GetProductsByCategory(string ProductCategory){
            IEnumerable<Product> products = a2Repo.GetProductByCategory(ProductCategory);
            if (products != null)
            {
            IEnumerable<ProductOutDtos> output = products.Select(e => new ProductOutDtos {ProductID = e.ProductID, ProductName = e.ProductName,  Description = e.Description, color = e.color, Price = e.Price, quantity = e.quantity, discount = e.discount, size = e.size, Category = e.Category });
            return Ok(output);
                
            }
            else
            {
             return NotFound();
            }
        }
        [HttpPut("UpadateProduct")]
        
        public ActionResult<string> UpdateProject(int ProjectID, ProductInDtos pi)
        {
            Product product = a2Repo.GetProductByProductID(ProjectID);
            if (product == null)
            {
               return Ok("Not found"); 
            }
            a2Repo.UpdateProduct(product, pi);
            return Ok("Update Successfully");
            
        }


        [HttpDelete("DeletePrduct")]
        
        public ActionResult<string> DeleteTeam(int ProjectID)
        {
            Product project = a2Repo.GetProductByProductID(ProjectID);
            
            if (project != null)
            {
                a2Repo.DeleteProduct(ProjectID);
                return Ok("delete successfully");

            }
            return Ok("Not found");

        }
        [HttpPost("UploadPhoto")]
         public async Task<IActionResult> UploadPhoto(IFormFile File)
        {
            var p = Directory.GetCurrentDirectory();
            string photodir = Path.Combine(p, "Photo");

            
            var FilePath = Path.Combine(photodir, File.FileName);
            
            using var fileStream = new FileStream(FilePath, FileMode.Create);
            
            await File.CopyToAsync(fileStream);
            
            return Ok(File);

        }
        
        
        [HttpGet("GetProductPhoto/{ProductName}")]
        public ActionResult GetProductPhoto(string ProductName)
        {
            string path = Directory.GetCurrentDirectory();
            
            string idir = Path.Combine(path, "Photo");
            
            string f1 = Path.Combine(idir, ProductName + ".jpg");

            string f2 = Path.Combine(idir, ProductName + ".jpeg");
            
            string f3 = Path.Combine(idir, ProductName + ".gif");
            
            string f4 = Path.Combine(idir, ProductName + ".png");
            
            string f5 = Path.Combine(idir, ProductName + ".pdf");
            
            string respheader = "";
            
            string filename = "";
            if (System.IO.File.Exists(f1))
            {
                respheader  = "image/jpg";
                filename = f1;
            }
            else if (System.IO.File.Exists(f2)){
                 respheader = "image/jpeg";
                 filename = f2;
            }
            else if (System.IO.File.Exists(f3))
            {
                respheader = "image/gif";
                filename= f3;
            }
            else if (System.IO.File.Exists(f4))
            {
                respheader = "image/png";
                filename = f4;
            }
            else
            {
                respheader  = "image/pdf";
                filename = f5;
            }
            return PhysicalFile(filename, respheader);
        }

        [HttpGet("GetReviewByProduct/{ProductName}")]
        public ActionResult<IEnumerable<Review>> GetReviewByProduct(string ProductName){
            IEnumerable<Review> reviews = a2Repo.GetReviewsByProductName(ProductName);
            return Ok(reviews);
        }
        [HttpGet("GetProductIDByColorSizeName")]
        public ActionResult<int> GetProductIDByColorSizeName(string color, string size, string name)
        {
            return Ok(a2Repo.GetProductIDByColorSizeName(color, size, name));
        }





    }
}