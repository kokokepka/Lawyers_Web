using Lawyers_Web_App.DAL.Entities.Cases;
using Lawyers_Web_App.DAL.Entities.UserEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lawyers_Web_App.DAL.Entities.Documents
{
    public class CaseDocument:BaseDoc
    {
        public int? CaseId { get; set; }
        public Case Case { get; set; }
    }
}
