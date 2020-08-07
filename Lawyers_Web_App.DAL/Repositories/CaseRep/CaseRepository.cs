using Lawyers_Web_App.DAL.EF;
using Lawyers_Web_App.DAL.Entities.Cases;
using Lawyers_Web_App.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lawyers_Web_App.DAL.Repositories.OtherRep
{
    public class CaseRepository : IRepository<Case>
    {
        private readonly LowyersContext db;

        public CaseRepository(LowyersContext db)
        {
            this.db = db;
        }

        public void Create(Case item)
        {
            db.Cases.Add(item);
        }

        public void Delete(int id)
        {
            Case _case = db.Cases.FirstOrDefault(n => n.Id == id);
            if (_case != null)
                db.Cases.Remove(_case);
        }

        public IEnumerable<Case> Find(Func<Case, bool> predicate)
        {
            return db.Cases.Include(c => c.Documents).Include(c => c.Client).Include(c => c.User).Include(c => c.Participants)
                .Include(c=>c.KindOfCase).Include(c=>c.Instance).Where(predicate).ToList();
        }

        public Case Get(int? id)
        {
            return db.Cases.Include(c => c.Documents).Include(c => c.Client).Include(c => c.Participants).Include(c => c.User)
            .Include(c => c.KindOfCase).Include(c => c.Instance).FirstOrDefault(n => n.Id == id);
        }

        public IEnumerable<Case> GetAll()
        {
            return db.Cases.Include(c => c.Documents).Include(c => c.Client).Include(c => c.User).Include(c => c.Participants)
                .Include(c => c.KindOfCase).Include(c => c.Instance);
        }

        public void Update(Case item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
