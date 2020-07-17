using Lawyers_Web_App.DAL.Interfaces;
using Lawyers_Web_App.DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lawyers_Web_App.BLL.Infrastructure
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddInternalServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, EFUnitOfWork>();
            return services;
        }
    }
}
