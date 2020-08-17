using Lawyers_Web_App.DAL.Entities.AccountEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Lawyers_Web_App.DAL.Entities.UserEntities
{
    public class Note
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public string Text { get; set; }
        public bool IsDone { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
