﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Lawyers_Web_App.DAL.Entities.Other
{
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Name { get; set; }
        public DateTime DateTime { get; set; }
        public virtual IEnumerable<Answer> Answers { get; set; }
    }
}
