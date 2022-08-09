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
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

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


        [Authorize]
        public IActionResult AddToCart( int ItemId)
        {
            var product = _context.Products.Include(i => i.Item).SingleOrDefault(p => p.Item.ID == ItemId);
            if(product != null) 
            {
                int userid = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier).ToString());
                var order = _context.Orders.FirstOrDefault(o => o.UserId == userid && !o.IsFinaly);
                if (order != null)
                {
                    var orderdetail = _context.OrderDetails
                        .FirstOrDefault(o => o.OrderId == order.OrderId && o.ProductId == product.ID);

                    if (orderdetail != null)
                    {
                        orderdetail.Count += 1;
                    }
                    else
                    {
                        _context.OrderDetails.Add(new OrderDetail()
                        {
                            OrderId = order.OrderId,
                            ProductId = product.ID,
                            Price = product.Item.Price,
                            Count = 1
                        });

                    }
                }
                else
                {
                    order = new Order()
                    {
                        IsFinaly=false,
                        CreateDate=DateTime.Now,
                        UserId=userid

                    };
                    _context.Orders.Add(order);
                    _context.SaveChanges();
                    _context.OrderDetails.Add(new OrderDetail()
                    {
                        OrderId=order.OrderId,
                        ProductId=product.ID,
                        Price=product.Item.Price,
                        Count=1
                    });
                    
                }
                _context.SaveChanges();
            }

          return  RedirectToAction("ShowCart");
          
        }

        [Authorize]
        public IActionResult ShowCart()
        {
            int userid = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier).ToString());

            var order = _context.Orders.Where(o => o.UserId == userid&& !o.IsFinaly).
                Include(o => o.OrderDetails).
                ThenInclude(o => o.Product).FirstOrDefault();

            return View(order);
        }

        public IActionResult RemoveCart(int DetailId)
        {
            var orderdetail = _context.OrderDetails.Find(DetailId);
            if (orderdetail.Count > 1)
            {
                orderdetail.Count -= 1;
                _context.Entry(orderdetail);
                _context.SaveChanges();
            }
            else
            {
                _context.Remove(orderdetail);
                _context.SaveChanges();
            }
           
            return RedirectToAction("ShowCart");
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
