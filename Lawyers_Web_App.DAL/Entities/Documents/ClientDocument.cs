using Lawyers_Web_App.DAL.Entities.UserEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lawyers_Web_App.DAL.Entities.Documents
{
    public class ClientDocument:BaseDoc
    {
        public int? ClientId { get; set; }
        public ClientProfile Client { get; set; }
    }
}
