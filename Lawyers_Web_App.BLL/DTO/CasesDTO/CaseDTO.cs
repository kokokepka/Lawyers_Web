﻿using Lawyers_Web_App.DAL.Entities.AccountEntities;
using Lawyers_Web_App.DAL.Entities.Cases.Additionally;
using Lawyers_Web_App.DAL.Entities.Documents;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lawyers_Web_App.BLL.DTO.CasesDTO
{
    public class CaseDTO 
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public KindOfCase KindOfCase { get; set; }
        public Instance Instance { get; set; }
        public int? ClientId { get; set; }
        public Client Client { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }
        public DateTime Date { get; set; }
        public string Article { get; set; }
        public string Verdict { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public string Decision { get; set; }
        public virtual IList<CaseUser> Participants { get; set; }
        public virtual IList<ClientDocument> Documents { get; set; }
    }
}
