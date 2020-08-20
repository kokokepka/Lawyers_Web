using Lawyers_Web_App.BLL.DTO;
using Lawyers_Web_App.BLL.DTO.OtherDTO;
using Lawyers_Web_App.BLL.Infrastructure;
using Lawyers_Web_App.BLL.Interfaces.Cases;
using Lawyers_Web_App.DAL.Entities.Cases;
using Lawyers_Web_App.BLL.Interfaces.Other;
using Lawyers_Web_App.BLL.Mappers;
using Lawyers_Web_App.DAL.Entities;
using Lawyers_Web_App.DAL.Entities.AccountEntities;
using Lawyers_Web_App.DAL.Entities.UserEntities;
using Lawyers_Web_App.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Lawyers_Web_App.BLL.DTO.CasesDTO;
using System.Linq;
using Lawyers_Web_App.DAL.Entities.Cases.Additionally;
using Microsoft.EntityFrameworkCore.Internal;
using Lawyers_Web_App.BLL.DTO.UsersDTO;
using Lawyers_Web_App.BLL.DTO.DocDTO;
using Lawyers_Web_App.DAL.Entities.Documents;
using System.Collections;

namespace Lawyers_Web_App.BLL.Services.Cases
{
    public class CaseService : ICaseService
    {
        IUnitOfWork _database { get; set; }

        public CaseService(IUnitOfWork database)
        {
            _database = database;
        }

        public void Dispose()
        {
            _database.Dispose();
        } 

        public void StartCase(CaseDTO caseDto, CaseUserDTO caseUser)
        {
            KindOfCase kindOfCase = _database.KindOfCases.Find(k => k.Id == caseDto.KindOfCaseId).FirstOrDefault();
            if (kindOfCase == null)
                throw new ValidationException(caseDto.KindOfCase + "- не найден", "");
            Instance instance = _database.Instances.Find(i => i.Id == caseDto.InstanceId).FirstOrDefault();
            if(instance == null)
                throw new ValidationException(caseDto.Instance + "- не найден", "");
            User user = _database.Users.Get(caseDto.UserId);
            if (user == null)
                throw new ValidationException("Пользователь не найден", "");
            //Case _case = _database.Cases.Find(c => c.Title == caseDto.Title).FirstOrDefault();
            //if (_case != null)
            //    throw new ValidationException("Дело с таким названием уже существует", "");
            Case _case = new Case
            {
                Title = "Дело " + caseUser.Surname +" "+ caseUser.Name[0]+". "+caseUser.Patronymic[0],
                KindOfCase = kindOfCase,
                Instance = instance,
                User = user,
                Date = caseDto.Date,
                ArticleOrCategory = caseDto.ArticleOrCategory,
                VerdictOrDecision = caseDto.VerdictOrDecision,
            };
            _database.Cases.Create(_case);
            _database.Save();
            CaseUser _caseUser = _database.CaseUsers.Find(c => c.Name == caseUser.Name && 
            c.Patronymic == caseUser.Patronymic &&
            c.Surname == caseUser.Surname && 
            c.DateOfBirth == caseUser.DateOfBirth && 
            c.Address == caseUser.Address).FirstOrDefault();
            if (_caseUser == null)
            {
                RoleInTheCase role = _database.CaseRoles.Get(caseUser.RoleInTheCaseId);
                _caseUser = CreateCaseUser(caseUser, _case, role);
                _database.CaseUsers.Create(_caseUser);
                _database.Save();
            }
            Client client = _database.Clients.Find(c => c.CaseUserId == _caseUser.Id).FirstOrDefault();
            if (client == null)
            {
                client = new Client()
                {
                    CaseUser = _caseUser,
                    Case = _case,
                    Money = 0
                };
                _database.Clients.Create(client);
                _database.Save();
            }
            else
            {
                client.Case = _case;
                _database.Save();
            }
        }

        public CaseDTO GetCase(int? id)
        {
            if (id == null)
                throw new ValidationException("Id дела не найдено", "");
            Case _case = _database.Cases.Get(id);
            if (_case == null)
                throw new ValidationException("Дело не найдено", "");
            CaseDTO caseDto = CreateCaseDto(_case);
            if (caseDto == null)
                throw new ValidationException("Не удалось создать CaseDTO", "");
            return caseDto;
        }

