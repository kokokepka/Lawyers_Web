using System;
using System.Collections.Generic;
using System.Text;

namespace Lawyers_Web_App.BLL.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string Address { get; set; }
    }
}
