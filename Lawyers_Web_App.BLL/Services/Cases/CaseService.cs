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

        public void StartCase(CaseDTO caseDto)
        {
            User user = _database.Users.Get(caseDto.UserId);
            if (user == null)
                throw new ValidationException("Пользователь не найден", "");
            Client client = _database.Clients.Get(caseDto.ClientId);
            if (client == null)
                throw new ValidationException("Клиент не найден", "");
            Case _newCase = new Case
            {
                Title = caseDto.Title,
                Client = client,
                User = user,
                Date = caseDto.Date,
                Article = caseDto.Article,
                Verdict = caseDto.Verdict,
                Category = caseDto.Category,
                Decision = caseDto.Decision,
                KindOfCase = caseDto.KindOfCase,
                Instance = caseDto.Instance,                
            };
            _database.Cases.Create(_newCase);
            _database.Save();
        }

        public CaseDTO GetCase(int? id)
        {
            if (id == null)
                throw new ValidationException("Id дело не найдено", "");
            var _case = _database.Cases.Get(id);
            if (_case == null)
                throw new ValidationException("Дело не найдено", "");

            var mapped = ObjectMapper.Mapper.Map<CaseDTO>(_case);
            return mapped;
        }

        public IEnumerable<CaseDTO> GetUserCases(int id)
        {
            var cases = _database.Cases.Find(c => c.UserId == id);
            var mapped = ObjectMapper.Mapper.Map<IEnumerable<CaseDTO>>(cases);
            return mapped;
        }

        public IEnumerable<CaseDTO> GetClientCases(ClientDTO client)
        {
            Client _client = _database.Clients.Find(c => c.Id == client.Id).FirstOrDefault();
            if (_client == null)
                throw new ValidationException("Клиент не найден", "");
            var cases = _database.Cases.Find(c => c.UserId == _client.Id);
            var mapped = ObjectMapper.Mapper.Map<IEnumerable<CaseDTO>>(cases);
            return mapped;
        }

        public ClientDTO GetCaseClient(CaseDTO caseDTO)
        {
            var client = _database.Clients.Get(caseDTO.ClientId);
            var mapped = ObjectMapper.Mapper.Map<ClientDTO>(client);
            return mapped;
        }

        public void AddParticipant(ClientDTO clientDTO)
        {
            CaseUser caseUser = _database.CaseUsers.Get(clientDTO.Id);
        }
    }
}
