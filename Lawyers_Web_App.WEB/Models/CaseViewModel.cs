using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lawyers_Web_App.WEB.Models
{
    public class CaseViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string KindOfCase { get; set; }
        public string Instance { get; set; }
        public int? ClientId { get; set; }
        public string Client { get; set; }
        public int? UserId { get; set; }
        public string User { get; set; }
        public DateTime Date { get; set; }
        public string Article { get; set; }
        public string Verdict { get; set; }
        public int CategoryId { get; set; }
        public string Category { get; set; }
        public string Decision { get; set; }
    }
}
