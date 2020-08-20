using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Lawyers_Web_App.DAL.Entities.Other
{
    public class Answer
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(1000)]
        public string Text { get; set; }
        public DateTime DateTime { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
