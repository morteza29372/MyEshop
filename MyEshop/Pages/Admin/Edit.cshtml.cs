using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyEshop.Data;
using MyEshop.Models;

namespace MyEshop.Pages.Admin
{
    public class EditModel : PageModel
    {
        private MyEshopContext _context;


        public EditModel(MyEshopContext context)
        {
            _context = context;
        }

        [BindProperty] public AddEditProductViewModel Product { get; set; }

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
            if (!ModelState.IsValid)
                return Page();

            var product = _context.Products.Find(Product.Id);
            var item = _context.Items.First(p => p.ID == product.ItemID);

            product.Name = Product.Name;
            product.Description = Product.Description;
            item.Price = Product.Price;
            item.QuantityInStock = Product.QuantityInStock;

            _context.SaveChanges();

            if (Product.Picture?.Length > 0)
            {
                string filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot",
                    "Images", product.ID + Path.GetExtension(Product.Picture.FileName));

                using (var stream = new FileStream(filepath, FileMode.Create))
                {
                    Product.Picture.CopyTo(stream);
                }
            }

            return RedirectToPage("Index");
        }
    }
}
