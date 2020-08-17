using Lawyers_Web_App.WEB.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lawyers_Web_App.WEB.Models.Cases
{
    public class CreateNewCaseViewModel
    {
        public PartNewCaseModel PartNewCase { get; set; }
        public ParticipantViewModel Client { get; set; }
    }
}
