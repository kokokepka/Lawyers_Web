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
    // логика взаимодействия с таблицей Users
    public class UserRepository : IRepository<User>
    {
        private LowyersContext db;
        public UserRepository(LowyersContext context)
        {
            db = context;
        }

        public void Create(User item)
        {
            db.Users.Add(item);
        }

        public void Delete(int id)
        {
            User user = db.Users.FirstOrDefault(u => u.Id == id);
            if (user != null)
                db.Users.Remove(user);
        }

        public IEnumerable<User> Find(Func<User, bool> predicate)
        {
            return db.Users.Include(u => u.Role).Include(u => u.Documents).Include(u => u.Cases).Where(predicate).ToList();
        }

        public User Get(int? id)
        {
            return db.Users.Include(u => u.Role).Include(u => u.Documents).Include(u => u.Cases).FirstOrDefault(u => u.Id == id);
        }

        public IEnumerable<User> GetAll()
        {
            return db.Users.Include(u => u.Role).Include(u => u.Documents).Include(u => u.Cases);
        }

        public void Update(User item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
