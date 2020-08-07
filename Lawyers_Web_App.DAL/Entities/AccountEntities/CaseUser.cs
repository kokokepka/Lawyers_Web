using Lawyers_Web_App.DAL.Entities.Documents;
using Lawyers_Web_App.DAL.Entities.Cases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Lawyers_Web_App.DAL.Entities.Cases.Additionally;

namespace Lawyers_Web_App.DAL.Entities.AccountEntities
{
    public class CaseUser : BaseUser
    {
        public int? CaseId { get; set; }
        public Case Case { get; set; }
        public int RoleInTheCaseId { get; set; }
        public RoleInTheCase RoleInTheCase { get; set; }
        public Client Client { get; set; }
    }
}
