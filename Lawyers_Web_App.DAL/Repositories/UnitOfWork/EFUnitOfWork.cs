﻿using Lawyers_Web_App.DAL.EF;
using Lawyers_Web_App.DAL.Entities;
using Lawyers_Web_App.DAL.Entities.Documents;
using Lawyers_Web_App.DAL.Entities.Cases;
using Lawyers_Web_App.DAL.Entities.UserEntities;
using Lawyers_Web_App.DAL.Interfaces;
using Lawyers_Web_App.DAL.Repositories.OtherRep;
using System;
using System.Collections.Generic;
using System.Text;
using Lawyers_Web_App.DAL.Repositories.AccountRep;
using Lawyers_Web_App.DAL.Repositories.DocumentRep;
using Lawyers_Web_App.DAL.Repositories.CaseRep;
using Lawyers_Web_App.DAL.Entities.AccountEntities;
using Lawyers_Web_App.DAL.Entities.Cases.Additionally;
using Lawyers_Web_App.DAL.Entities.Other;

namespace Lawyers_Web_App.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private LowyersContext db;
        private UserRepository _userRepository;
        private ClientRepository _clientProfileRepository;
        private CaseUserRepository _caseUserRepository;
        private RoleRepository _roleRepository;
        private UserDocumentRepository _userDocumentRepository;
        private ClientDocumentRepository _clientDocumentRepository;
        private NoteRepository _noteRepositity;
        private CaseRepository _caseRepositoty;
        private InstanceRepository _instanceRepository;
        private KindOfCaseRepository _kindOfCaseRepository;
        private RoleInTheCaseRepository _roleInTheCaseRepository;
        private QuestionRepository _questionRepository;
        private CommentRepository _commentRepository;
        private AnswerRepository _answerRepository;
        private PriceRepository _priceRepository;
        private ScheduleRepository _scheduleRepository;


        public EFUnitOfWork()
        {
            db = new LowyersContext(LowyersContext.ops.dbOption);
        }

        public IRepository<User> Users => _userRepository ?? new UserRepository(db);

        public IRepository<Role> Roles => _roleRepository ?? new RoleRepository(db);

        public IRepository<UserDocument> UserDocuments => _userDocumentRepository ?? new UserDocumentRepository(db);

        public IRepository<CaseDocument> ClientDocuments => _clientDocumentRepository ?? new ClientDocumentRepository(db);

        public IRepository<Note> Notes => _noteRepositity ?? new NoteRepository(db);

        public IRepository<CaseUser> CaseUsers => _caseUserRepository ?? new CaseUserRepository(db);

        public IRepository<Case> Cases => _caseRepositoty ?? new CaseRepository(db);

        public IRepository<Client> Clients => _clientProfileRepository ?? new ClientRepository(db);

        public IRepository<KindOfCase> KindOfCases => _kindOfCaseRepository ?? new KindOfCaseRepository(db);

        public IRepository<Instance> Instances => _instanceRepository ?? new InstanceRepository(db);

        public IRepository<RoleInTheCase> CaseRoles => _roleInTheCaseRepository ?? new RoleInTheCaseRepository(db);

        public IRepository<Question> Questions => _questionRepository ?? new QuestionRepository(db);

        public IRepository<Comment> Comments => _commentRepository ?? new CommentRepository(db);

        public IRepository<Answer> Answers => _answerRepository ?? new AnswerRepository(db);

        public IRepository<Price> Prices => _priceRepository ?? new PriceRepository(db);

        public IRepository<Schedule> Schedules => _scheduleRepository ?? new ScheduleRepository(db);

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
