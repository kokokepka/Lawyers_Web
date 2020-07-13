using Lawyers_Web_App.DAL.EF;
using Lawyers_Web_App.DAL.Entities;
using Lawyers_Web_App.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lawyers_Web_App.DAL.Repositories
{
    // логика взаимодействия с таблицей Users
    class DocumentRepository : IRepository<Document>
    {
        private LowyersContext db;
        public DocumentRepository(LowyersContext context)
        {
            db = context;
        }

        public void Create(Document item)
        {
            db.Documents.Add(item);
        }

        public void Delete(int id)
        {
            Document doc = db.Documents.FirstOrDefault(d => d.Id == id);
            if (doc != null)
                db.Documents.Remove(doc);
        }

        public IEnumerable<Document> Find(Func<Document, bool> predicate)
        {
            return db.Documents.Where(predicate).ToList();
        }

        public Document Get(int id)
        {
            return db.Documents.FirstOrDefault(u => u.Id == id);
        }

        public IEnumerable<Document> GetAll()
        {
            return db.Documents;
        }

        public void Update(Document item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
