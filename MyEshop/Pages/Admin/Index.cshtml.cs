using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MyEshop.Data;
using MyEshop.Models;

namespace MyEshop.Pages.Admin
{
    public class IndexModel : PageModel
    {
        private MyEshopContext _context;

        public IndexModel(MyEshopContext context)
        {
            _context = context;
        }
        public IEnumerable<Product> Products { get; set; }

        public void OnGet()
        {
            Products = _context.Products.Include(o=> o.Item);
        }

        public void OnPost()
        {

        }
    }
}
