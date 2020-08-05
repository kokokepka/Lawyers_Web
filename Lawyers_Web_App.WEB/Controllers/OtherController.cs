using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Lawyers_Web_App.BLL.DTO;
using Lawyers_Web_App.BLL.DTO.OtherDTO;
using Lawyers_Web_App.BLL.Infrastructure;
using Lawyers_Web_App.BLL.Interfaces.Cases;
using Lawyers_Web_App.BLL.Interfaces.Other;
using Lawyers_Web_App.WEB.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lawyers_Web_App.WEB.Controllers
{
    public class OtherController : Controller
    {      
        private readonly INoteService _noteService;
        private readonly IMapper _mapper;

        public OtherController(INoteService noteService, IMapper mapper)
        {
            _noteService = noteService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        

        [HttpGet]
        public IActionResult MadeNote()
        {
            return View();
        }

        [HttpPost]
        public IActionResult MadeNote(NodeViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _noteService.MakeNote(new NoteDTO()
                    {
                        Text = model.Text,
                        IsDone = model.IsDone,
                        DateTime = DateTime.Parse(model.Date + model.Time),
                        UserId = model.UserId
                    });
                    return RedirectToAction("AddCaseMessage", "Other");
                }
                catch (ValidationException ex)
                {
                    ModelState.AddModelError(ex.Property, ex.Message);
                }

            }
            return View(model);
        }
    }
}
