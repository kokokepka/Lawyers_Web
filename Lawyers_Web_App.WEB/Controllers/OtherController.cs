using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Lawyers_Web_App.BLL.DTO;
using Lawyers_Web_App.BLL.DTO.OtherDTO;
using Lawyers_Web_App.BLL.Infrastructure;
using Lawyers_Web_App.BLL.Interfaces;
using Lawyers_Web_App.BLL.Interfaces.Cases;
using Lawyers_Web_App.BLL.Interfaces.Other;
using Lawyers_Web_App.WEB.Models;
using Lawyers_Web_App.WEB.Models.Other;
using Microsoft.AspNetCore.Mvc;

namespace Lawyers_Web_App.WEB.Controllers
{
    public class OtherController : Controller
    {      
        private readonly INoteService _noteService;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public OtherController(INoteService noteService, IAccountService accountService, IMapper mapper)
        {
            _noteService = noteService;
            _accountService = accountService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult MakeNote()
        {
            try
            {
                string login = User.Identity.Name;
                if (login != null)
                {
                    UserDTO user = _accountService.GetUser(login);
                    NoteViewModel note = new NoteViewModel { UserId = user.Id };
                    return PartialView(note);
                }

            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult MakeNote(NoteViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _noteService.MakeNote(new NoteDTO()
                    {
                        Title = model.Title,
                        Text = model.Text,
                        IsDone = false,
                        Date = model.Date,
                        Time = model.Time,
                        UserId = model.UserId
                    });
                    return RedirectToAction("AddNodeMessage", "Other");
                }
                catch (ValidationException ex)
                {
                    ModelState.AddModelError(ex.Property, ex.Message);
                }

            }
            return PartialView(model);
        }

        [HttpGet]
        public IActionResult MyNotes(int userId)
        {
            try
            {
                var notes = _noteService.GetUserNotes(userId);
                var model = _mapper.Map<IEnumerable<NoteViewModel>>(notes);
                return PartialView(model);
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult DeleteNote(int id)
        {
            _noteService.DeleteNote(id);
            return View();
        }
    }
}
