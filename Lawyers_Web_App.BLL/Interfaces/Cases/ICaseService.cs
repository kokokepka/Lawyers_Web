using Lawyers_Web_App.BLL.DTO;
using Lawyers_Web_App.BLL.DTO.CasesDTO;
using Lawyers_Web_App.BLL.DTO.OtherDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lawyers_Web_App.BLL.Interfaces.Cases
{
    public interface ICaseService
    {
        void StartCase(CaseDTO caseDto);
        void AddParticipant(ClientDTO clientDTO);
        CaseDTO GetCase(int? id);
        IEnumerable<CaseDTO> GetUserCases(int id);
        IEnumerable<CaseDTO> GetClientCases(ClientDTO client);
        ClientDTO GetCaseClient(CaseDTO caseDTO);
        void Dispose();
    }
}
