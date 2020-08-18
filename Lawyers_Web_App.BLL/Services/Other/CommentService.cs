using Lawyers_Web_App.BLL.DTO.OtherDTO;
using Lawyers_Web_App.BLL.Infrastructure;
using Lawyers_Web_App.BLL.Interfaces.Other;
using Lawyers_Web_App.BLL.Mappers;
using Lawyers_Web_App.DAL.Entities.Other;
using Lawyers_Web_App.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lawyers_Web_App.BLL.Services.Other
{
    public class CommentService : ICommentService<CommentDTO>
    {
        IUnitOfWork _database { get; set; }
        public CommentService(IUnitOfWork database)
        {
            _database = database;
        }
        public void Add(CommentDTO item)
        {
            if (item == null)
                throw new ValidationException("Модель ровна null", "");
            Comment comment = new Comment
            {
                Text = item.Text,
                DateTime = DateTime.Now,
                Name = item.Name
            };
            _database.Comments.Create(comment);
            _database.Save();
        }

        public void Delete(int id)
        {
            _database.Comments.Delete(id);
            _database.Save();
        }

        public void Dispose()
        {
            _database.Dispose(); 
        }

        public CommentDTO Get(int id)
        {
            Comment comment = _database.Comments.Get(id);
            if (comment == null)
                throw new ValidationException("Кооментарий не найден", "");
            var map = ObjectMapper.Mapper.Map<CommentDTO>(comment);
            return map;
        }

        public IEnumerable<CommentDTO> GetAll()
        {
            IEnumerable<Comment> comments = _database.Comments.GetAll();
            var mapped = ObjectMapper.Mapper.Map<IEnumerable<CommentDTO>>(comments);
            return mapped;
        }
    }
}
