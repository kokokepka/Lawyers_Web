using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lawyers_Web_App.WEB.Models.Other
{
    public class NoteViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите заголовок")]
        [Display(Name ="Заголовок")]        
        public string Title { get; set; }

        [Required(ErrorMessage ="Дата не выбрана")]
        [Display(Name ="Дата события")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Display(Name = "Время события")]
        [DataType(DataType.Time)]
        public DateTime Time { get; set; }

        [Display(Name ="Текст события(если необходимо)")]
        [StringLength(100)]
        public string Text { get; set; }
        public bool IsDone { get; set; }
        public int UserId { get; set; }
    }
}
