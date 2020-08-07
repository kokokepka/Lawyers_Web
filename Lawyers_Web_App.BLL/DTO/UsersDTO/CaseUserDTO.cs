using System;
using System.Collections.Generic;
using System.Text;

namespace Lawyers_Web_App.BLL.DTO.UsersDTO
{
    public class CaseUserDTO:BaseUserDTO
    {
        public int? CaseId { get; set; }
        public int RoleInTheCaseId { get; set; }
        public string RoleInTheCase { get; set; }
    }
}
