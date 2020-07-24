using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Lawyers_Web_App.WEB.Models;
using Lawyers_Web_App.BLL.Interfaces;
using Lawyers_Web_App.BLL.DTO;
using Lawyers_Web_App.WEB.Models.Account;
using Lawyers_Web_App.BLL.Infrastructure;

namespace Lawyers_Web_App.WEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger, IAccountService accountService)
        {
            _logger = logger;
        }

        public IActionResult Index()
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
