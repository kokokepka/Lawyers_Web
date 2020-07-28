using Lawyers_Web_App.DAL.EF;
using Lawyers_Web_App.DAL.Interfaces;
using Lawyers_Web_App.DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Lawyers_Web_App.BLL.Interfaces;
using Lawyers_Web_App.BLL.Services;
using Lawyers_Web_App.BLL.Interfaces.Documents;
using Lawyers_Web_App.BLL.DTO.DocDTO;
using Lawyers_Web_App.BLL.DTO;
using Lawyers_Web_App.BLL.Interfaces.Other;
using Lawyers_Web_App.BLL.DTO.OtherDTO;

namespace Lawyers_Web_App.BLL.Infrastructure
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddInternalServices(this IServiceCollection services, IConfiguration configuration)
        {            
            services.AddDbContext<LowyersContext>(c =>
                c.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            //services.AddScoped<IUserDocumentService, DocumentUserService>();         
            services.AddScoped<IDocService<UserDocDTO, UserDTO>, DocumentUserService>();
            services.AddScoped<IDocService<CaseDocDTO, CaseDTO>, DocumentCaseService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<INoteService, NoteService>();
            services.AddScoped<ICaseService, CaseService>();
            services.AddScoped<IUnitOfWork, EFUnitOfWork>();
            return services;
        }
    }
}
