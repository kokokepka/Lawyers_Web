using Lawyers_Web_App.DAL.Entities.Documents;
using Lawyers_Web_App.DAL.Entities.Cases;
using Lawyers_Web_App.DAL.Entities.UserEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Lawyers_Web_App.DAL.Entities.Other;

namespace Lawyers_Web_App.DAL.Entities.AccountEntities
{
    // Модель пользователя, объект которой будет храниться в бд
    public class User : BaseUser
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public byte[] Avatar { get; set; }
        public int? RoleId { get; set; }
        public Role Role { get; set; }
        public virtual IList<UserDocument> Documents { get; set; }
        public virtual IList<Note> Notes { get; set; }
        public virtual IList<Case> Cases { get; set; }
        public int? ScheduleId { get; set; }
        public Schedule Schedule { get; set; }
    }
}
