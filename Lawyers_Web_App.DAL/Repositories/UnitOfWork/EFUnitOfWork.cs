using Lawyers_Web_App.DAL.EF;
using Lawyers_Web_App.DAL.Entities;
using Lawyers_Web_App.DAL.Entities.Documents;
using Lawyers_Web_App.DAL.Entities.Other;
using Lawyers_Web_App.DAL.Entities.UserEntities;
using Lawyers_Web_App.DAL.Interfaces;
using Lawyers_Web_App.DAL.Repositories.Document;
using Lawyers_Web_App.DAL.Repositories.OtherRep;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lawyers_Web_App.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private LowyersContext db;
        private UserRepository _userRepository;
        private ClientRepository _clientProfileRepository;
        private RoleRepository _roleRepository;
        private UserDocumentRepository _userDocumentRepository;
        private ClientDocumentRepository _clientDocumentRepository;
        private NoteRepositity _noteRepositity;
        private CaseRepository _caseRepositoty;
        public EFUnitOfWork()
        {
            db = new LowyersContext(LowyersContext.ops.dbOption);
        }

        public IRepository<User> Users => _userRepository ?? new UserRepository(db);
        public IRepository<Role> Roles => _roleRepository ?? new RoleRepository(db);
        public IRepository<ClientProfile> ClientProfiles => _clientProfileRepository ?? new ClientRepository(db);
        public IRepository<UserDocument> UserDocuments => _userDocumentRepository ?? new UserDocumentRepository(db);
        public IRepository<ClientDocument> ClientDocuments => _clientDocumentRepository ?? new ClientDocumentRepository(db);
        public IRepository<Note> Notes => _noteRepositity ?? new NoteRepositity(db);

        public IRepository<Case> Cases => _caseRepositoty ?? new CaseRepository(db);

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
