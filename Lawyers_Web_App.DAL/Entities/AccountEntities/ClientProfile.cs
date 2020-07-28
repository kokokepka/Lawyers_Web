using Lawyers_Web_App.DAL.Entities.Documents;
using Lawyers_Web_App.DAL.Entities.Other;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Lawyers_Web_App.DAL.Entities.UserEntities
{
    public class ClientProfile : BaseUser
    {
        public Case Case { get; set; }
    }
}
