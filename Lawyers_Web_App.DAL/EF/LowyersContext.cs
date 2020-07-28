﻿using Lawyers_Web_App.DAL.Entities;
using Lawyers_Web_App.DAL.Entities.Documents;
using Lawyers_Web_App.DAL.Entities.Other;
using Lawyers_Web_App.DAL.Entities.UserEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lawyers_Web_App.DAL.EF
{
    // Класс контекста данных
    public class LowyersContext:DbContext
    {
        public class OptionsBuild
        {
            public OptionsBuild()
            {
                setting = new AppConfiguration();
                opsBuilder = new DbContextOptionsBuilder<LowyersContext>();
                opsBuilder.UseSqlServer(setting.sqlConnectionString);
                dbOption = opsBuilder.Options;
            }
            public DbContextOptionsBuilder<LowyersContext> opsBuilder { get; set; }
            public DbContextOptions<LowyersContext> dbOption { get; set; }
            private AppConfiguration setting { get; set; }
        }
        public static OptionsBuild ops = new OptionsBuild();

        public LowyersContext(DbContextOptions<LowyersContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<ClientProfile> ClientProfiles { get; set; }
        public DbSet<Role> Roles { get; set; } 
        public DbSet<UserDocument> UserDocuments { get; set; }
        public DbSet<ClientDocument> ClientDocuments { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Case> Cases { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasOne(u => u.Role).WithMany(r => r.Users)
                .HasForeignKey(u=>u.RoleId).OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<UserDocument>().HasOne(u => u.User).WithMany(d => d.Documents)
                .HasForeignKey(u=>u.UserId);
            modelBuilder.Entity<ClientDocument>().HasOne(c => c.Case).WithMany(d => d.Documents)
                .HasForeignKey(u=>u.CaseId).OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Note>().HasOne(s => s.User).WithMany(u => u.Notes)
                .HasForeignKey(n=>n.UserId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ClientProfile>().HasOne(c => c.Case).WithOne(c => c.Client)
                .HasForeignKey<Case>(c=>c.ClientId).OnDelete(DeleteBehavior.SetNull); ;

            Role[] roles = new Role[]
            {
                new Role(){Id = 1, Name= "Заведующий", Design = "main-lawyer"},
                new Role(){Id = 2, Name = "Адвокат", Design = "lawyer"}
            };
            modelBuilder.Entity<Role>().HasData(roles);

            User[] users = new User[]
            {
                new User()
                {
                    Id = 1,
                    Login="asada",
                    Password = "77BF1AD4AA4741B8C9D35443FF8AFEAC",
                    Name = "Асадчая",
                    Surname = "Светлана",
                    Patronymic = "Яковлевна",
                    DateOfBirth = DateTime.Parse("29.08.1969"),
                    Email = "asadchaya.a.s@gmail.com",
                    Phone = "+375(44)747-88-51",
                    RoleId = 1                   
                },

                new User()
                {
                    Id = 2,
                    Login="law",
                    Password = "52CF164593824035FBB66861577A0C32",
                    Name = "Пивнова",
                    Surname = "Елена",
                    Patronymic = "Михайловна",
                    DateOfBirth = DateTime.Parse("09.04.1975"),
                    Email = "pivaaa@gmail.com",
                    Phone = "+375(44)695-25-44",
                    RoleId = 2
                }
            };

            modelBuilder.Entity<User>().HasData(users);

            ClientProfile client = new ClientProfile()
            {
                Id = 1,
                Name = "Пупкин",
                Surname = "Андрей",
                Patronymic = "Леонидович",
                DateOfBirth = DateTime.Parse("22.05.1987"),
                Email = "pup@gmail.com",
                Phone = "+375(44)695-25-44"
            };
            modelBuilder.Entity<ClientProfile>().HasData(client);

            modelBuilder.Entity<Case>().HasData(new Case[]
            {
                new Case
                {
                    Id = 1,
                    Title = "Дело № 1",
                    IsOpen = true,
                    ClientId = 1,
                    UserId = 1
                }
            });
        }
    }
}