        public IEnumerable<CaseDTO> GetClientCases(ClientDTO client)
        {
            Client _client = _database.Clients.Find(c => c.Id == client.Id).FirstOrDefault();

            if (_client == null)
                throw new ValidationException("Клиент не найден", "");
            var cases = _database.Cases.Find(c => c.Id == _client.CaseUserId);
            return GetListCases(cases);
        }

        public ClientDTO GetCaseClient(CaseDTO caseDTO)
        {
            var client = _database.Clients.Find(c => c.CaseId == caseDTO.Id);
            var mapped = ObjectMapper.Mapper.Map<ClientDTO>(client);
            return mapped;
        }

        public void AddParticipant(int caseId, CaseUserDTO caseUserDTO)
        {
            Case _case = _database.Cases.Get(caseId);
            if(_case == null)
                throw new ValidationException("Дело не найдено", "");
            CaseUser caseUser = _database.CaseUsers.Get(caseUserDTO.Id);
            if (caseUser == null)
            {
                RoleInTheCase role = _database.CaseRoles.Get(caseUserDTO.RoleInTheCaseId);
                caseUser = CreateCaseUser(caseUserDTO, _case, role);
                _database.CaseUsers.Create(caseUser);
                _database.Save();
            }
            _case.Participants.Add(caseUser);
            _database.Save();
        }

        public IEnumerable<CaseDTO> GetUserCases(int id, string kindofcase)
        {
            User user = _database.Users.Get(id);
            if (user == null)
                throw new ValidationException("Пользователь не найден", "");
            IEnumerable<Case> cases = _database.Cases.Find(c => c.UserId == user.Id && c.KindOfCase.Name == kindofcase);           
            return GetListCases(cases);
        }

        public IEnumerable<CaseUserDTO> GetParticipant(int caseId)
        {
            //Case _case = _database.Cases.Get(caseId);
            //if (_case == null)
            //    throw new ValidationException("Дело не найдено", "");
            Case _case = GetCase(caseId);
            IEnumerable<CaseUser> caseUsers = _database.CaseUsers.Find(cu => cu.CaseId == _case.Id);
            IList<CaseUserDTO> resultUsers = new List<CaseUserDTO>();
            foreach(var item in caseUsers)
            {
                resultUsers.Add(GetCaseUserDto(item));
            }
            return resultUsers;
        }

        public CaseUserDTO GetClient(int caseId)
        {
            //Case _case = _database.Cases.Get(caseId);
            //if (_case == null)
            //    throw new ValidationException("Дело не найдено", "");
            Case _case = GetCase(caseId);
            CaseUser caseUser = _database.CaseUsers.Find(cu => cu.CaseId == _case.Id).FirstOrDefault();
            return GetCaseUserDto(caseUser);
        }

        public IEnumerable<CaseDocDTO> GetCaseDocs(int caseId)
        {
            //Case _case = _database.Cases.Get(caseId);
            //if (_case == null)
            //    throw new ValidationException("Дело не найдено", "");
            Case _case = GetCase(caseId);
            IEnumerable<CaseDocument> clientDocuments = _database.ClientDocuments.Find(cd => cd.CaseId == _case.Id);
            var mapped = ObjectMapper.Mapper.Map<IEnumerable<CaseDocDTO>>(clientDocuments);
            return mapped;
        }

        private IEnumerable<CaseDTO> GetListCases(IEnumerable<Case> cases)
        {
            IList<CaseDTO> caseDtos = new List<CaseDTO>();
            foreach (var item in cases)
            {
                caseDtos.Add(CreateCaseDto(item));
            }
            return caseDtos;
        }

        private CaseDTO CreateCaseDto(Case _case)
        {
            return new CaseDTO
            {
                Id = _case.Id,
                Title = _case.Title,
                KindOfCaseId = _case.KindOfCaseId,
                KindOfCase = _case.KindOfCase.Name,
                InstanceId = _case.InstanceId,
                Instance = _case.Instance.Name,
                ClientId = _case.Client.Id,
                UserId = _case.User.Id,
                Date = _case.Date,
                ArticleOrCategory = _case.ArticleOrCategory,
                VerdictOrDecision = _case.VerdictOrDecision,
            };
        }

