using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Lawyers_Web_App.DAL.Entities.Cases.Additionally
{
    public class Instance
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual IList<Case> Cases { get; set; }
        public virtual IList<KindOfCaseInstance> KindOfCaseInstances { get; set; }
        public Instance()
        {
            KindOfCaseInstances = new List<KindOfCaseInstance>();
        }
    }
}
