using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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

        #region Register

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

        #endregion

        #region Login

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel login)
        {
            if (!ModelState.IsValid)
            {

                return View(login);
            }

            var user = _userRepository.GetUserForLogin(login.Email, login.Password);
            if (user==null)
            {
                ModelState.AddModelError("Email","اطلاعات صحیح نمی باشد!!!");
                return View(login);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()),
                new Claim(ClaimTypes.Name, user.Email),
                new Claim("IsAdmin", user.IsAdmin.ToString()),
                // new Claim("CodeMeli", user.Email),

            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            var properties = new AuthenticationProperties
            {
                IsPersistent = login.RememberMe
            };

            HttpContext.SignInAsync(principal, properties);

            return Redirect("/");
        }


        #endregion

        #region LogOut

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Account/Login");
        }

        #endregion
    }
}
