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
    public class QuestionRepository : IRepository<Question>
    {
        private readonly LowyersContext db;

        public QuestionRepository(LowyersContext lowyersContext)
        {
            db = lowyersContext;
        }
        public void Create(Question item)
        {
            db.Questions.Add(item);
        }

        public void Delete(int id)
        {
            Question question = db.Questions.FirstOrDefault(n => n.Id == id);
            if (question != null)
                db.Questions.Remove(question);
        }

        public IEnumerable<Question> Find(Func<Question, bool> predicate)
        {
            return db.Questions.Include(q => q.Answers).Where(predicate).ToList();
        }

        public Question Get(int? id)
        {
            return db.Questions.Include(q => q.Answers).FirstOrDefault(n => n.Id == id);
        }

        public IEnumerable<Question> GetAll()
        {
            return db.Questions.Include(q => q.Answers);
        }

        public void Update(Question item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
