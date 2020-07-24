using Lawyers_Web_App.BLL.DTO;
using Lawyers_Web_App.BLL.DTO.DocDTO;
using Lawyers_Web_App.BLL.Infrastructure;
using Lawyers_Web_App.BLL.Interfaces;
using Lawyers_Web_App.BLL.Mappers;
using Lawyers_Web_App.DAL.Entities.Documents;
using Lawyers_Web_App.DAL.Entities.UserEntities;
using Lawyers_Web_App.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lawyers_Web_App.BLL.Services
{
    public class DocumentClientService : IClientDocumentService
    {
        IUnitOfWork _database { get; set; }

        public DocumentClientService(IUnitOfWork uow)
        {
            _database = uow;
        }
        public void Dispose()
        {
            _database.Dispose();
        }

        public ClientDocDTO GetDoc(int? id)
        {
            if (id == null)
                throw new ValidationException("Id пользователя не найдено", "");
            var doc = _database.UserDocuments.Get(id);
            if (doc == null)
                throw new ValidationException("Пользователь не найден", "");

            return new ClientDocDTO { Id = doc.Id, Name = doc.Name, Path = doc.Path, Date = doc.Date, ClientId = doc.UserId };
        }

        public void MakeDoc(ClientDocDTO clientDocDto)
        {
            ClientProfile client = _database.ClientProfiles.Get(clientDocDto.ClientId);
            if (client == null)
                throw new ValidationException("Пользователь не найден", "");
            ClientDocument _newDo = new ClientDocument
            {
                Name = clientDocDto.Name,
                Path = clientDocDto.Path,
                Date = DateTime.Now.Date,
                Client = client
            };
            _database.ClientDocuments.Create(_newDo);
            _database.Save();
        }

        public IEnumerable<ClientDocDTO> GetAllDocs()
        {
            var docs = _database.UserDocuments.GetAll();
            var mapped = ObjectMapper.Mapper.Map<IEnumerable<ClientDocDTO>>(docs);
            return mapped;
        }

        public IEnumerable<ClientDocDTO> GetUserDocs(ClientDTO clientDto)
        {
            var docs = _database.ClientDocuments.Find(d => d.ClientId == clientDto.Id);
            var mapped = ObjectMapper.Mapper.Map<IEnumerable<ClientDocDTO>>(docs);
            return mapped;
        }
    }
}
