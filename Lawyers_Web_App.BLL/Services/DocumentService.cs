using Lawyers_Web_App.BLL.DTO;
using Lawyers_Web_App.BLL.Infrastructure;
using Lawyers_Web_App.BLL.Interfaces;
using Lawyers_Web_App.BLL.Mappers;
using Lawyers_Web_App.DAL.Entities;
using Lawyers_Web_App.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lawyers_Web_App.BLL.Services
{
    public class DocumentService : IDocumentService
    {
        IUnitOfWork Database { get; set; }

        public DocumentService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public void Dispose()
        {
            Database.Dispose();
        }

        public IEnumerable<DocumentDTO> GetDocs()
        {
            var docs = Database.Documents.GetAll();
            var mapped = ObjectMapper.Mapper.Map<IEnumerable<DocumentDTO>>(docs);
            return mapped;
        }

        public DocumentDTO GetDoc(int? id)
        {
            if(id == null)
                throw new ValidationException("Id пользователя не найдено", "");
            var doc = Database.Documents.Get(id);
            if (doc == null)
                throw new ValidationException("Пользователь не найден", "");

            return new DocumentDTO { Id = doc.Id, Name = doc.Name, Path = doc.Path, Date = doc.Date, UserId = doc.UserId };
        }

        public void MakeDoc(DocumentDTO orderDto)
        {
            User user = Database.Users.Get(orderDto.UserId);
            //if(user == null)
            //    throw new ValidationException("Пользователь не найден", "");

            Document _newDo = new Document
            {
                Name = orderDto.Name,
                Path = orderDto.Path,
                Date = DateTime.Now.Date,
                User = user
            };

            Database.Documents.Create(_newDo);
            Database.Save();
        }
    }
}
