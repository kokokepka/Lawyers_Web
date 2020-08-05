using Lawyers_Web_App.BLL.DTO;
using Lawyers_Web_App.BLL.DTO.DocDTO;
using Lawyers_Web_App.BLL.Infrastructure;
using Lawyers_Web_App.BLL.Interfaces;
using Lawyers_Web_App.BLL.Interfaces.Documents;
using Lawyers_Web_App.BLL.Mappers;
using Lawyers_Web_App.DAL.Entities;
using Lawyers_Web_App.DAL.Entities.AccountEntities;
using Lawyers_Web_App.DAL.Entities.Documents;
using Lawyers_Web_App.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lawyers_Web_App.BLL.Services.Documents
{
    public class DocumentUserService : IDocService<UserDocDTO, UserDTO>
    {
        IUnitOfWork _database { get; set; }

        public DocumentUserService(IUnitOfWork uow)
        {
            _database = uow;
        }
        public void Dispose()
        {
            _database.Dispose();
        }

        public UserDocDTO GetDoc(int? id)
        {
            if(id == null)
                throw new ValidationException("Id документа не найдено", "");
            var doc = _database.UserDocuments.Get(id);
            if (doc == null)
                throw new ValidationException("Документ не найден", "");

            return new UserDocDTO { Id = doc.Id, Name = doc.Name, Path = doc.Path, Date = doc.Date, UserId = doc.UserId };
        }

        public void MakeDoc(UserDocDTO userDocDto)
        {
            User user = _database.Users.Get(userDocDto.UserId);
            if (user == null)
                throw new ValidationException("Пользователь не найден", "");
            UserDocument _newDo = new UserDocument
            {
                Name = userDocDto.Name,
                Path = userDocDto.Path,
                Date = DateTime.Now.Date,
                User = user
            };
            _database.UserDocuments.Create(_newDo);
            _database.Save();
        }

        public IEnumerable<UserDocDTO> GetAllDocs()
        {
            var docs = _database.UserDocuments.GetAll();
            var mapped = ObjectMapper.Mapper.Map<IEnumerable<UserDocDTO>>(docs);
            return mapped;
        }

        public IEnumerable<UserDocDTO> GetDocs(UserDTO userDto)
        {
            var docs = _database.UserDocuments.Find(d => d.UserId == userDto.Id);
            var mapped = ObjectMapper.Mapper.Map<IEnumerable<UserDocDTO>>(docs);
            return mapped;
        }
    }
}
