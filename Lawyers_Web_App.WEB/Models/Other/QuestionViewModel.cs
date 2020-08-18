using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lawyers_Web_App.WEB.Models.Other
{
    public class QuestionViewModel
    {
        public int Id { get; set; }
        [Required (ErrorMessage ="Задайте вопрос")]
        [MaxLength(150,ErrorMessage ="Длина не должна привышать 150 символов")]
        [Display(Name ="Вопрос. ( Не следует расписывать всю проблему. Опишите суть вопроса и мы обязательно с вами свяжемся и проконсультируем).")]
        public string Text { get; set; }

        [Required(ErrorMessage = "Укажите ваше имя")]
        [Display(Name = "Укажите ваше имя")]
        [MaxLength(50, ErrorMessage = "Длина не должна привышать 50 символов")]
        public string Name { get; set; }
        public DateTime DateTime { get; set; }
        public IEnumerable<AnswerModel> Answers { get; set; }
    }
}
