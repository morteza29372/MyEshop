using System;
using Microsoft.AspNetCore.Mvc;
using MyEshop.Data.Repository;
using MyEshop.Models;

namespace MyEshop.Controllers
{
    public class AccountController : Controller
    {
        private IUserRepository _userRepository;

        public AccountController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
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

            if (_userRepository.IsExistByEmail(register.Email.ToLower()))
            {
                ModelState.AddModelError("Email","این ایمیل قبلا ثبت نام شده است");
                return View("Register");
            }

            var user = new Users()
            {
                Email = register.Email.ToLower(),
                IsAdmin = false,
                Password = register.Password,
                RegisterDate = DateTime.Now,
            };

            _userRepository.AddUser(user);

            return View("RegisterSuccess",register);
        }
    }
}
