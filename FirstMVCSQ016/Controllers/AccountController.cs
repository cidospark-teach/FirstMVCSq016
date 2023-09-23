using FirstMVCSQ016.Data.Entities;
using FirstMVCSQ016.Services;
using FirstMVCSQ016.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
//using Newtonsoft.Json.Linq;

namespace FirstMVCSQ016.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailService _emailService;

        public AccountController(IAccountService accountService, UserManager<AppUser> userManager, IEmailService emailService)
        {
            _accountService = accountService;
            _userManager = userManager;
            _emailService = emailService;
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


        [HttpGet]
        public IActionResult ResetPasword(string Email, string token)
        {
            var viewObj = new ResetPasswordViewModel();
            if (string.IsNullOrEmpty(token))
            {
                ViewBag.ErrToken = "Token is required";
            }
            if (string.IsNullOrEmpty(Email))
            {
                ViewBag.ErrEmail = "Email is required";
            }
                    
            viewObj.Token = token;
            viewObj.Email = Email;

            return View(viewObj);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPasword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if(user != null)
                {
                    var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }

                    foreach(var err in result.Errors)
                    {
                        ModelState.AddModelError(err.Code, err.Description);
                    }

                }
                ViewBag.ErrEmail = "Email is invlaid";
            }

            return View(model);
        }


        [HttpGet]
        public IActionResult ForgotPassword()
        {
            if (_accountService.IsLoggedInAsync(User))
                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (_accountService.IsLoggedInAsync(User))
                return RedirectToAction("Index", "Home");

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if(user != null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var link = Url.Action("ResetPasword", "Account", new { model.Email, token }, Request.Scheme);

                    if (!string.IsNullOrEmpty(link))
                    {
                        //ViewBag.Link = link;
                        if (await _emailService.SendAsync(model.Email, "Reset Password Link", link))
                        {
                            ViewBag.Err = "A reset password link has been sent to the email provided. Please goto email and click on the link to continue";
                        }
                        else
                        {
                            ViewBag.Err = "Failed to send a reset password link. Please try agin later.";
                        }
                    }

                }
                
            }
            return View(model);
        }
    }
}
