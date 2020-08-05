using Lawyers_Web_App.DAL.EF;
using Lawyers_Web_App.DAL.Entities;
using Lawyers_Web_App.DAL.Entities.AccountEntities;
using Lawyers_Web_App.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lawyers_Web_App.DAL.Repositories.AccountRep
{
    public class RoleRepository : IRepository<Role>
    {
        private LowyersContext db;
        public RoleRepository(LowyersContext lowyersContext)
        {
            db = lowyersContext;
        }
        public void Create(Role item)
        {
            db.Roles.Add(item);
        }

        public void Delete(int id)
        {
            Role role = db.Roles.FirstOrDefault(r => r.Id == id);
            if (role != null)
                db.Roles.Remove(role);
        }

        public IEnumerable<Role> Find(Func<Role, bool> predicate)
        {
            return db.Roles.Where(predicate).ToList();
        }

        public Role Get(int? id)
        {
            return db.Roles.FirstOrDefault(u => u.Id == id);
        }

        public IEnumerable<Role> GetAll()
        {
            return db.Roles.ToList();
        }

        public void Update(Role item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
