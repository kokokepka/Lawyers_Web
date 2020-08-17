using Lawyers_Web_App.DAL.Entities.AccountEntities;
using Lawyers_Web_App.DAL.Entities.Cases.Additionally;
using Lawyers_Web_App.DAL.Entities.Documents;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lawyers_Web_App.BLL.DTO.CasesDTO
{
    public class CaseDTO 
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int KindOfCaseId { get; set; }
        public string KindOfCase { get; set; }
        public int InstanceId { get; set; }
        public string Instance { get; set; }
        public int ClientId { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public string ArticleOrCategory { get; set; }
        public string VerdictOrDecision { get; set; }
        //public int? CategoryId { get; set; }
        //public string Category { get; set; }
        //public virtual IList<CaseUser> Participants { get; set; }
        //public virtual IList<ClientDocument> Documents { get; set; }
    }
}
