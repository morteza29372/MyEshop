using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyEshop.Data;
using MyEshop.Models;
using System.IO;

namespace MyEshop.Pages.Admin
{
    public class AddModel : PageModel
    {
        private MyEshopContext _context;

        public AddModel(MyEshopContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AddEditProductViewModel Product{ get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var item = new Item()
            {
                Price = Product.Price,
                QuantityInStock = Product.QuantityInStock

            };
            _context.Add(item);
            _context.SaveChanges();

            var pro = new Product()
            {
                Name = Product.Name,
                Description = Product.Description,
                Item = item
            };
            _context.Add(pro);
            _context.SaveChanges();
            pro.ItemID = pro.ID;
            _context.SaveChanges();

            if (Product.Picture?.Length > 0)
            {
                string filepath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot",
                    "Images",pro.ID+Path.GetExtension(Product.Picture.FileName));

                using (var stream=new FileStream(filepath,FileMode.Create))
                {
                    Product.Picture.CopyTo(stream);
                }
            }


            return RedirectToPage("Index");
        }
    }
}
