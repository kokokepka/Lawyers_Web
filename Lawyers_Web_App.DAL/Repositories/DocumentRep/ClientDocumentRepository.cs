using Lawyers_Web_App.DAL.EF;
using Lawyers_Web_App.DAL.Entities.Documents;
using Lawyers_Web_App.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lawyers_Web_App.DAL.Repositories.Document
{
    public class ClientDocumentRepository : IRepository<ClientDocument>
    {
        private LowyersContext db;
        public ClientDocumentRepository(LowyersContext context)
        {
            db = context;
        }

        public void Create(ClientDocument item)
        {
            db.ClientDocuments.Add(item);
        }

        public void Delete(int id)
        {
            ClientDocument doc = db.ClientDocuments.FirstOrDefault(c => c.Id == id);
            if (doc != null)
                db.ClientDocuments.Remove(doc);
        }

        public IEnumerable<ClientDocument> Find(Func<ClientDocument, bool> predicate)
        {
            return db.ClientDocuments.Where(predicate).ToList();
        }

        public ClientDocument Get(int? id)
        {
            return db.ClientDocuments.FirstOrDefault(u => u.Id == id);
        }

        public IEnumerable<ClientDocument> GetAll()
        {
            return db.ClientDocuments;
        }

        public void Update(ClientDocument item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
