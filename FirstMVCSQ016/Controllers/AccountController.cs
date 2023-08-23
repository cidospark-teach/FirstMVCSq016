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
        public IActionResult Message()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Message(string str)
        {
            //MailMessage mailMessage = new MailMessage();
            //mailMessage.From = new MailAddress("cidospark27@gmail.com");
            //mailMessage.To.Add("fibe21@yahoo.com");
            //mailMessage.Subject = "Mail testing";
            //mailMessage.Body = "This is test email from FirstMVCSQ016";
            ////message.IsBodyHtml = true; //to make message body as html
            ////message.Body = htmlString;

            //SmtpClient smtpClient = new SmtpClient("in-v3.mailjet.com", 465)
            //{
            //    Credentials = new NetworkCredential("0d68d72314cd0c1d507867381b96a3e1", "492c6cfd5162cc500a04b8d27e314e1d"),
            //    EnableSsl = true
            //};
            ////smtpClient.Host = "smtp.gmail.com";
            ////smtpClient.Port = 587;
            ////smtpClient.UseDefaultCredentials = false;
            ////smtpClient.Credentials = new NetworkCredential("cidospark27@gmail.com", "unclECidos2");
            ////smtpClient.EnableSsl = true;
            ////smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

            //try
            //{
            //    smtpClient.Send(mailMessage);

            //    ViewBag.Success = "Email Sent Successfully.";
            //    return View();
            //}
            //catch (Exception ex)
            //{
            //    ViewBag.Err = "Error: " + ex.Message;
            //    return View();
            //}

            try {
                                    MailjetClient client = new MailjetClient("0d68d72314cd0c1d507867381b96a3e1", "492c6cfd5162cc500a04b8d27e314e1d")
                                    {
                                        BaseAdress = "https://api.mailjet.com",
                                    };
                MailjetRequest request = new MailjetRequest
                {
                    Resource = Send.Resource,
                }
                 .Property(Send.Messages,
                 new JArray {
                         new JObject {
                             new JProperty("FromEmail", "fibe21@yahoo.com"),
                             new JProperty("FromName", "Francis"),
                             new JProperty("Recipients", new JArray
                             {
                                 new JObject
                                 {
                                     new JProperty("Email", "cidospark27@gmail.com"),
                                    new JProperty("Name", "Cidos")
                                 }
                             }),
                             new JProperty("Subject", "Greetings from Mailjet."),
                             new JProperty("Text-part", "My first Mailjet email"),
                             new JProperty("Html-part", "<h3>Dear passenger 1, welcome to <a href='https://www.mailjet.com/'>Mailjet</a>!</h3><br />May the delivery force be with you!"),
                             new JProperty("CustomID", "AppGettingStartedTest"),
                         }
                 });
                                     
                         //            new JArray {
                         //new JObject {
                         // {
                         //  "From",
                         //  new JObject {
                         //   {"Email", "cidospark27@gmail.com"},
                         //   {"Name", "Francis"}
                         //  }
                         // }, {
                         //  "To",
                         //  new JArray {
                         //   new JObject {
                         //    {
                         //     "Email",
                         //     "fibe21@yahoo.com"
                         //    }, {
                         //     "Name",
                         //     "FIbe"
                         //    }
                         //   }
                         //  }
                         // }, 
                         // {
                         //  "Subject",
                         //  "Greetings from Mailjet."
                         // }, {
                         //  "TextPart",
                         //  "My first Mailjet email"
                         // }, {
                         //  "HTMLPart",
                         //  "<h3>Dear passenger 1, welcome to <a href='https://www.mailjet.com/'>Mailjet</a>!</h3><br />May the delivery force be with you!"
                         // }, {
                         //  "CustomID",
                         //  "AppGettingStartedTest"
                         // }
                         //}
                         //            });
                                    
                                    
                                    MailjetResponse response = await client.PostAsync(request);
                                    if (response.IsSuccessStatusCode)
                                    {
                                        ViewBag.Success = "Email Sent Successfully.";
                                        return View();
                                    }
                                    else
                                    {
                                        ViewBag.Err = "Error: ";
                                        return View();
                                    }
            }
            catch(Exception ex)
            {
                ViewBag.Err = "Error: " + ex.Message;
                return View();
            }
            

        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
