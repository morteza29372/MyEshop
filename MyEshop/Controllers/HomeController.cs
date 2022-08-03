using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyEshop.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MyEshop.Data;
using Microsoft.EntityFrameworkCore;

namespace MyEshop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private MyEshopContext _context;

        private static Cart _cart = new Cart();
        public HomeController(ILogger<HomeController> logger, MyEshopContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var product = _context.Products.ToList();
            return View(product);
        }

        public IActionResult Detail(int id)
        {
            var product = _context.Products.Include(c => c.Item).
                SingleOrDefault(p => p.ID == id);

            if(product==null)
            {
                return NotFound();
            }

            var categories = _context.Products.Where(p => p.ID == id).
                SelectMany(c => c.CategoryToProducts).Select(ca => ca.Category).ToList();

            var vm = new DetailViewModel
            {
                categories = categories,
                Product = product

            };
            return View(vm);
        }

        public IActionResult AddToCart( int ItemId)
        {
            var product = _context.Products.Include(i => i.Item).SingleOrDefault(p => p.Item.ID == ItemId);
            if(product != null) 
            {
                var cartitem = new CartItem
                {
                    item = product.Item,
                    Quantity = 1
                };
                _cart.AddItem(cartitem);


            }

          return  RedirectToAction("ShowCart");
          
        }

        public IActionResult ShowCart()
        {
            var cartvm = new CartViewModel
            {
                cartItems=_cart.cartItems,
                OrderTotal=_cart.cartItems.Sum(C=> C.GetTotalPrice())
            };

            return View(cartvm);
        }


        public IActionResult ContactUs()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
