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
    public class InstanceRepository : IRepository<Instance>
    {
        LowyersContext _db;
        public InstanceRepository(LowyersContext db)
        {
            _db = db;
        }

        public void Create(Instance item)
        {
            _db.Instances.Add(item);
        }

        public void Delete(int id)
        {
            Instance _instance = _db.Instances.FirstOrDefault(n => n.Id == id);
            if (_instance != null)
                _db.Instances.Remove(_instance);
        }

        public IEnumerable<Instance> Find(Func<Instance, bool> predicate)
        {
            return _db.Instances.Include(i => i.Cases).Include(i => i.KindOfCaseInstances).ThenInclude(ki => ki.KindOfCase).Where(predicate).ToList();
        }

        public Instance Get(int? id)
        {
            return _db.Instances.Include(i => i.Cases).Include(i => i.KindOfCaseInstances).ThenInclude(ki => ki.KindOfCase).FirstOrDefault(n => n.Id == id);
        }

        public IEnumerable<Instance> GetAll()
        {
            return _db.Instances.Include(i => i.Cases).Include(i => i.KindOfCaseInstances).ThenInclude(ki => ki.KindOfCase);
        }

        public void Update(Instance item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }
    }
}
