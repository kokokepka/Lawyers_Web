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
        UserDTO GetUser(string login);
        UserDTO GetUser(int id);
        void AddUserPhoto(UserDTO userDTO);
        void AddUserDocument(int userId, UserDocDTO userDoc);
        UserDocDTO GetUserDocument(int docId);
        IEnumerable<UserDocDTO> GetUserDocuments(int userId);
        IEnumerable<UserDTO> GetAllUsers();
        void DeleteDoc(int Id);
        void DeleteUser(int Id);
        void Dispose();
    }
}
