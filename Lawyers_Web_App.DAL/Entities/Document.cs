using System;
using System.Collections.Generic;
using System.Text;

namespace Lawyers_Web_App.DAL.Entities
{
    // Модель документов, объект которой будет храниться в бд
    public class Document
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime Date { get; set; }
    }
}
