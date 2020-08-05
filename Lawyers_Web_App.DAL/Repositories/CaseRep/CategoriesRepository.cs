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
    public class CategoriesRepository : IRepository<Category>
    {
        private LowyersContext db;

        public CategoriesRepository(LowyersContext db)
        {
            this.db = db;
        }

        public void Create(Category item)
        {
            db.Categories.Add(item);
        }

        public void Delete(int id)
        {
            Category category = db.Categories.FirstOrDefault(c => c.Id == id);
            if (category != null)
                db.Categories.Remove(category);
        }

        public IEnumerable<Category> Find(Func<Category, bool> predicate)
        {
            return db.Categories.Include(c => c.Сases)
               .Where(predicate).ToList();
        }

        public Category Get(int? id)
        {
            return db.Categories.Include(c => c.Сases).FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Category> GetAll()
        {
            return db.Categories;
        }

        public void Update(Category item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