        private CaseUserDTO GetCaseUserDto(CaseUser caseUser)
        {
            return new CaseUserDTO
            {
                Id = caseUser.Id,
                Name = caseUser.Name,
                Surname = caseUser.Surname,
                Patronymic = caseUser.Patronymic,
                DateOfBirth = caseUser.DateOfBirth,
                Phone = caseUser.Phone,
                HomePhone = caseUser.HomePhone,
                Email = caseUser.Email,
                Address = caseUser.Address,
                CaseId = caseUser.CaseId,
                RoleInTheCaseId = caseUser.RoleInTheCaseId, 
                RoleInTheCase = caseUser.RoleInTheCase.Name
            };
        }

        private CaseUser CreateCaseUser(CaseUserDTO caseUser, Case _case, RoleInTheCase role)
        {
            return new CaseUser
            {
                Name = caseUser.Name,
                Surname = caseUser.Surname,
                Patronymic = caseUser.Patronymic,
                DateOfBirth = caseUser.DateOfBirth,
                Phone = caseUser.Phone,
                HomePhone = caseUser.HomePhone,
                Email = caseUser.Email,
                Address = caseUser.Address,
                Case = _case,
                RoleInTheCase = role
            };
        }

        private Case GetCase(int id)
        {
            Case _case = _database.Cases.Get(id);
            if (_case == null)
                throw new ValidationException("Дело не найдено", "");
            return _case;
        }

        public void AddDocument(CaseDocDTO doc)
        {
             Case _case = _database.Cases.Get(doc.CaseId);
            if (_case == null)
                throw new ValidationException("Дело не найдено", "");
            CaseDocument _newDoc = new CaseDocument
            {
                Name = doc.Name,
                Path = doc.Path,
                Date = doc.Date,
                Case = _case
            };
            _database.ClientDocuments.Create(_newDoc);
            _database.Save();
        }

        public IEnumerable<RoleCaseDTO> GetRoleCase(int kindCaseId)
        {
            //Case _case = GetCase(caseId);
            KindOfCase kindOfCase = _database.KindOfCases.Get(kindCaseId);
            if (kindOfCase == null)
                throw new ValidationException("Вид дела не найден", "");
            var roles = _database.CaseRoles.Find(r => r.KindOfCaseId == kindOfCase.Id);
            var mapped = ObjectMapper.Mapper.Map<IEnumerable<RoleCaseDTO>>(roles);
            return mapped;
        }

        public void DeleteDoc(int Id)
        {
            CaseDocument doc = _database.ClientDocuments.Get(Id);
            if (doc == null)
                throw new ValidationException("Документ не найден", "");
            _database.ClientDocuments.Delete(doc.Id);
            _database.Save();
        }

        public IEnumerable<InstanceDTO> GetInstances(int kindCaseId)
        {
            KindOfCase kindOfCase = _database.KindOfCases.Get(kindCaseId);
            if (kindOfCase == null)
                throw new ValidationException("Вид дела не найден", "");
            var instances = _database.Instances.Find(r => r.KindOfCaseInstances.FirstOrDefault
            (f => f.KindOfCaseId == kindOfCase.Id).KindOfCaseId == kindOfCase.Id);
            var mapped = ObjectMapper.Mapper.Map<IEnumerable<InstanceDTO>>(instances);
            return mapped;
        }

        public KindOfCaseDTO GetKind(int kind_id)
        {
            KindOfCase kindOfCase = _database.KindOfCases.Get(kind_id);
            if (kindOfCase == null)
                throw new ValidationException("Вид не найден", "");
            var _tmp_instance = kindOfCase.KindOfCaseInstances.Select(i => i.Instance).ToList();
            KindOfCaseDTO kind = new KindOfCaseDTO
            {
                Id = kindOfCase.Id,
                Name = kindOfCase.Name
            };
            if (_tmp_instance == null)
                throw new ValidationException("инстанции не найдены", "");
            var map = ObjectMapper.Mapper.Map<IEnumerable<InstanceDTO>>(_tmp_instance);
            kind.Instances = map;
            return kind;
        }

        public KindOfCaseDTO FindKindByName(string kind_name)
        {
            var kindOfCase = _database.KindOfCases.Find(k => k.Name == kind_name).FirstOrDefault();
            if (kindOfCase == null)
                throw new ValidationException("Вид не найден", "");
            var map = ObjectMapper.Mapper.Map<KindOfCaseDTO>(kindOfCase);
            return map;
        }

        public void DeleteCase(int case_id)
        {
            Case _case = _database.Cases.Get(case_id);
            if (_case == null)
                throw new ValidationException("Дело не найдено", "");
            _database.Cases.Delete(_case.Id);
            _database.Save();
        }
    }
}
