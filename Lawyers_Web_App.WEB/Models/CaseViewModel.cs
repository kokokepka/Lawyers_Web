using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lawyers_Web_App.WEB.Models
{
    public class CaseViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsOpen { get; set; }
        public int? ClientId { get; set; }
        public int? UserId { get; set; }
        public string TypeOfCase { get; set; }
    }
}
