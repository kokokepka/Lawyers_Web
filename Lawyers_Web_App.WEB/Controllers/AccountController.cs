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
using AutoMapper;
using System.IO;

namespace Lawyers_Web_App.WEB.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly ILogger<AccountController> _logger;
        private readonly IMapper _mapper;

        public AccountController(ILogger<AccountController> logger, IAccountService accountService, IMapper mapper)
        {
            _logger = logger;
            _accountService = accountService;
            _mapper = mapper;
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
                        //Name = model.Name,
                        //Surname = model.Surname,
                        //Patronymic = model.Patronymic,
                        //DateOfBirth = model.DateOfBirth,
                        //Email = model.Email,
                        //Phone = model.Phone
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
        public IActionResult AddPhoto()
        {
            return PartialView();
        }

        [HttpPost]
        public IActionResult AddPhoto(UserViewModel userView)
        {
            //try
            //{
            //    if (userView.Avatar != null)
            //    {
            //        byte[] imageData;
            //        using (var binaryReader = new BinaryReader(userView.Avatar.OpenReadStream()))
            //        {
            //            imageData = binaryReader.ReadBytes((int)userView.Avatar.Length);
            //        }
            //        _accountService.AddUserPhoto(new UserDTO { Id = userView.Id, Avatar = imageData });
            //        return RedirectToAction("PrivateOffice", "Account");
            //    }
            //    else
            //    {
            //        return View();
            //    }
            //}
            //catch(ValidationException ex)
            //{
            //    ModelState.AddModelError(ex.Property, ex.Message);
            //}
            return View();
        } 

        [HttpGet]
        public IActionResult PrivateOffice()
        {
            try
            {
                string login = User.Identity.Name;
                if(login != null)
                {
                    UserDTO user = _accountService.GetUser(login);
                    var model = _mapper.Map<UserViewModel>(user);
                    return View(model);
                }
               
            }
            catch(ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
