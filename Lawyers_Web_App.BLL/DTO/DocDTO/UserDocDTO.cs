using System;
using System.Collections.Generic;
using System.Text;

namespace Lawyers_Web_App.BLL.DTO
{
    public class UserDocDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public int? UserId { get; set; }
        public DateTime? Date { get; set; }
    }
}
