using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lawyers_Web_App.WEB.Models.Cases
{
    public class CaseUserViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Phone { get; set; }
        public string HomePhone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int? CaseId { get; set; }
        public int RoleInTheCaseId { get; set; }
        public string RoleInTheCase { get; set; }
    }
}
