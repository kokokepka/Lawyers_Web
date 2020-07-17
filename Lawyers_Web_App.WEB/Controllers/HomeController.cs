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

namespace Lawyers_Web_App.WEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IDocumentService _docService;

        public HomeController(ILogger<HomeController> logger, IDocumentService documentService)
        {
            _logger = logger;
            _docService = documentService;
        }

        public IActionResult Index()
        {
            IEnumerable<DocumentDTO> docDtos = _docService.GetDocs();
            IList<DocumentViewModels> documentViews = new List<DocumentViewModels>();
            foreach(var item in docDtos)
            {
                documentViews.Add(new DocumentViewModels
                {
                    Id = item.Id,
                    Name = item.Name,
                    Path = item.Path,
                    UserId = item.UserId,
                    Date = item.Date
                });
            }
            return View(documentViews);
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
