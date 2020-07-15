using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Lawyers_Web_App.BLL.DTO;
using Lawyers_Web_App.DAL.Entities;

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
                CreateMap<Document, DocumentDTO>().ReverseMap();         
            }
        }
    }
}
