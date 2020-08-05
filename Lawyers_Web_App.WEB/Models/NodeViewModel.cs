using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lawyers_Web_App.WEB.Models
{
    public class NodeViewModel
    {
        public string Date { get; set; }
        public string Time { get; set; }
        public string Text { get; set; }
        public bool IsDone { get; set; }
        public int UserId { get; set; }
    }
}
