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
    public class PriceRepository : IRepository<Price>
    {
        private readonly LowyersContext db;
        public PriceRepository(LowyersContext lowyersContext)
        {
            db = lowyersContext;
        }
        public void Create(Price item)
        {
            db.Prices.Add(item);
        }

        public void Delete(int id)
        {
            Price price = db.Prices.FirstOrDefault(n => n.Id == id);
            if (price != null)
                db.Prices.Remove(price);
        }

        public IEnumerable<Price> Find(Func<Price, bool> predicate)
        {
            return db.Prices.Where(predicate).ToList();
        }

        public Price Get(int? id)
        {
            return db.Prices.FirstOrDefault(n => n.Id == id);
        }

        public IEnumerable<Price> GetAll()
        {
            return db.Prices;
        }

        public void Update(Price item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
