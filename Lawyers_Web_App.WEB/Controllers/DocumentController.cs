using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Lawyers_Web_App.BLL.DTO;
using Lawyers_Web_App.BLL.Interfaces;
using Lawyers_Web_App.WEB.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lawyers_Web_App.WEB.Controllers
{
    public class DocumentController : Controller
    {
        private IUserDocumentService _userDocumentService;
        private IWebHostEnvironment _webHostEnvironment;

        public DocumentController(IUserDocumentService userDocumentService, IWebHostEnvironment webHostEnvironment)
        {
            _userDocumentService = userDocumentService;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddFile()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddFile(IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                // путь к папке Files
                string path = "/Files/" + uploadedFile.FileName;

                // сохраняем файл в папку Files в каталоге wwwroot
                using(var fileStream = new FileStream(_webHostEnvironment.WebRootPath+path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                _userDocumentService.MakeDoc(new UserDocDTO { Name = uploadedFile.FileName, Path = path });
            }
        }
    }
}
