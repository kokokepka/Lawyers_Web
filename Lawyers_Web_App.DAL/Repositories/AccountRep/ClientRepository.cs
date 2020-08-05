using Lawyers_Web_App.DAL.EF;
using Lawyers_Web_App.DAL.Entities.AccountEntities;
using Lawyers_Web_App.DAL.Entities.UserEntities;
using Lawyers_Web_App.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lawyers_Web_App.DAL.Repositories.AccountRep
{
    public class ClientRepository : IRepository<Client>
    {
        private readonly LowyersContext db;
        public ClientRepository(LowyersContext context)
        {
            db = context;
        }
        public void Create(Client item)
        {
            db.Clients.Add(item);
        }

        public void Delete(int id)
        {
            Client client = db.Clients.FirstOrDefault(c => c.Id == id);
            if (client != null)
                db.Clients.Remove(client);
        }

        public IEnumerable<Client> Find(Func<Client, bool> predicate)
        {
            return db.Clients.Include(c => c.Case)
                .Where(predicate).ToList();
        }

        public Client Get(int? id)
        {
            return db.Clients.Include(c => c.Case)
                .FirstOrDefault(u => u.Id == id);
        }

        public IEnumerable<Client> GetAll()
        {
            return db.Clients.Include(c => c.Case);
        }

        public void Update(Client item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
