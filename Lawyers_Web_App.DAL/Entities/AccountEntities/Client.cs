using Lawyers_Web_App.DAL.Entities.Cases;
using Lawyers_Web_App.DAL.Entities.Cases.Additionally;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Lawyers_Web_App.DAL.Entities.AccountEntities
{
    public class Client
    {
        [Key]
        public int Id { get; set; }
        public int? CaseUserId { get; set; }
        public CaseUser CaseUser { get; set; }
        public int? CaseId { get; set; }
        public Case Case { get; set; }
        public int? Money { get; set; }
    }
}
