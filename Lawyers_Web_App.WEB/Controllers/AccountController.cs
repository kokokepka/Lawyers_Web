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
using Microsoft.AspNetCore.Hosting;
using Lawyers_Web_App.WEB.Models.Users;

namespace Lawyers_Web_App.WEB.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly ILogger<AccountController> _logger;
        private IWebHostEnvironment _webHostEnvironment;
        private readonly IMapper _mapper;

        public AccountController(ILogger<AccountController> logger, IAccountService accountService, IMapper mapper,  IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _accountService = accountService;
            _webHostEnvironment = webHostEnvironment;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult RegisterUser()
        {
            return PartialView();
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
                        Phone = model.Phone,
                        HomePhone = model.HomePhone,
                        Address = model.Address
                        
                    });
                    return RedirectToAction("AddUserMessage", "Account");

                }
                catch (ValidationException ex)
                {
                    ModelState.AddModelError(ex.Property, ex.Message);
                }
            }
            return PartialView();
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
        public IActionResult AddPhoto(int id)
        {
            try
            {
                UserDTO user = _accountService.GetUser(id);
                if (user != null)
                {
                    UserViewModel model = new UserViewModel { Id = user.Id };
                    return PartialView(model);
                }
                
            }
            catch(ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult AddPhoto(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (model.Foto != null)
                    {
                        string file_type = Path.GetExtension(model.Foto.FileName);
                        if (file_type != ".jpg")
                        {
                            return RedirectToAction("ErrorMessage", "Home");
                        }
                        byte[] imageData;
                        using (var binaryReader = new BinaryReader(model.Foto.OpenReadStream()))
                        {
                            imageData = binaryReader.ReadBytes((int)model.Foto.Length);
                        }
                        _accountService.AddUserPhoto(new UserDTO { Id = model.Id, Avatar = imageData });
                        return RedirectToAction("PrivateOffice", "Account");
                        //return RedirectToAction("Index");
                    }
                    else
                    {
                        return RedirectToAction("PrivateOffice", "Account");
                    }
                }
                catch (ValidationException ex)
                {
                    ModelState.AddModelError(ex.Property, ex.Message);
                }
            }
            
            return PartialView(model);
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

        [HttpGet]
        public IActionResult MyDocuments()
        {
            try
            {
                string login = User.Identity.Name;
                if (login != null)
                {
                    UserDTO user = _accountService.GetUser(login);
                    var doc = _accountService.GetUserDocuments(user.Id);
                    var model = _mapper.Map<IEnumerable<DocViewModel>>(doc);
                    return PartialView(model);
                }
                else
                    return RedirectToAction("Index", "Home");
            }
            catch(ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return RedirectToAction("PrivateOffice", "Account");
        }

        [HttpGet]
        public IActionResult AddDocUser()
        {
            try
            {
                string login = User.Identity.Name;
                if (login != null)
                {
                    UserDTO user = _accountService.GetUser(login);
                    DocViewModel model = new DocViewModel { SomethingId = user.Id };
                    return PartialView(model);
                }
                else
                    return RedirectToAction("Index", "Home");
            }
            catch(ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> AddDocUser(DocViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.UploadedFile != null)
                {
                    // путь к папке Files
                    string path = @"\Files\Users\" + model.UploadedFile.FileName;
                    // сохраняем файл в папку Files в каталоге wwwroot
                    using (var fileStream = new FileStream(_webHostEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await model.UploadedFile.CopyToAsync(fileStream);
                    }
                    _accountService.AddUserDocument(model.SomethingId, new UserDocDTO
                    {
                        Name = model.UploadedFile.FileName,
                        Path = path,
                        Date = DateTime.Now.Date,
                        UserId = model.SomethingId
                    });
                    return RedirectToAction("MyDocuments", "Account");
                }
            }
            return PartialView(model);
        }

        [HttpGet]
        public IActionResult DeleteDocument(int id, string path)
        {
            if (System.IO.File.Exists(_webHostEnvironment.WebRootPath + path))
            {
                System.IO.File.Delete(_webHostEnvironment.WebRootPath + path);
            }
            _accountService.DeleteDoc(id);
            return RedirectToAction("MyDocuments", "Account");
        }
    }
}
