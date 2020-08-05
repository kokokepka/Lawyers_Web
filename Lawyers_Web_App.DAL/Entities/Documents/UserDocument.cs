using Lawyers_Web_App.DAL.Entities.AccountEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lawyers_Web_App.DAL.Entities.Documents
{
    public class UserDocument:BaseDoc
    {
        public int? UserId { get; set; }
        public User User { get; set; }
    }
}
