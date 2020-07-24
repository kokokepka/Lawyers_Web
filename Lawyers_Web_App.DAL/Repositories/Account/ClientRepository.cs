using Lawyers_Web_App.DAL.EF;
using Lawyers_Web_App.DAL.Entities.UserEntities;
using Lawyers_Web_App.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lawyers_Web_App.DAL.Repositories
{
    public class ClientRepository : IRepository<ClientProfile>
    {
        private readonly LowyersContext db;
        public ClientRepository(LowyersContext context)
        {
            db = context;
        }
        public void Create(ClientProfile item)
        {
            db.ClientProfiles.Add(item);
        }

        public void Delete(int id)
        {
            ClientProfile client = db.ClientProfiles.FirstOrDefault(c => c.Id == id);
            if (client != null)
                db.ClientProfiles.Remove(client);
        }

        public IEnumerable<ClientProfile> Find(Func<ClientProfile, bool> predicate)
        {
            return db.ClientProfiles.Include(c=>c.Documents).Where(predicate).ToList();
        }

        public ClientProfile Get(int? id)
        {
            return db.ClientProfiles.Include(u => u.Documents).FirstOrDefault(u => u.Id == id);
        }

        public IEnumerable<ClientProfile> GetAll()
        {
            return db.ClientProfiles.Include(u => u.Documents);
        }

        public void Update(ClientProfile item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
