using Lawyers_Web_App.DAL.EF;
using Lawyers_Web_App.DAL.Entities.AccountEntities;
using Lawyers_Web_App.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lawyers_Web_App.DAL.Repositories.AccountRep
{
    public class CaseUserRepository : IRepository<CaseUser>
    {
        private readonly LowyersContext db;
        public CaseUserRepository(LowyersContext context)
        {
            db = context;
        }
        public void Create(CaseUser item)
        {
            db.CaseUsers.Add(item);
        }

        public void Delete(int id)
        {
            CaseUser user = db.CaseUsers.FirstOrDefault(c => c.Id == id);
            if (user != null)
                db.CaseUsers.Remove(user);
        }

        public IEnumerable<CaseUser> Find(Func<CaseUser, bool> predicate)
        {
            return db.CaseUsers.Include(c => c.Case).Include(c=>c.RoleInTheCase)
                .Where(predicate).ToList();
        }

        public CaseUser Get(int? id)
        {
            return db.CaseUsers.Include(c => c.Case).Include(c => c.RoleInTheCase)
                .FirstOrDefault(u => u.Id == id);
        }

        public IEnumerable<CaseUser> GetAll()
        {
            return db.CaseUsers.Include(c => c.Case).Include(c => c.RoleInTheCase);
        }

        public void Update(CaseUser item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
