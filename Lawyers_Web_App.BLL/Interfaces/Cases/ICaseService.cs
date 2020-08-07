using Lawyers_Web_App.BLL.DTO;
using Lawyers_Web_App.BLL.DTO.CasesDTO;
using Lawyers_Web_App.BLL.DTO.OtherDTO;
using Lawyers_Web_App.BLL.DTO.UsersDTO;
using Lawyers_Web_App.DAL.Entities.Cases.Additionally;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lawyers_Web_App.BLL.Interfaces.Cases
{
    public interface ICaseService
    {
        void StartCase(CaseDTO caseDto, CaseUserDTO caseUser);
        void AddParticipant(int caseId, CaseUserDTO caseUserDTO);
        CaseDTO GetCase(int? id);
        IEnumerable<CaseDTO> GetUserCases(int id, string kindofcase);
        ClientDTO GetCaseClient(CaseDTO caseDTO);
        void Dispose();
    }
}
