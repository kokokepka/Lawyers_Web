using Lawyers_Web_App.DAL.Entities.AccountEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Lawyers_Web_App.DAL.Entities.Cases.Additionally
{
    public class RoleInTheCase
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int KindOfCaseId { get; set; }
        public KindOfCase KindOfCase { get; set; }
        public virtual IList<CaseUser> CaseUsers { get; set; }
    }
}
