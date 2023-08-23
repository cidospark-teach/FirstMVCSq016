using FirstMVCSQ016.Data.Entities;
using FirstMVCSQ016.Services;
using FirstMVCSQ016.ViewModels;
using Mailjet.Client;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
//using System;
//using System.Net;
//using System.Net.Mail;
//using Mailjet.Client;
using Mailjet.Client.Resources;
using System;
//using Newtonsoft.Json.Linq;

namespace FirstMVCSQ016.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly UserManager<AppUser> _userManager;

        public AccountController(IAccountService accountService, UserManager<AppUser> userManager)
        {
            _accountService = accountService;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Login(string ReturnUrl)
        {
            if (_accountService.IsLoggedInAsync(User))
                return RedirectToAction("Index", "Home");

            ViewBag.ReturnUrl = ReturnUrl;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string ReturnUrl)
        {
            if (_accountService.IsLoggedInAsync(User))
                return RedirectToAction("Index", "Home");

            if (ModelState.IsValid)
            {
                var user =await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    if (await _accountService.LoginAsync(user, model.Password))
                    {
                        if (string.IsNullOrEmpty(ReturnUrl))
                            return RedirectToAction("Index", "Home");
                        else
                            return LocalRedirect(ReturnUrl);
                    }
                }
                //ModelState.AddModelError("Login failed", "Invalid credential");
                ViewBag.Err="Invalid credential";
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _accountService.LogoutAsync();

            return RedirectToAction("Index", "Home");
        }

        

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
