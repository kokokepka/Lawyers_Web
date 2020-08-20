using Lawyers_Web_App.DAL.EF;
using Lawyers_Web_App.DAL.Entities.UserEntities;
using Lawyers_Web_App.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lawyers_Web_App.DAL.Repositories.CaseRep
{
    public class NoteRepository : IRepository<Note>
    {
        private readonly LowyersContext db;
        public NoteRepository(LowyersContext lowyersContext)
        {
            db = lowyersContext;
        }

        public void Create(Note item)
        {
            db.Notes.Add(item);
        }

        public void Delete(int id)
        {
            Note note = db.Notes.FirstOrDefault(n => n.Id == id);
            if (note != null)
                db.Notes.Remove(note);
        }

        public IEnumerable<Note> Find(Func<Note, bool> predicate)
        {
            return db.Notes.Include(n => n.User).Where(predicate).ToList();
        }

        public Note Get(int? id)
        {
            return db.Notes.Include(n => n.User).FirstOrDefault(n => n.Id == id);
        }

        public IEnumerable<Note> GetAll()
        {
            return db.Notes.Include(n=>n.User);
        }

        public void Update(Note item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
