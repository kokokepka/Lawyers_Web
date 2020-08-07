using Lawyers_Web_App.BLL.DTO;
using Lawyers_Web_App.BLL.DTO.OtherDTO;
using Lawyers_Web_App.BLL.Infrastructure;
using Lawyers_Web_App.BLL.Interfaces.Cases;
using Lawyers_Web_App.DAL.Entities.Cases;
using Lawyers_Web_App.BLL.Interfaces.Other;
using Lawyers_Web_App.BLL.Mappers;
using Lawyers_Web_App.DAL.Entities;
using Lawyers_Web_App.DAL.Entities.AccountEntities;
using Lawyers_Web_App.DAL.Entities.UserEntities;
using Lawyers_Web_App.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Lawyers_Web_App.BLL.DTO.CasesDTO;
using System.Linq;
using Lawyers_Web_App.DAL.Entities.Cases.Additionally;
using Microsoft.EntityFrameworkCore.Internal;
using Lawyers_Web_App.BLL.DTO.UsersDTO;

namespace Lawyers_Web_App.BLL.Services.Cases
{
    public class CaseService : ICaseService
    {
        IUnitOfWork _database { get; set; }

        public CaseService(IUnitOfWork database)
        {
            _database = database;
        }

        public void Dispose()
        {
            _database.Dispose();
        } 

        public void StartCase(CaseDTO caseDto, CaseUserDTO caseUser)
        {
            KindOfCase kindOfCase = _database.KindOfCases.Find(k => k.Name == caseDto.KindOfCase).FirstOrDefault();
            if (kindOfCase == null)
                throw new ValidationException(caseDto.KindOfCase + "- не найден", "");
            Instance instance = _database.Instances.Find(i => i.Name == caseDto.Instance).FirstOrDefault();
            if(instance == null)
                throw new ValidationException(caseDto.Instance + "- не найден", "");
            Category category = null;
            if (caseDto.Category != null)
            {
                category = _database.Categories.Find(c => c.Name == caseDto.Category).FirstOrDefault();
                if(category == null)
                    throw new ValidationException(caseDto.Category + "- не найден", "");
            }
            User user = _database.Users.Get(caseDto.UserId);
            if (user == null)
                throw new ValidationException("Пользователь не найден", "");
            //Case _case = _database.Cases.Find(c => c.Title == caseDto.Title).FirstOrDefault();
            //if (_case != null)
            //    throw new ValidationException("Дело с таким названием уже существует", "");
            Case _case = new Case
            {
                Title = caseDto.Title,
                KindOfCase = kindOfCase,
                Instance = instance,
                User = user,
                Date = caseDto.Date,
                Article = caseDto.Article,
                Verdict = caseDto.Verdict,
                Category = category
            };
            _database.Cases.Create(_case);
            _database.Save();
            CaseUser _caseUser = _database.CaseUsers.Find(c => c.Name == caseUser.Name && 
            c.Patronymic == caseUser.Patronymic &&
            c.Surname == caseUser.Surname && 
            c.DateOfBirth == caseUser.DateOfBirth && 
            c.Address == caseUser.Address).FirstOrDefault();
            if (_caseUser == null)
            {
                _caseUser = CreateCaseUser(caseUser, _case);
                _database.CaseUsers.Create(_caseUser);
                _database.Save();
            }
            Client client = _database.Clients.Find(c => c.CaseUserId == _caseUser.Id).FirstOrDefault();
            if (client == null)
            {
                client = new Client()
                {
                    CaseUser = _caseUser,
                    Case = _case,
                    Money = 0
                };
                _database.Clients.Create(client);
                _database.Save();
            }
            else
            {
                client.Case = _case;
                _database.Save();
            }
        }

        public CaseDTO GetCase(int? id)
        {
            if (id == null)
                throw new ValidationException("Id дело не найдено", "");
            Case _case = _database.Cases.Get(id);
            if (_case == null)
                throw new ValidationException("Дело не найдено", "");
            CaseDTO caseDto = CreateCaseDto(_case);
            if (caseDto == null)
                throw new ValidationException("Не удалось создать CaseDTO", "");
            return caseDto;
        }

        public IEnumerable<CaseDTO> GetClientCases(ClientDTO client)
        {
            Client _client = _database.Clients.Find(c => c.Id == client.Id).FirstOrDefault();

            if (_client == null)
                throw new ValidationException("Клиент не найден", "");
            var cases = _database.Cases.Find(c => c.Id == _client.CaseUserId);
            return GetListCases(cases);
        }

        public ClientDTO GetCaseClient(CaseDTO caseDTO)
        {
            var client = _database.Clients.Find(c => c.CaseId == caseDTO.Id);
            var mapped = ObjectMapper.Mapper.Map<ClientDTO>(client);
            return mapped;
        }

        public void AddParticipant(int caseId, CaseUserDTO caseUserDTO)
        {
            Case _case = _database.Cases.Get(caseId);
            if(_case == null)
                throw new ValidationException("Дело не найдено", "");
            CaseUser caseUser = _database.CaseUsers.Get(caseUserDTO.Id);
            if (caseUser != null)
            {
                _case.Participants.Add(caseUser);               
            }
            else
            {
                caseUser = CreateCaseUser(caseUserDTO, _case);
                _database.CaseUsers.Create(caseUser);
            }
            _database.Save();
        }

        public IEnumerable<CaseDTO> GetUserCases(int id, string kindofcase)
        {
            User user = _database.Users.Get(id);
            if (user == null)
                throw new ValidationException("Пользователь не найден", "");
            IEnumerable<Case> cases = _database.Cases.Find(c => c.UserId == user.Id && c.KindOfCase.Name == kindofcase);           
            return GetListCases(cases);
        }

        private IEnumerable<CaseDTO> GetListCases(IEnumerable<Case> cases)
        {
            IList<CaseDTO> caseDtos = new List<CaseDTO>();
            foreach (var item in cases)
            {
                caseDtos.Add(CreateCaseDto(item));
            }
            return caseDtos;
        }

        private CaseDTO CreateCaseDto(Case _case)
        {
            int? categoryId = null;
            string category = null;
            if (_case.Category != null)
            {
                categoryId = _case.CategoryId;
                category = _case.Category.Name;
            }
            return new CaseDTO
            {
                Id = _case.Id,
                Title = _case.Title,
                KindOfCaseId = _case.KindOfCaseId,
                KindOfCase = _case.KindOfCase.Name,
                InstanceId = _case.InstanceId,
                Instance = _case.Instance.Name,
                ClientId = _case.Client.Id,
                UserId = _case.User.Id,
                Date = _case.Date,
                Article = _case.Article,
                Verdict = _case.Verdict,
                CategoryId = categoryId,
                Category = category
            };
        }
        
        private CaseUser CreateCaseUser(CaseUserDTO caseUser, Case _case)
        {
            return new CaseUser
            {
                Name = caseUser.Name,
                Surname = caseUser.Surname,
                Patronymic = caseUser.Patronymic,
                DateOfBirth = caseUser.DateOfBirth,
                Phone = caseUser.Phone,
                Email = caseUser.Email,
                Address = caseUser.Address,
                Case = _case
            };
        }
    }
}
