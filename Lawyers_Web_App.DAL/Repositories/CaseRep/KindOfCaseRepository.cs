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
    public class KindOfCaseRepository : IRepository<KindOfCase>
    {
        LowyersContext _db;
        public KindOfCaseRepository(LowyersContext db)
        {
            _db = db;
        }

        public void Create(KindOfCase item)
        {
            _db.KindOfCases.Add(item);
        }

        public void Delete(int id)
        {
            KindOfCase kindOfCase = _db.KindOfCases.FirstOrDefault(k => k.Id == id);
            if (kindOfCase != null)
                _db.KindOfCases.Remove(kindOfCase);
        }

        public IEnumerable<KindOfCase> Find(Func<KindOfCase, bool> predicate)
        {
            return _db.KindOfCases.Include(k => k.RoleInTheCases).Include(k => k.Cases).Include(k => k.KindOfCaseInstances)
                .ThenInclude(ki => ki.Instance).Where(predicate).ToList();
        }

        public KindOfCase Get(int? id)
        {
            return _db.KindOfCases.Include(k => k.RoleInTheCases).Include(k => k.Cases).Include(k => k.KindOfCaseInstances)
                .ThenInclude(ki => ki.Instance).FirstOrDefault(n => n.Id == id);
        }

        public IEnumerable<KindOfCase> GetAll()
        {
            return _db.KindOfCases.Include(k => k.RoleInTheCases).Include(k => k.Cases).Include(k => k.KindOfCaseInstances)
                .ThenInclude(ki => ki.Instance);
        }

        public void Update(KindOfCase item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }
    }
}
