using Microsoft.AspNetCore.Mvc;
using MyEshop.Models;

namespace MyEshop.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModels register)
        {
            if (!ModelState.IsValid)
            {
                return View("Register");  
            }

            return View();
        }
    }
}
