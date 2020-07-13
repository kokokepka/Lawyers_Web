using Lawyers_Web_App.DAL.EF;
using Lawyers_Web_App.DAL.Entities;
using Lawyers_Web_App.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lawyers_Web_App.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private LowyersContext db;
        private UserRepository userRepository;
        private DocumentRepository documentRepository;

        public EFUnitOfWork(string connectionString)
        {
            db = new LowyersContext(connectionString);
        }

        public IRepository<User> Users
        {
            get
            {
                if(userRepository == null)
                {
                    userRepository = new UserRepository(db);
                }
                return userRepository;
            }
        }

        public IRepository<Document> Documents
        {
            get
            {
                if(documentRepository == null)
                {
                    documentRepository = new DocumentRepository(db);
                }
                return documentRepository;
            }
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public void Save()
        {
            db.SaveChanges();
        }
    }
}
