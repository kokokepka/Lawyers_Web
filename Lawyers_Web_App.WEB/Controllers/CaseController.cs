using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Lawyers_Web_App.BLL.DTO;
using Lawyers_Web_App.BLL.DTO.CasesDTO;
using Lawyers_Web_App.BLL.Infrastructure;
using Lawyers_Web_App.BLL.Interfaces.Cases;
using Lawyers_Web_App.WEB.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lawyers_Web_App.WEB.Controllers
{
    public class CaseController : Controller
    {
        private readonly ICaseService _caseService;
        private readonly IMapper _mapper;

        public CaseController(ICaseService caseService, IMapper mapper)
        {
            _caseService = caseService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult MadeCase()
        {
            return View();
        }

        [HttpPost]
        public IActionResult MadeCase(CaseViewModel model)
        {
            if (ModelState.IsValid)
            {
                //try
                //{
                //    _caseService.StartCase(new CaseDTO()
                //    {
                //        Title = model.Title,
                //        ClientId = model.ClientId,
                //        UserId = model.UserId
                //    });
                //    return RedirectToAction("AddCaseMessage", "Other");
                //}
                //catch (ValidationException ex)
                //{
                //    ModelState.AddModelError(ex.Property, ex.Message);
                //}

            }
            return View(model);
        }

        private IEnumerable<CaseViewModel> MyCases(int id, string kindcase)
        {
            var cases = _caseService.GetUserCases(id, kindcase);
            var model = _mapper.Map<IEnumerable<CaseViewModel>>(cases);
            return model;
        }

        [HttpGet]
        public IActionResult MyCriminalCases(int id)
        {           
            try
            {
                IEnumerable<CaseViewModel> model = MyCases(id, "Уголовное дело");
                return PartialView(model);
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult MyCivilCases(int id, string kindcase)
        {
            try
            {
                IEnumerable<CaseViewModel> model = MyCases(id, "Гражданское дело");
                return PartialView(model);
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return RedirectToAction("Index", "Home");

        }

        [HttpGet]
        public IActionResult MyAdministrativeCases(int id, string kindcase)
        {
            try
            {
                IEnumerable<CaseViewModel> model = MyCases(id, "Административное дело");
                return PartialView(model);
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
