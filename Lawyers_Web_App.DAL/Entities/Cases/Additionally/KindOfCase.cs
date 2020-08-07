using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Lawyers_Web_App.DAL.Entities.Cases.Additionally
{
    public class KindOfCase
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual IList<RoleInTheCase> RoleInTheCases { get; set; }
        public virtual IList<Case> Cases { get; set; }
        public virtual IList<KindOfCaseInstance> KindOfCaseInstances { get; set; }
        public KindOfCase()
        {
            KindOfCaseInstances = new List<KindOfCaseInstance>(); 
        }
    }
}
