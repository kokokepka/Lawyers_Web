using Lawyers_Web_App.BLL.DTO;
using Lawyers_Web_App.BLL.DTO.DocDTO;
using Lawyers_Web_App.BLL.DTO.OtherDTO;
using Lawyers_Web_App.BLL.Infrastructure;
using Lawyers_Web_App.BLL.Interfaces;
using Lawyers_Web_App.BLL.Interfaces.Documents;
using Lawyers_Web_App.BLL.Mappers;
using Lawyers_Web_App.DAL.Entities.Documents;
using Lawyers_Web_App.DAL.Entities.Other;
using Lawyers_Web_App.DAL.Entities.UserEntities;
using Lawyers_Web_App.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lawyers_Web_App.BLL.Services
{
    public class DocumentCaseService : IDocService<CaseDocDTO, CaseDTO>
    {
        IUnitOfWork _database { get; set; }

        public DocumentCaseService(IUnitOfWork uow)
        {
            _database = uow;
        }
        public void Dispose()
        {
            _database.Dispose();
        }

        public CaseDocDTO GetDoc(int? id)
        {
            if (id == null)
                throw new ValidationException("Id документа не найдено", "");
            var doc = _database.ClientDocuments.Get(id);
            if (doc == null)
                throw new ValidationException("Документ не найден", "");

            return new CaseDocDTO { Id = doc.Id, Name = doc.Name, Path = doc.Path, Date = doc.Date, CaseId = doc.CaseId };
        }

        public void MakeDoc(CaseDocDTO docDto)
        {
            Case _case = _database.Cases.Get(docDto.CaseId);
            if (_case == null)
                throw new ValidationException("Дело не найдено", "");
            ClientDocument _newDo = new ClientDocument
            {
                Name = docDto.Name,
                Path = docDto.Path,
                Date = docDto.Date,
                Case = _case
            };
            _database.ClientDocuments.Create(_newDo);
            _database.Save();
        }

        public IEnumerable<CaseDocDTO> GetAllDocs()
        {
            var docs = _database.UserDocuments.GetAll();
            var mapped = ObjectMapper.Mapper.Map<IEnumerable<CaseDocDTO>>(docs);
            return mapped;
        }

        public IEnumerable<CaseDocDTO> GetDocs(CaseDTO userDto)
        {
            var docs = _database.ClientDocuments.Find(d => d.CaseId == userDto.Id);
            var mapped = ObjectMapper.Mapper.Map<IEnumerable<CaseDocDTO>>(docs);
            return mapped;
        }
    }
}
