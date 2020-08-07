using Lawyers_Web_App.DAL.Entities.Cases;
using Lawyers_Web_App.DAL.Entities.Cases.Additionally;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lawyers_Web_App.BLL.DTO
{
    public class ClientDTO
    {
        public int Id { get; set; }
        public int? CaseUserId { get; set; }
        public int? CaseId { get; set; }
        public int? Money { get; set; }
    }
}
