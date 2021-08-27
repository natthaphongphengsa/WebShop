using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebShop.Models;

namespace WebShop.Data
{
    public class DataInitializer
    {
        public static void SeedData(ApplicationDbContext dbContext)
        {
            dbContext.Database.Migrate();
            SeedAdmin(dbContext);
            //SeedCategory(dbContext);
            //SeedProducts(dbContext);
        }
        public static void SeedProducts(ApplicationDbContext dbContext)
        {
            var webClient = new WebClient();
            webClient.Encoding = Encoding.UTF8;
            var json = webClient.DownloadString(@"https://fakestoreapi.com/products/");
            var products = JsonConvert.DeserializeObject<List<ProductsAPI>>(json);

            foreach (var item in products)
            {
                if (!dbContext.product.Any(c => c.Name == item.title))
                {
                    dbContext.product.Add(new Product()
                    {
                        Name = item.title,
                        Description = item.description,
                        Price = item.price,
                        Image = item.image,
                        Category = dbContext.category.First(c => c.Name == item.category)
                    });
                }
            }
            dbContext.SaveChanges();
        }
        public static void SeedCategory(ApplicationDbContext dbContext)
        {
            var webClient = new WebClient();
            webClient.Encoding = Encoding.UTF8;
            var json = webClient.DownloadString(@"https://fakestoreapi.com/products/");
            var Products = JsonConvert.DeserializeObject<List<ProductsAPI>>(json);

            foreach (var name in Products)
            {
                if (!dbContext.category.Any(c => c.Name == name.category))
                {
                    dbContext.category.Add(new Category() { Name = name.category });
                    dbContext.SaveChanges();
                }
            }
        }
        public static void SeedAdmin(ApplicationDbContext dbContext)
        {            
            if (!dbContext.Roles.Any(c => c.Name == "Admin"))
            {
                dbContext.Roles.Add(new Microsoft.AspNetCore.Identity.IdentityRole("Admin"));                
            }
            var role = dbContext.Roles.First(c => c.Name == "Admin");
            var user = dbContext.Users.First(c => c.UserName == "Natthaphong@hotmail.com");
            if (!dbContext.UserRoles.Any(r => r.UserId == user.Id))
            {
                dbContext.UserRoles.Add(new Microsoft.AspNetCore.Identity.IdentityUserRole<string>() { RoleId = role.Id, UserId = user.Id });
            }

            dbContext.SaveChanges();
        }
    }
}
