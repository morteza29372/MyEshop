using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyEshop.Data;

namespace MyEshop.Controllers
{
    public class ProductController : Controller
    {
        private MyEshopContext _context;

        public ProductController(MyEshopContext context)
        {
            _context = context;
        }
        [Route("Group/{id}/{name}")]
        public IActionResult ShowProductByGroupId(int id,string name)
        {
            ViewData["GroupName"] = name;
            var product = _context.CategoryToProducts
                .Where(c => c.CategoryID == id).
                Include(p => p.Product)
                .Select(pr => pr.Product).ToList();

            return View(product);
        }
    }
}
