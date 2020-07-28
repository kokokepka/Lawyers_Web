using Lawyers_Web_App.DAL.Entities.Documents;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lawyers_Web_App.BLL.DTO.OtherDTO
{
    public class CaseDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsOpen { get; set; }
        public int? ClientId { get; set; }
        public int? UserId { get; set; }
        public virtual IList<ClientDocument> Documents { get; set; }
    }
}
