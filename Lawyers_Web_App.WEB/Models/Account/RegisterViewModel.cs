using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lawyers_Web_App.WEB.Models.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="Укажите логин")]
        [Display(Name ="Логин")]
        [StringLength(10, MinimumLength =3,ErrorMessage ="Длина должна состовлять от 3 до 10 символов")]
        public string Login { get; set; }

        [Required(ErrorMessage ="Укажите пароль")]
        [Display(Name ="Пароль")]
        [StringLength(20, MinimumLength =8,ErrorMessage = "Длина должна состовлять от 8 до 20 символов")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage ="Подтвердите пароль")]
        [Display(Name ="Повторите пароль")]
        [Compare("Password",ErrorMessage ="Пароли не совпадают")]
        [DataType(DataType.Password)]
        public string CofirmPassword { get; set; }

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
