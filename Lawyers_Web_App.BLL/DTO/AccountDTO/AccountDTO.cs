using System;
using System.Collections.Generic;
using System.Text;

namespace Lawyers_Web_App.BLL.DTO.AccountDTO
{
    public class AccountDTO
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public int? RoleId { get; set; }
        public string Role { get; set; }
    }
}
