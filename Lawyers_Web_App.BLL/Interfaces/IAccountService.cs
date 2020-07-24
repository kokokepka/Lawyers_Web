using Lawyers_Web_App.BLL.DTO;
using Lawyers_Web_App.BLL.DTO.AccountDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lawyers_Web_App.BLL.Interfaces
{
    public interface IAccountService
    {
        AccountDTO Login(string login, string password);
        void Register(UserDTO userDTO);
        void Dispose();
    }
}
