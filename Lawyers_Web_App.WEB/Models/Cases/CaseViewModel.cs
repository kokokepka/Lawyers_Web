using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lawyers_Web_App.WEB.Models.Cases
{
    public class CaseViewModel
    {
        public int Id { get; set; }       
        public string Title { get; set; }
        public int KindOfCaseId { get; set; }
        public string KindOfCase { get; set; }
        public string Instance { get; set; }
        public int? ClientId { get; set; }
        public CaseUserViewModel Client { get; set; }
        public int? UserId { get; set; }
        public DateTime Date { get; set; }
        public string ArticleOrCategory { get; set; }
        public string VerdictOrDecision { get; set; }
        //public int CategoryId { get; set; }
        //public string Category { get; set; }
        public IEnumerable<CaseUserViewModel> Participants { get; set; }
        public IEnumerable<DocViewModel> Documents { get; set; }
    }
}
