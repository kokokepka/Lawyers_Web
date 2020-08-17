using Lawyers_Web_App.DAL.EF;
using Lawyers_Web_App.DAL.Entities.Documents;
using Lawyers_Web_App.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lawyers_Web_App.DAL.Repositories.DocumentRep
{
    public class ClientDocumentRepository : IRepository<CaseDocument>
    {
        private LowyersContext db;
        public ClientDocumentRepository(LowyersContext context)
        {
            db = context;
        }

        public void Create(CaseDocument item)
        {
            db.ClientDocuments.Add(item);
        }

        public void Delete(int id)
        {
            CaseDocument doc = db.ClientDocuments.FirstOrDefault(c => c.Id == id);
            if (doc != null)
                db.ClientDocuments.Remove(doc);
        }

        public IEnumerable<CaseDocument> Find(Func<CaseDocument, bool> predicate)
        {
            return db.ClientDocuments.Where(predicate).ToList();
        }

        public CaseDocument Get(int? id)
        {
            return db.ClientDocuments.FirstOrDefault(u => u.Id == id);
        }

        public IEnumerable<CaseDocument> GetAll()
        {
            return db.ClientDocuments;
        }

        public void Update(CaseDocument item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
