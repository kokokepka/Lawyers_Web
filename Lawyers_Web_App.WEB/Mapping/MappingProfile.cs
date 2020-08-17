using AutoMapper;
using Lawyers_Web_App.BLL.DTO;
using Lawyers_Web_App.BLL.DTO.CasesDTO;
using Lawyers_Web_App.BLL.DTO.DocDTO;
using Lawyers_Web_App.BLL.DTO.OtherDTO;
using Lawyers_Web_App.BLL.DTO.UsersDTO;
using Lawyers_Web_App.WEB.Models;
using Lawyers_Web_App.WEB.Models.Cases;
using Lawyers_Web_App.WEB.Models.Other;
using Lawyers_Web_App.WEB.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lawyers_Web_App.WEB.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<UserDTO, UserViewModel>()
                .ForMember(opt => opt.Avatar, uopt => uopt.MapFrom(src => src.Avatar));
            CreateMap<CaseDTO, CaseViewModel>();
            CreateMap<CaseUserDTO, CaseUserViewModel>();
            CreateMap<CaseDocDTO, DocViewModel>()
                .ForMember(opt => opt.SomethingId, opt => opt.MapFrom(src => src.CaseId));
            CreateMap<UserDocDTO, DocViewModel>()
                 .ForMember(opt => opt.SomethingId, opt => opt.MapFrom(src => src.UserId));
            CreateMap<DocViewModel, CaseDocDTO>()
               .ForMember(opt => opt.CaseId, opt => opt.MapFrom(src => src.SomethingId));
            CreateMap<DocViewModel, UserDocDTO>()
                 .ForMember(opt => opt.UserId, opt => opt.MapFrom(src => src.SomethingId));
            CreateMap<RoleCaseDTO, RoleCaseModel>();
            CreateMap<NoteDTO, NoteViewModel>();
            CreateMap<KindOfCaseDTO, KindOfCaseModel>();
            CreateMap<InstanceDTO, InstanceModel>();
        }
    }
}
