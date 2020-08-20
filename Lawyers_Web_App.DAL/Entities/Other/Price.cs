using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Lawyers_Web_App.DAL.Entities.Other
{
    public class Price
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(500)]
        public string ForWhat { get; set; }
        public double StartSum { get; set; }
        public double FinishSum { get; set; } 
    }
}
