using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lawyers_Web_App.WEB.Models.Other
{
    public class AnswerModel
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        [Required(ErrorMessage = "Задайте вопрос")]
        [MaxLength(200, ErrorMessage = "Длина не должна привышать 200 символов")]
        [Display(Name = "Ответ. ( Максимальо лаконичный и понятный ).")]
        public string Text { get; set; }
        public DateTime DateTime { get; set; }
    }
}
