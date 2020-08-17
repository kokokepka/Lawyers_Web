using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lawyers_Web_App.WEB.Models.Cases
{
    public class PartNewCaseModel
    {
        public int KindOfCaseId { get; set; }
        public string KindOfCase { get; set; }
        public int UserId { get; set; }

        [Required(ErrorMessage ="Введите дату")]
        [Display(Name = "Введите дату")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Введите статью или категорию")]
        [Display(Name = "Введите статью или категорию")]
        public string ArticleOrCategory { get; set; }

        [Required(ErrorMessage = "Введите вердикт или решение")]
        [Display(Name = "Введите вердикт или решение")]
        public string VerdictOrDecision { get; set; }

        [Required(ErrorMessage = "Выберете инстанцию")]
        [Display(Name = "Выберете инстанцию")]
        public int InstanceId { get; set; }
        public IEnumerable<InstanceModel> InstanceModels { get; set; }
    }
}
