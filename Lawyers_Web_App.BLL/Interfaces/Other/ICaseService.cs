using Lawyers_Web_App.BLL.DTO;
using Lawyers_Web_App.BLL.DTO.OtherDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lawyers_Web_App.BLL.Interfaces.Other
{
    public interface ICaseService
    {
        void StartCase(CaseDTO caseDto);
        CaseDTO GetCase(int? id);
        IEnumerable<CaseDTO> GetUserCases(UserDTO userDto);
        void Dispose();
    }
}
