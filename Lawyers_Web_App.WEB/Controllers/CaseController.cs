using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Lawyers_Web_App.BLL.DTO;
using Lawyers_Web_App.BLL.DTO.CasesDTO;
using Lawyers_Web_App.BLL.DTO.DocDTO;
using Lawyers_Web_App.BLL.DTO.UsersDTO;
using Lawyers_Web_App.BLL.Infrastructure;
using Lawyers_Web_App.BLL.Interfaces;
using Lawyers_Web_App.BLL.Interfaces.Cases;
using Lawyers_Web_App.BLL.Interfaces.Documents;
using Lawyers_Web_App.DAL.Entities.AccountEntities;
using Lawyers_Web_App.WEB.Models;
using Lawyers_Web_App.WEB.Models.Cases;
using Lawyers_Web_App.WEB.Models.Users;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Lawyers_Web_App.WEB.Controllers
{
    public class CaseController : Controller
    {
        private readonly ICaseService _caseService;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        private IWebHostEnvironment _webHostEnvironment;

        public CaseController(ICaseService caseService, IAccountService accountService, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _caseService = caseService;
            _accountService = accountService;
            _webHostEnvironment = webHostEnvironment;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult MadeCase(int kind_case_id)
        {
            string login = User.Identity.Name;
            if (login == null)
            {
                return RedirectToAction("Index", "Home");
            }
            UserDTO user = _accountService.GetUser(login);
            //IEnumerable<InstanceDTO> instances = _caseService.GetInstances(kind_case);
            KindOfCaseDTO kindCase = _caseService.GetKind(kind_case_id);
            if (kindCase == null)
                return RedirectToAction("Index", "Home");
            var map_instances = _mapper.Map<IEnumerable<InstanceModel>>(kindCase.Instances);
            IEnumerable<RoleCaseDTO> roles = _caseService.GetRoleCase(kindCase.Id);
            var view_roles = _mapper.Map<IEnumerable<RoleCaseModel>>(roles);
            if (view_roles != null && map_instances != null)
            {
                CreateNewCaseViewModel model = new CreateNewCaseViewModel
                {
                    PartNewCase = new PartNewCaseModel
                    {
                        UserId = user.Id,
                        InstanceModels = map_instances,
                        KindOfCaseId = kindCase.Id,
                        KindOfCase = kindCase.Name
                    },
                    Client = new ParticipantViewModel
                    {
                        KindCaseId = kindCase.Id,
                        Roles = view_roles
                    }
                };
                return PartialView(model);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult MadeCase(CreateNewCaseViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    CaseDTO _case = new CaseDTO
                    {
                        InstanceId = model.PartNewCase.InstanceId,
                        VerdictOrDecision = model.PartNewCase.VerdictOrDecision,
                        ArticleOrCategory = model.PartNewCase.ArticleOrCategory,
                        Date = model.PartNewCase.Date,
                        KindOfCaseId = model.PartNewCase.KindOfCaseId,
                        UserId = model.PartNewCase.UserId
                    };
                    CaseUserDTO client = CreateCaseUser(model.Client);
                    _caseService.StartCase(_case, client);
                    return RedirectToAction("MyCases", "Case", new { id = model.PartNewCase.UserId, kind_case = model.PartNewCase.KindOfCase }); ;
                }
                catch (ValidationException ex)
                {
                    ModelState.AddModelError(ex.Property, ex.Message);
                }

            }
            return PartialView(model);
        }

        private AboutCaseModel MyCasesAdd(int id, string kindcase)
        {
            var cases = _caseService.GetUserCases(id, kindcase);
            var model = _mapper.Map<IEnumerable<CaseViewModel>>(cases);
            var kind_case = _caseService.FindKindByName(kindcase);
            var kind = _mapper.Map<KindOfCaseModel>(kind_case);
            if (kind == null || model == null)
                throw new ValidationException("Не найдено вид или дела", "");
            string[] tpmstr = kindcase.Split(" ");
            string title = kindcase;
            if (tpmstr.Count() == 2)
            {
                title = tpmstr[0].Replace("ое", "ые")+ " " + tpmstr[1].Replace("о", "а");
            }
            return new AboutCaseModel { Cases = model, KindOfCase = kind , Title = title };
        }

        [HttpGet]
        public IActionResult MyCases(int id, string kind_case)
        {
            try
            {
                AboutCaseModel model = MyCasesAdd(id, kind_case);
                return PartialView(model);
            }
            catch(ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult MyCriminalCases(int id)
        {
            try
            {
                AboutCaseModel model = MyCasesAdd(id, "Уголовное дело");
                return PartialView(model);
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult MyCivilCases(int id)
        {
            try
            {
                AboutCaseModel model = MyCasesAdd(id, "Гражданское дело");
                return PartialView(model);
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return RedirectToAction("Index", "Home");

        }

        [HttpGet]
        public IActionResult MyAdministrativeCases(int id)
        {
            try
            {
                AboutCaseModel model = MyCasesAdd(id, "Административное дело");
                return PartialView(model);
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult AboutCase(int id)
        {
            try
            {
                CaseDTO caseDTO = _caseService.GetCase(id);
                if (caseDTO != null)
                {
                    var model = _mapper.Map<CaseViewModel>(caseDTO);
                    var participant = _caseService.GetParticipant(caseDTO.Id);
                    var client = _caseService.GetClient(caseDTO.Id);
                    var documents = _caseService.GetCaseDocs(caseDTO.Id);
                    model.Client = _mapper.Map<CaseUserViewModel>(client);
                    model.Participants = _mapper.Map<IEnumerable<CaseUserViewModel>>(participant);
                    model.Documents = _mapper.Map<IEnumerable<DocViewModel>>(documents);
                    return PartialView(model);
                }

            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult AddDocumentCase(int caseid)
        {
            DocViewModel model = new DocViewModel { SomethingId = caseid };
            return PartialView(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddDocumentCase(DocViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.UploadedFile != null)
                {
                    // путь к папке Files
                    string path = @"\Files\Cases\" + model.UploadedFile.FileName;
                    // сохраняем файл в папку Files в каталоге wwwroot
                    using (var fileStream = new FileStream(_webHostEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await model.UploadedFile.CopyToAsync(fileStream);
                    }
                    _caseService.AddDocument(new CaseDocDTO
                    {
                        Name = model.UploadedFile.FileName,
                        Path = path,
                        Date = DateTime.Now.Date,
                        CaseId = model.SomethingId
                    });
                    return RedirectToAction("AboutCase","Case", new { id = model.SomethingId });
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Download(string file_name, string path)
        {
            // Путь к файлу
            string pp = _webHostEnvironment.ContentRootPath;
            string file_path = _webHostEnvironment.WebRootPath + path;
            // Тип файла - content-type
            string file_type = "application/" + Path.GetExtension(file_name);
            // Имя файла - необязательно
            return PhysicalFile(file_path, file_type, file_name);
        }

        [HttpGet]
        public IActionResult DeleteDocument(int id, int caseid, string path)
        {
            if (System.IO.File.Exists(_webHostEnvironment.WebRootPath + path))
            {
                System.IO.File.Delete(_webHostEnvironment.WebRootPath + path);
            }
            _caseService.Delete(id);
            return RedirectToAction("AboutCase", "Case", new { id = caseid });
        }

        [HttpGet]
        public IActionResult AddParticipantInCase(int caseid, int kindcase)
        {
            try
            {
                IEnumerable<RoleCaseDTO> roles = _caseService.GetRoleCase(kindcase);
                var view_roles = _mapper.Map<IEnumerable<RoleCaseModel>>(roles);
                if (roles != null)
                {
                    ParticipantViewModel model = new ParticipantViewModel
                    {
                        CaseId = caseid,
                        KindCaseId = kindcase,
                        Roles = view_roles
                    };
                    return PartialView(model);
                }
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult AddParticipantInCase(ParticipantViewModel model)
        {
            if (ModelState.IsValid)
            {
                CaseUserDTO _newCaseUser = CreateCaseUser(model);
                _caseService.AddParticipant(model.CaseId, _newCaseUser);
                return RedirectToAction("AboutCase", "Case", new { id = model.CaseId });
            }
            return PartialView(model);
        }

        private CaseUserDTO CreateCaseUser(ParticipantViewModel model)
        {
            {
                return new CaseUserDTO
                {
                    Name = model.Name,
                    Surname = model.Surname,
                    Patronymic = model.Patronymic,
                    DateOfBirth = model.DateOfBirth,
                    Address = model.Address,
                    Phone = model.Phone,
                    HomePhone = model.HomePhone,
                    Email = model.Email,
                    CaseId = model.CaseId,
                    RoleInTheCaseId = model.RoleId
                };
            }
        }
    }
}
