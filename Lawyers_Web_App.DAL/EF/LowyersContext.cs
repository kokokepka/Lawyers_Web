using Lawyers_Web_App.DAL.Entities;
using Lawyers_Web_App.DAL.Entities.Documents;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasOne(u => u.Role).WithMany(r => r.Users).OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<UserDocument>().HasOne(u => u.User).WithMany(d => d.Documents).OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<ClientDocument>().HasOne(c => c.Client).WithMany(d => d.Documents).OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Role>().HasData(new Role[]
            {
                new Role(){Id = 1, Name= "Заведующий", Design = "main-lawyer"},
                new Role(){Id = 2, Name = "Адвокат", Design = "lawyer"}
            });
        }

    }
}
