using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebShop.Data;
using WebShop.Models;

namespace WebShop.Pages
{
    public class SearchProductsModel : PageModel
    {
        public List<Product> products { get; set; } = new List<Product>();
        public List<Category> categories { get; set; } = new List<Category>();
        public readonly ApplicationDbContext _dbContext;
        public string SeachInput { get; set; }

        public SearchProductsModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void OnGet(string searchinput)
        {
            SeachInput = searchinput;
            products = _dbContext.product.Where(c => c.Name.Contains(searchinput)).ToList();
            categories = _dbContext.category.ToList();
        }
    }
}
