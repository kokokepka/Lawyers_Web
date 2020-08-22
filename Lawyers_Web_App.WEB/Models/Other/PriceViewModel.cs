using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lawyers_Web_App.WEB.Models.Other
{
    public class PriceViewModel
    {
        public int Id { get; set; }
        public string ForWhat { get; set; }
        public double StartSum { get; set; }
        public double FinishSum { get; set; }
    }
}
