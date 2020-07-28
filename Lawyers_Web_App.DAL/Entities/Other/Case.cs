using Lawyers_Web_App.DAL.Entities.Documents;
using Lawyers_Web_App.DAL.Entities.UserEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lawyers_Web_App.DAL.Entities.Other
{
    public class Case
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsOpen { get; set; }
        public int? ClientId { get; set; }
        public ClientProfile Client { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }
        public virtual IList<ClientDocument> Documents { get; set; }
    }
}
