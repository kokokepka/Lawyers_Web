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
    public class ScheduleRepository : IRepository<Schedule>
    {
        private readonly LowyersContext db;
        public ScheduleRepository(LowyersContext lowyersContext)
        {
            db = lowyersContext;
        }
        public void Create(Schedule item)
        {
            db.Schedules.Add(item);
        }

        public void Delete(int id)
        {
            Schedule schedule = db.Schedules.FirstOrDefault(n => n.Id == id);
            if (schedule != null)
                db.Schedules.Remove(schedule);
        }

        public IEnumerable<Schedule> Find(Func<Schedule, bool> predicate)
        {
            return db.Schedules.Where(predicate).ToList();
        }

        public Schedule Get(int? id)
        {
            return db.Schedules.FirstOrDefault(n => n.Id == id);
        }

        public IEnumerable<Schedule> GetAll()
        {
            return db.Schedules;
        }

        public void Update(Schedule item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
