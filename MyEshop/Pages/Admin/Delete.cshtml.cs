using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyEshop.Data;
using MyEshop.Models;
using System.IO;
using System.Linq;

namespace MyEshop.Pages.Admin
{
    public class DeleteModel : PageModel
    {
        private MyEshopContext _context;


        public DeleteModel(MyEshopContext context)
        {
            _context = context;
        }

        [BindProperty] 
        public AddEditProductViewModel Product { get; set; }

        public void OnGet(int id)
        {
            Product = _context.Products.Include(p => p.Item).Where(c => c.ID == id)
                .Select(s => new AddEditProductViewModel()
                {
                    Id = s.ID,
                    Name = s.Name,
                    Description = s.Description,
                    Price = s.Item.Price,
                    QuantityInStock = s.Item.QuantityInStock

                }).FirstOrDefault();
        }

        public IActionResult OnPost()
        {
            var product = _context.Products.Find(Product.Id);
            var item = _context.Items.First(p => p.ID == product.ItemID);
            _context.Items.Remove(item);
            _context.Products.Remove(product);
            _context.SaveChanges();

            string filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot",
                "Images", product.ID+".jpg");
            if (System.IO.File.Exists(filepath))
            {
                System.IO.File.Delete(filepath);
            }

            return RedirectToPage("Index");
        }
    }
}
