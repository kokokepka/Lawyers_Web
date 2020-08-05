using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public CaseController(ICaseService caseService)
        {
            _caseService = caseService;
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

        [HttpGet]
        public IActionResult MyCases(int id)
        {
            //try
            //{
            //    var cases = _caseService.GetUserCases(id);
            //    var model = _mapper.Map<IEnumerable<CaseViewModel>>(cases);
            //    return PartialView(model);
            //}
            //catch (ValidationException ex)
            //{
            //    ModelState.AddModelError(ex.Property, ex.Message);
            //}
            return RedirectToAction("Index", "Home");
        }

    }
}
