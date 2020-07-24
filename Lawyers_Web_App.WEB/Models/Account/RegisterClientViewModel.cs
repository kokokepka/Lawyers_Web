using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lawyers_Web_App.WEB.Models.Account
{
    public class RegisterClientViewModel
    {
        [Required(ErrorMessage = "Введите фамилию")]
        [Display(Name = "Фамилия")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Введите имя")]
        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите отчество")]
        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }

        [Required(ErrorMessage = "Введите дату рождения")]
        [Display(Name = "Дата рождения")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Введите номер телефона")]
        [Display(Name = "Телефон")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Введите адрес электронной почты")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
