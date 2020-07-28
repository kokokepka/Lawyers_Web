using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Lawyers_Web_App.DAL.Entities.UserEntities
{
    public class BaseUser
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Phone { get; set; }
        public string? Email { get; set; }
    }
}
