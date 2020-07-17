using Lawyers_Web_App.DAL.Entities;
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
        public DbSet<Document> Documents { get; set; }

        
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    //modelBuilder.Entity<User>().HasKey(u => u.Id);
        //    //modelBuilder.Entity<User>().HasMany(u => u.Documents).WithOne(e => e.User).HasForeignKey(u => u.UserId).IsRequired();
        //    //modelBuilder.Entity<Document>().HasKey(d=>d.Id);
        //    //modelBuilder.Entity<Document>().HasOne(u => u.User).WithMany(u => u.Documents).HasForeignKey(d => d.UserId).IsRequired();
        //    //modelBuilder.Entity<Document>()
        //    //               .Property(e => e.Id)
        //    //               .ValueGeneratedOnAdd();  
        //}
    }
}
