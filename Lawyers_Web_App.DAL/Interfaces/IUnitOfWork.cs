using Lawyers_Web_App.DAL.Entities;
using Lawyers_Web_App.DAL.Entities.Documents;
using Lawyers_Web_App.DAL.Entities.Cases;
using Lawyers_Web_App.DAL.Entities.UserEntities;
using System;
using System.Collections.Generic;
using System.Text;
using Lawyers_Web_App.DAL.Entities.AccountEntities;
using Lawyers_Web_App.DAL.Entities.Cases.Additionally;

namespace Lawyers_Web_App.DAL.Interfaces
{
    // Паттерн Unit of Work позволяет упростить работу с различными репозиториями
    public interface IUnitOfWork : IDisposable
    {
        IRepository<User> Users { get; }
        IRepository<CaseUser> CaseUsers { get; }
        IRepository<Client> Clients { get; }
        IRepository<Role> Roles { get; }
        IRepository<UserDocument> UserDocuments { get; }
        IRepository<ClientDocument> ClientDocuments { get; }
        IRepository<Note> Notes { get; }
        IRepository<Case> Cases { get; }
        IRepository<Category> Categories { get; }
        IRepository<KindOfCase> KindOfCases { get; }
        IRepository<Instance> Instances { get; }
        IRepository<RoleInTheCase> CaseRoles { get; }
        void Save();
    }
}
