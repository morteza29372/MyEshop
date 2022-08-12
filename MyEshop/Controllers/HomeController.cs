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
using ZarinpalSandbox;

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

            if (product == null)
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
        public IActionResult AddToCart(int ItemId)
        {
            var product = _context.Products.Include(i => i.Item).SingleOrDefault(p => p.Item.ID == ItemId);
            if (product != null)
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
                        IsFinaly = false,
                        CreateDate = DateTime.Now,
                        UserId = userid

                    };
                    _context.Orders.Add(order);
                    _context.SaveChanges();
                    _context.OrderDetails.Add(new OrderDetail()
                    {
                        OrderId = order.OrderId,
                        ProductId = product.ID,
                        Price = product.Item.Price,
                        Count = 1
                    });

                }
                _context.SaveChanges();
            }

            return RedirectToAction("ShowCart");

        }

        [Authorize]
        public IActionResult ShowCart()
        {
            int userid = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier).ToString());

            var order = _context.Orders.Where(o => o.UserId == userid && !o.IsFinaly).
                Include(o => o.OrderDetails).
                ThenInclude(o => o.Product).FirstOrDefault();

            return View(order);
        }
        [Authorize]
        public IActionResult Payment()
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var order = _context.Orders
                .Include(o => o.OrderDetails)
                .FirstOrDefault(o => o.UserId == userId && !o.IsFinaly);
            if (order == null)
                return NotFound();

            var payment = new Payment((int)order.OrderDetails.Sum(d => d.Price));
            var res = payment.PaymentRequest($"پرداخت فاکتور شماره {order.OrderId}",
                "http://localhost:63625/Home/OnlinePayment/" + order.OrderId, "morteza@yahoo.com", "09134135345");
            if (res.Result.Status == 100)
            {
                return Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + res.Result.Authority);
            }
            else
            {
                return BadRequest();
            }

        }

        public IActionResult OnlinePayment(int id)
        {
            if (HttpContext.Request.Query["Status"] != "" &&
                HttpContext.Request.Query["Status"].ToString().ToLower() == "ok" &&
                HttpContext.Request.Query["Authority"] != "")
            {
                string authority = HttpContext.Request.Query["Authority"].ToString();
                var order = _context.Orders.Include(o => o.OrderDetails)
                    .FirstOrDefault(o => o.OrderId == id);
                var payment = new Payment((int)order.OrderDetails.Sum(d => d.Price));
                var res = payment.Verification(authority).Result;
                if (res.Status == 100)
                {
                    order.IsFinaly = true;
                    _context.Orders.Update(order);
                    _context.SaveChanges();
                    ViewBag.code = res.RefId;
                    return View();
                }
            }

            return NotFound();
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
