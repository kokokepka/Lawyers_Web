using System;
using System.Collections.Generic;
using System.Text;

namespace Lawyers_Web_App.DAL.Entities.UserEntities
{
    public class Note
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public string Text { get; set; }
        public bool IsDone { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
