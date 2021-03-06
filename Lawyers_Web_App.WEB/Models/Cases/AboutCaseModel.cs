﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lawyers_Web_App.WEB.Models.Cases
{
    public class AboutCaseModel
    {
        public IEnumerable<CaseViewModel> Cases { get; set; }
        public KindOfCaseModel KindOfCase { get; set; }
        public string Title { get; set; }
    }
}
