using Lawyers_Web_App.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lawyers_Web_App.DAL.Interfaces
{
    // Паттерн Unit of Work позволяет упростить работу с различными репозиториями
    public interface IUnitOfWork : IDisposable
    {
        IRepository<User> Users { get; }
        IRepository<Document> Documents { get; }
        void Save();
    }
}
