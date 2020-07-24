using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Lawyers_Web_App.WEB.Models.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Укажите логин")]
        [Display(Name ="Логин")]
        public string Login { get; set; }
        [Required(ErrorMessage ="Укажите пароль")]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
