using Lawyers_Web_App.WEB.Models.Cases;
using Lawyers_Web_App.WEB.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lawyers_Web_App.WEB.Models
{
    public class SearchModel
    {
        IEnumerable<DocViewModel> Docs { get; set; }
        IEnumerable<UserViewModel> Users { get; set; }
        IEnumerable<CaseViewModel> Cases { get; set; }
        IEnumerable<CaseUserViewModel> CaseUsers { get; set; }
    }
}
