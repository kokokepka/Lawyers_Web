using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lawyers_Web_App.WEB.Models
{
    public class DocumentViewModels
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public int? UserId { get; set; }
        public DateTime Date { get; set; }
    }
}
