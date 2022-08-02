using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyEshop.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MyEshop.Data;

namespace MyEshop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private MyEshopContext _context;
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

        public IActionResult Detail()
        {
            return null;
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
