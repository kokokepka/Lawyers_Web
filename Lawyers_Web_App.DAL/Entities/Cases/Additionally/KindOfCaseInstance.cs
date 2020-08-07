using System;
using System.Collections.Generic;
using System.Text;

namespace Lawyers_Web_App.DAL.Entities.Cases.Additionally
{
    public class KindOfCaseInstance
    {
        public int KindOfCaseId { get; set; }
        public KindOfCase KindOfCase { get; set; }
        public int InstanceId { get; set; }
        public Instance Instance { get; set; }
    }
}
