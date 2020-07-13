using System;
using System.Collections.Generic;
using System.Text;

namespace Lawyers_Web_App.DAL.Entities
{
    // Модель пользователя, объект которой будет храниться в бд
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string Address { get; set; }
        public List<Document> Documents { get; set; }
    }
}
