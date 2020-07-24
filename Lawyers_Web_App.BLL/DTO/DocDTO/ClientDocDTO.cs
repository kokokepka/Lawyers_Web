using System;
using System.Collections.Generic;
using System.Text;

namespace Lawyers_Web_App.BLL.DTO.DocDTO
{
    public class ClientDocDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public int? ClientId { get; set; }
        public DateTime Date { get; set; }
    }
}
