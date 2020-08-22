using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lawyers_Web_App.WEB.Models.Other
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        [Display(Name= "Имя")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Введите ваш отзыв")]
        [Display(Name = "Ваш отзыв. Нам очень важно ваше мнение.")]
        public string Text { get; set; }
        public DateTime DateTime { get; set; }
    }
}
