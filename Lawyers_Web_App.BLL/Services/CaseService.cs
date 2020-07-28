using Lawyers_Web_App.BLL.DTO;
using Lawyers_Web_App.BLL.DTO.OtherDTO;
using Lawyers_Web_App.BLL.Infrastructure;
using Lawyers_Web_App.BLL.Interfaces.Other;
using Lawyers_Web_App.BLL.Mappers;
using Lawyers_Web_App.DAL.Entities;
using Lawyers_Web_App.DAL.Entities.Other;
using Lawyers_Web_App.DAL.Entities.UserEntities;
using Lawyers_Web_App.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lawyers_Web_App.BLL.Services
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
        public CaseDTO GetCase(int? id)
        {
            if (id == null)
                throw new ValidationException("Id дело не найдено", "");
            var note = _database.Notes.Get(id);
            if (note == null)
                throw new ValidationException("Дело не найдено", "");

            return new CaseDTO { Id = note.Id,  };
        }

        public IEnumerable<CaseDTO> GetUserCases(UserDTO userDto)
        {
            var cases = _database.Cases.Find(c => c.UserId == userDto.Id);
            var mapped = ObjectMapper.Mapper.Map<IEnumerable<CaseDTO>>(cases);
            return mapped;
        }

        public void StartCase(CaseDTO caseDto)
        {
            User user = _database.Users.Get(caseDto.UserId);
            if (user == null)
                throw new ValidationException("Пользователь не найден", "");
            ClientProfile client = _database.ClientProfiles.Get(caseDto.ClientId);
            if (client == null)
                throw new ValidationException("Клиент не найден", "");
            Case _newCase = new Case
            {
                Title = caseDto.Title,
                IsOpen = true,
                Client = client,
                User = user
            };
            _database.Cases.Create(_newCase);
            _database.Save();
        }
    }
}
