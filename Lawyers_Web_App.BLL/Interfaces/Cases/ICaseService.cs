using Lawyers_Web_App.BLL.DTO;
using Lawyers_Web_App.BLL.DTO.CasesDTO;
using Lawyers_Web_App.BLL.DTO.DocDTO;
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
        IEnumerable<CaseUserDTO> GetParticipant(int caseId);
        CaseUserDTO GetClient(int caseId);
        IEnumerable<CaseDocDTO> GetCaseDocs(int caseId);
        void AddDocument(CaseDocDTO doc);
        IEnumerable<RoleCaseDTO> GetRoleCase(int kindCaseId);
        IEnumerable<InstanceDTO> GetInstances(int kindCaseId);
        KindOfCaseDTO GetKind(int kind_id);
        KindOfCaseDTO FindKindByName(string kind_name);
        void Delete(int userId);
        void Dispose();
    }
}
