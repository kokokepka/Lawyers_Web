using Lawyers_Web_App.DAL.EF;
using Lawyers_Web_App.DAL.Entities.Other;
using Lawyers_Web_App.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lawyers_Web_App.DAL.Repositories.OtherRep
{
    public class AnswerRepository : IRepository<Answer>
    {
        private readonly LowyersContext db;
        public AnswerRepository(LowyersContext lowyersContext)
        {
            db = lowyersContext;
        }
        public void Create(Answer item)
        {
            db.Answers.Add(item);
        }

        public void Delete(int id)
        {
            Answer answer = db.Answers.FirstOrDefault(a => a.Id == id);
            if (answer != null)
                db.Answers.Remove(answer);
        }

        public IEnumerable<Answer> Find(Func<Answer, bool> predicate)
        {
            return db.Answers.Include(a=>a.Question).Where(predicate).ToList();
        }

        public Answer Get(int? id)
        {
            return db.Answers.Include(a => a.Question).FirstOrDefault(n => n.Id == id);
        }

        public IEnumerable<Answer> GetAll()
        {
            return db.Answers.Include(a => a.Question);
        }

        public void Update(Answer item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
