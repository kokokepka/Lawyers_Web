using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Lawyers_Web_App.BLL.DTO;
using Lawyers_Web_App.BLL.DTO.DocDTO;
using Lawyers_Web_App.BLL.Interfaces;
using Lawyers_Web_App.BLL.Interfaces.Documents;
using Lawyers_Web_App.WEB.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lawyers_Web_App.WEB.Controllers
{
    public class DocumentController : Controller
    {
        private IDocService<CaseDocDTO, ClientDTO> _clientDocumentService;
        private IDocService<UserDocDTO,UserDTO> _userDocumentService;
        private IWebHostEnvironment _webHostEnvironment;

        public DocumentController(IDocService<UserDocDTO, UserDTO> userDocumentService, 
            IDocService<CaseDocDTO, ClientDTO> clientDocumentService, IWebHostEnvironment webHostEnvironment)
        {
            _userDocumentService = userDocumentService;
            _clientDocumentService = clientDocumentService;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddClientFile()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddClientFile(DocViewModel model)
        {
            if (ModelState.IsValid)
            {
                if(model.UploadedFile != null)
                {
                    // путь к папке Files
                    string path = "/Files/Clients/" + model.UploadedFile.FileName;
                    // сохраняем файл в папку Files в каталоге wwwroot
                    using (var fileStream = new FileStream(_webHostEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await model.UploadedFile.CopyToAsync(fileStream);
                    }
                    _clientDocumentService.MakeDoc(new CaseDocDTO { Name = model.UploadedFile.FileName, Path = path,
                        Date = DateTime.Now.Date, CaseId = model.SomethingId});
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult AddUserFile()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddUserFile(DocViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.UploadedFile != null)
                {
                    // путь к папке Files
                    string path = "/Files/Users/" + model.UploadedFile.FileName;
                    // сохраняем файл в папку Files в каталоге wwwroot
                    using (var fileStream = new FileStream(_webHostEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await model.UploadedFile.CopyToAsync(fileStream);
                    }
                    _userDocumentService.MakeDoc(new UserDocDTO
                    {
                        Name = model.UploadedFile.FileName,
                        Path = path,
                        Date = DateTime.Now.Date,
                        UserId = model.SomethingId
                    });
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }



    }
}
