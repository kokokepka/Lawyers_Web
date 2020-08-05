using Lawyers_Web_App.DAL.EF;
using Lawyers_Web_App.DAL.Entities;
using Lawyers_Web_App.DAL.Entities.Documents;
using Lawyers_Web_App.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lawyers_Web_App.DAL.Repositories.DocumentRep
{
    // логика взаимодействия с таблицей Users
    public class UserDocumentRepository : IRepository<UserDocument>
    {
        private LowyersContext db;
        public UserDocumentRepository(LowyersContext context)
        {
            db = context;
        }

        public void Create(UserDocument item)
        {
            db.UserDocuments.Add(item);
        }

        public void Delete(int id)
        {
            UserDocument doc = db.UserDocuments.FirstOrDefault(d => d.Id == id);
            if (doc != null)
                db.UserDocuments.Remove(doc);
        }

        public IEnumerable<UserDocument> Find(Func<UserDocument, bool> predicate)
        {
            return db.UserDocuments.Where(predicate).ToList();
        }

        public UserDocument Get(int? id)
        {
            return db.UserDocuments.FirstOrDefault(u => u.Id == id);
        }

        public IEnumerable<UserDocument> GetAll()
        {
            return db.UserDocuments;
        }

        public void Update(UserDocument item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
