﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Lawyers_Web_App.DAL.Entities.Other
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        [MaxLength(1000)]
        public string Text { get; set; }
        public DateTime DateTime { get; set; }
    }
}
