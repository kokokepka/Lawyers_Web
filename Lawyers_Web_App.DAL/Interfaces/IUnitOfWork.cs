using Lawyers_Web_App.DAL.Entities;
using Lawyers_Web_App.DAL.Entities.Documents;
using Lawyers_Web_App.DAL.Entities.Other;
using Lawyers_Web_App.DAL.Entities.UserEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lawyers_Web_App.DAL.Interfaces
{
    // Паттерн Unit of Work позволяет упростить работу с различными репозиториями
    public interface IUnitOfWork : IDisposable
    {
        IRepository<User> Users { get; }
        IRepository<ClientProfile> ClientProfiles { get; }
        IRepository<Role> Roles { get; }
        IRepository<UserDocument> UserDocuments { get; }
        IRepository<ClientDocument> ClientDocuments { get; }
        IRepository<Note> Notes { get; }
        IRepository<Case> Cases { get; }
        void Save();
    }
}
