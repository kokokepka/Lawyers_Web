using Lawyers_Web_App.DAL.Entities.AccountEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Lawyers_Web_App.DAL.Entities.Other
{
    public class Schedule
    {
        [Key]
        public int Id { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public virtual IList<User> Users { get; set; }
    }
}
