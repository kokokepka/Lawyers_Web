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
using AutoMapper;
using Lawyers_Web_App.WEB.Models.Users;
using Lawyers_Web_App.BLL.Interfaces.Other;
using Lawyers_Web_App.WEB.Models.Other;
using Lawyers_Web_App.BLL.DTO.OtherDTO;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Lawyers_Web_App.WEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAccountService _accountService;
        private readonly ICommentService<CommentDTO> _commentService;
        private readonly IQuestionService _questionService;
        private readonly IPriceService _priceService;
        private readonly IMapper _mapper;
        public HomeController(ILogger<HomeController> logger, IAccountService accountService,
            ICommentService<CommentDTO> commentService, IQuestionService questionService,
            IPriceService priceService, IMapper mapper)
        {
            _accountService = accountService;
            _commentService = commentService;
            _questionService = questionService;
            _priceService = priceService;
            _mapper = mapper;
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
        [HttpGet]        
        public ActionResult ErrorMessage()
        {
            return PartialView();
        }

        [HttpGet]
        public IActionResult AllLowyers()
        {
            try
            {
                IEnumerable<UserDTO> users = _accountService.GetAllUsers();
                var map = _mapper.Map<IEnumerable<UserViewModel>>(users);
                return View(map);
            }
            catch(ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }           
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult AskQuestion()
        {
            return PartialView();
        }
        [HttpPost]
        public IActionResult AskQuestion(QuestionViewModel model)
        {         
            try
            {
                if (model.Text != null)
                {
                    string name = model.Name != null ? model.Name : "Гость";
                    QuestionDTO questionDTO = new QuestionDTO
                    {
                        Name = name,
                        Text = model.Text
                    };
                    _questionService.Add(questionDTO);                   
                }
                return RedirectToAction("AllQuestions", "Home");
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return RedirectToAction("Index", "Home");
               
        }

        [HttpGet]
        public IActionResult AllQuestions()
        {
            try
            {
                IEnumerable<QuestionDTO> questions = _questionService.GetAll();
                var map = _mapper.Map<IEnumerable<QuestionViewModel>>(questions);
                foreach (var item in map)
                {
                    var tmp_answer = _questionService.GetAnswers(item.Id);
                    if (tmp_answer.Count() > 0)
                    {
                        var answers = _mapper.Map<IEnumerable<AnswerModel>>(tmp_answer);
                        item.Answers = answers;
                    }
                }
                return View(map);
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return RedirectToAction("Index", "Home");      
        }

        [HttpGet] 
        public IActionResult AllComments()
        {
            try
            {
                IEnumerable<CommentDTO> comments = _commentService.GetAll();
                var map = _mapper.Map<IEnumerable<CommentViewModel>>(comments);
                return View(map);
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return RedirectToAction("Index", "Home");           
        }

        [HttpGet]
        public IActionResult AddComment()
        {
            return PartialView();
        }
        [HttpPost]
        public IActionResult AddComment(CommentViewModel model)
        {
            try
            {
                if (model.Text != null && model.Text.Length > 0)
                {
                    string name;
                    if (model.Name == null || model.Name.Length <= 0)
                        name = "Гость";
                    else
                        name = model.Name;
                    _commentService.Add(new CommentDTO
                    {
                        Name = name,
                        Text = model.Text,
                        DateTime = DateTime.Now
                    });
                }
                return RedirectToAction("AllComments", "Home");
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return RedirectToAction("Index", "Home");             
        }

        [HttpGet]
        public IActionResult DeleteComment(int id)
        {
            try
            {
                CommentDTO comment = _commentService.Get(id);
                if (comment != null)
                    _commentService.Delete(comment.Id);
                return RedirectToAction("AllComments", "Home");
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return RedirectToAction("Index", "Home");
          
        }

        [HttpGet]
        public IActionResult DeleteQestion(int id)
        {
            try
            {
                QuestionDTO question = _questionService.Get(id);
                if (question != null)
                    _questionService.Delete(question.Id);
                return RedirectToAction("AllQuestions", "Home");
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return RedirectToAction("Index", "Home");           
        }

        [HttpGet]
        public IActionResult AnswerTheQuestion(int id)
        {
            try
            {
                QuestionDTO question = _questionService.Get(id);
                if (question != null)
                {
                    AnswerModel answer = new AnswerModel { QuestionId = question.Id };
                    return PartialView(answer);
                }
                return RedirectToAction("AllQuestions", "Home");
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return RedirectToAction("AllQuestions", "Home");
        }

        [HttpPost]
        public IActionResult AnswerTheQuestion(AnswerModel model)
        {
            try
            {
                if (model.Text != null && model.Text.Length < 200 && model.Text.Length > 1)
                {
                    AnswerDTO answer = new AnswerDTO
                    {
                        Text = model.Text,
                        QuestionId = model.QuestionId
                    };
                    _questionService.AddAnswer(answer);
                }                               
                return RedirectToAction("AllQuestions", "Home");
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);                    
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult DeleteAnswer(int id)
        {
            try
            {
                AnswerDTO answer = _questionService.GetOneAnswer(id);
                if (answer != null)
                    _questionService.DeleteAnswer(id);
                return RedirectToAction("AllQuestions", "Home");
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
                return RedirectToAction("Index", "Home");
            }          
        }

        [HttpGet]
        public IActionResult KindLawyerHelp()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Price()
        {
            IEnumerable<PriceDTO> prices = _priceService.GetPrices();
            if (prices != null)
            {
                var model = _mapper.Map<IEnumerable<PriceViewModel>>(prices);
                return View(model);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
