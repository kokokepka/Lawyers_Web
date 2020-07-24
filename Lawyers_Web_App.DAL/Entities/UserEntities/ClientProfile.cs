using Lawyers_Web_App.DAL.Entities.Documents;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Lawyers_Web_App.DAL.Entities.UserEntities
{
    public class ClientProfile : BaseUser
    {
        public virtual IList<ClientDocument> Documents { get; set; }

    }
}
