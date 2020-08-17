using Lawyers_Web_App.DAL.EF;
using Lawyers_Web_App.DAL.Entities.Other;
using Lawyers_Web_App.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lawyers_Web_App.DAL.Repositories.OtherRep
{
    public class CommentRepository : IRepository<Comment>
    {
        private readonly LowyersContext db;

        public CommentRepository(LowyersContext lowyersContext)
        {
            db = lowyersContext;
        }
        public void Create(Comment item)
        {
            db.Comments.Add(item);
        }

        public void Delete(int id)
        {
            Comment comment = db.Comments.FirstOrDefault(n => n.Id == id);
            if (comment != null)
                db.Comments.Remove(comment);
        }

        public IEnumerable<Comment> Find(Func<Comment, bool> predicate)
        {
            return db.Comments.Where(predicate).ToList();
        }

        public Comment Get(int? id)
        {
            return db.Comments.FirstOrDefault(n => n.Id == id);
        }

        public IEnumerable<Comment> GetAll()
        {
            return db.Comments;
        }

        public void Update(Comment item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
