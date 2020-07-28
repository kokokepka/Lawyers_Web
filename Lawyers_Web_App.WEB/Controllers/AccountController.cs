using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lawyers_Web_App.BLL.DTO;
using Lawyers_Web_App.BLL.Interfaces;
using Lawyers_Web_App.WEB.Models.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Lawyers_Web_App.BLL.Infrastructure;
using Lawyers_Web_App.WEB.Models;
using Lawyers_Web_App.BLL.DTO.AccountDTO;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace Lawyers_Web_App.WEB.Controllers
{
    public class AccountController : Controller
    {
        private IAccountService _accountService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(ILogger<AccountController> logger, IAccountService accountService)
        {
            _logger = logger;
            _accountService = accountService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult RegisterUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RegisterUser(RegisterUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _accountService.Register(new UserDTO
                    {
                        Name = model.Name,
                        Surname = model.Surname,
                        Patronymic = model.Patronymic,
                        Login = model.Login,
                        Password = model.Password,
                        DateOfBirth = model.DateOfBirth,
                        Email = model.Email,
                        Phone = model.Phone
                    });
                    return View("RegistMessage", "Account");

                }
                catch (ValidationException ex)
                {
                    ModelState.AddModelError(ex.Property, ex.Message);
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                AccountDTO userDTO = _accountService.Login(model.Login, model.Password);
                if(userDTO != null)
                {
                    await Authenticate(userDTO.Login, userDTO.Role);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Неверные логин и (или) пароль");
            }
            return View(model);
        }
        
        [Authorize]
        public async Task<IActionResult> Logoff()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult RegisterClient()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegisterClient(RegisterClientViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _accountService.RegisterClient(new ClientDTO
                    {
                        Name = model.Name,
                        Surname = model.Surname,
                        Patronymic = model.Patronymic,
                        DateOfBirth = model.DateOfBirth,
                        Email = model.Email,
                        Phone = model.Phone
                    });
                    return RedirectToAction("Index", "Home");
                }
                catch(ValidationException ex)
                {
                    ModelState.AddModelError(ex.Property, ex.Message);
                }
            }
            return View(model);
        }

        private async Task Authenticate(string userLogin, string userRole)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userLogin),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, userRole)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        [HttpGet]
        public IActionResult PrivateOffice()
        {
            return View();
        }
    }
}
