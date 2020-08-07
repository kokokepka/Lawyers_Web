using Lawyers_Web_App.DAL.EF;
using Lawyers_Web_App.DAL.Entities.Cases.Additionally;
using Lawyers_Web_App.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lawyers_Web_App.DAL.Repositories.CaseRep
{
    public class RoleInTheCaseRepository : IRepository<RoleInTheCase>
    {
        LowyersContext _db;
        public RoleInTheCaseRepository(LowyersContext db)
        {
            _db = db;
        }

        public void Create(RoleInTheCase item)
        {
            _db.RoleInTheCases.Add(item);
        }

        public void Delete(int id)
        {
            RoleInTheCase roleInTheCase = _db.RoleInTheCases.FirstOrDefault(k => k.Id == id);
            if (roleInTheCase != null)
                _db.RoleInTheCases.Remove(roleInTheCase);
        }

        public IEnumerable<RoleInTheCase> Find(Func<RoleInTheCase, bool> predicate)
        {
            return _db.RoleInTheCases.Include(rc => rc.CaseUsers).Include(rc => rc.KindOfCase).Where(predicate).ToList();
        }

        public RoleInTheCase Get(int? id)
        {
            return _db.RoleInTheCases.Include(rc => rc.CaseUsers).Include(rc => rc.KindOfCase).FirstOrDefault(n => n.Id == id);
        }

        public IEnumerable<RoleInTheCase> GetAll()
        {
            return _db.RoleInTheCases.Include(rc => rc.CaseUsers).Include(rc => rc.KindOfCase);
        }
        public void Update(RoleInTheCase item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }
    }
}
