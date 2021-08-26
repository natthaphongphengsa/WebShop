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
    public class ProductsListModel : PageModel
    {
        public readonly ApplicationDbContext _dbContext;
        public ProductsListModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<Product> products { get; set; } = new List<Product>();
        public void OnGet(int? id)
        {
            //products = _dbContext.product.Where(c => c.Category.Id == id).ToList();
        }
    }
}
