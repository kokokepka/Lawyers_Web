using System;
using System.Collections.Generic;
using System.Text;

namespace Lawyers_Web_App.BLL.DTO
{
    public class UserDTO:BaseUserDTO
    {
        public string Login { get; set; }
        public string Password { get; set; }

    }
}
