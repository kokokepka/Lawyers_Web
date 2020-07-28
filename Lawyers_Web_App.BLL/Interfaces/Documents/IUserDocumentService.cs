using Lawyers_Web_App.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lawyers_Web_App.BLL.Interfaces
{
    public interface IUserDocumentService
    {
        void MakeDoc(UserDocDTO userDocDto);
        UserDocDTO GetDoc(int? id);
        IEnumerable<UserDocDTO> GetAllDocs();
        IEnumerable<UserDocDTO> GetUserDocs(UserDTO userDto);
        void Dispose();
    }
}
