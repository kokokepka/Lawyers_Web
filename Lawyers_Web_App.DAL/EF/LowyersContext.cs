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
        public DbSet<User> Users { get; set; }
        public DbSet<Document> Documents { get; set; }

        private string connectionString;

        public LowyersContext()
        {
            Database.EnsureCreated();
            connectionString = "";
        }

        public LowyersContext(string connectionString)
        {
            Database.EnsureCreated();
            this.connectionString = connectionString;
        }

        // инициализация базы данных начальными значениями
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=LawyersDb;Trusted_Connection=True;MultipleActiveResultSets=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            User[] users = new User[]
                {
                    new User { Id=1, Name="User1", LastName = "Last1", Patronymic = "Patronymic1", Address = "Address1"},
                    new User { Id=1, Name="User2", LastName = "Last2", Patronymic = "Patronymic1", Address = "Address1"},
                    new User { Id=1, Name="User3", LastName = "Last3", Patronymic = "Patronymic1", Address = "Address1"}
                };
            modelBuilder.Entity<User>().HasData(users);

            modelBuilder.Entity<Document>().HasData(
                new Document[]
                {
                    new Document { Id=1, Name="Doc1", Path = "//", User = users[0], Date = DateTime.Now.Date },
                    new Document { Id=1, Name="Doc2", Path = "//", User = users[1], Date = DateTime.Now.Date },
                    new Document { Id=1, Name="Doc3", Path = "//", User = users[2], Date = DateTime.Now.Date }
                });
        }
    }
}
