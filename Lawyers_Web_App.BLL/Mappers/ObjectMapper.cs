using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Lawyers_Web_App.BLL.DTO;
using Lawyers_Web_App.BLL.DTO.CasesDTO;
using Lawyers_Web_App.BLL.DTO.DocDTO;
using Lawyers_Web_App.BLL.DTO.OtherDTO;
using Lawyers_Web_App.BLL.DTO.UsersDTO;
using Lawyers_Web_App.DAL.Entities;
using Lawyers_Web_App.DAL.Entities.AccountEntities;
using Lawyers_Web_App.DAL.Entities.Cases;
using Lawyers_Web_App.DAL.Entities.Cases.Additionally;
using Lawyers_Web_App.DAL.Entities.Documents;
using Lawyers_Web_App.DAL.Entities.Other;
using Lawyers_Web_App.DAL.Entities.UserEntities;

namespace Lawyers_Web_App.BLL.Mappers
{
    public static class ObjectMapper
    {
        private static Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                // This line ensures that internal properties are also mapped over.
                cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                cfg.AddProfile<DtoMapper>();
            });
            var mapper = config.CreateMapper();
            return mapper;
        });

        public static IMapper Mapper => Lazy.Value;

        public class DtoMapper : Profile
        {
            public DtoMapper()
            {
                CreateMap<User, UserDTO>().ReverseMap();
                //CreateMap<CaseUser, CaseUserDTO>().ReverseMap()
                //    .For(c => c.RoleInTheCase.Name, cd => cd.MapFrom(src => src.RoleInTheCase));
                CreateMap<UserDocument, UserDocDTO>().ReverseMap();
                CreateMap<Client, ClientDTO>().ReverseMap();
                CreateMap<CaseDocument, CaseDocDTO>().ReverseMap();
                CreateMap<Note, NoteDTO>().ReverseMap();
                CreateMap<RoleInTheCase, RoleCaseDTO>().ReverseMap();
                CreateMap<Instance, InstanceDTO>().ReverseMap();
                CreateMap<KindOfCase, KindOfCaseDTO>().ReverseMap();
                CreateMap<Question, QuestionDTO>().ReverseMap();
                CreateMap<Comment, CommentDTO>().ReverseMap();
                CreateMap<Answer, AnswerDTO>().ReverseMap();
                //CreateMap<Case, CaseDTO>().ReverseMap().AfterMap((_case, caseDto) => _case.Instance = caseDto.Instance.Name)
                //    .ForPath(c => c.Category.Name, cd => cd.MapFrom(src => src.Category))
                //    //.ForPath(c => c.Instance.Name, cd => cd.MapFrom(src => src.Instance))
                //    .ForPath(c => c.KindOfCase.Name, cd => cd.MapFrom(src => src.KindOfCase));
            }
        }
    }
}
