﻿using Lawyers_Web_App.DAL.Entities.Cases;
using Lawyers_Web_App.DAL.Entities.Cases.Additionally;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lawyers_Web_App.DAL.Entities.AccountEntities
{
    public class Client : BaseUser
    {
        public int? CaseId { get; set; }
        public Case Case { get; set; }
        public RoleInTheCase RoleInTheCase { get; set; }
    }
}
