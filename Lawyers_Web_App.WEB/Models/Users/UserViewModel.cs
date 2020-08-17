using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lawyers_Web_App.WEB.Models.Users
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Phone { get; set; }
        public string HomePhone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        [Display(Name= "Загрузите фото")]
        [Required(ErrorMessage = "Загрузите фото")]
        public IFormFile Foto { get; set; }
        public byte[] Avatar { get; set; }
    }
}
