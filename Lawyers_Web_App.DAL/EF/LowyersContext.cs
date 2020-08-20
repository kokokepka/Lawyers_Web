using Lawyers_Web_App.DAL.Entities;
using Lawyers_Web_App.DAL.Entities.Documents;
using Lawyers_Web_App.DAL.Entities.Cases;
using Lawyers_Web_App.DAL.Entities.UserEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using Lawyers_Web_App.DAL.Entities.AccountEntities;
using Lawyers_Web_App.DAL.Entities.Cases.Additionally;
using Lawyers_Web_App.DAL.Entities.Other;

namespace Lawyers_Web_App.DAL.EF
{
    // Класс контекста данных
    public class LowyersContext : DbContext
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
        public DbSet<CaseUser> CaseUsers { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserDocument> UserDocuments { get; set; }
        public DbSet<CaseDocument> ClientDocuments { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Case> Cases { get; set; }
        public DbSet<Instance> Instances { get; set; }
        public DbSet<KindOfCase> KindOfCases { get; set; }
        public DbSet<RoleInTheCase> RoleInTheCases { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<Schedule> Schedules { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasOne(u => u.Role).WithMany(r => r.Users)
               .HasForeignKey(u => u.RoleId);
            modelBuilder.Entity<User>().HasMany(u => u.Cases).WithOne(c => c.User).HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<User>().HasMany(u => u.Documents).WithOne(d => d.User).HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<User>().HasMany(u => u.Notes).WithOne(n => n.User).HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<User>().HasOne(u => u.Schedule).WithMany(s => s.Users).HasForeignKey(u => u.ScheduleId);
            modelBuilder.Entity<Case>().HasMany(c => c.Documents).WithOne(d => d.Case)
                .HasForeignKey(u => u.CaseId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<CaseUser>().HasOne(c => c.Case).WithMany(c => c.Participants)
                .HasForeignKey(c => c.CaseId);

            modelBuilder.Entity<Case>().HasOne(c => c.Client).WithOne(cu => cu.Case)
                .HasForeignKey<Client>(c => c.CaseId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CaseUser>().HasOne(c => c.Client).WithOne(c => c.CaseUser)
                .HasForeignKey<Client>(c => c.CaseUserId);

            modelBuilder.Entity<Instance>().HasMany(i => i.Cases).WithOne(c => c.Instance)
                .HasForeignKey(c => c.InstanceId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<KindOfCase>().HasMany(i => i.Cases).WithOne(c => c.KindOfCase)
                .HasForeignKey(c => c.KindOfCaseId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<RoleInTheCase>().HasMany(i => i.CaseUsers).WithOne(c => c.RoleInTheCase)
                .HasForeignKey(c => c.RoleInTheCaseId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<RoleInTheCase>().HasOne(r => r.KindOfCase).WithMany(k => k.RoleInTheCases)
                .HasForeignKey(r => r.KindOfCaseId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<KindOfCaseInstance>()
            .HasKey(k => new { k.KindOfCaseId, k.InstanceId });

            modelBuilder.Entity<KindOfCaseInstance>()
                .HasOne(kc => kc.KindOfCase)
                .WithMany(ki => ki.KindOfCaseInstances)
                .HasForeignKey(kc => kc.KindOfCaseId);

            modelBuilder.Entity<KindOfCaseInstance>()
                .HasOne(i => i.Instance)
                .WithMany(ki => ki.KindOfCaseInstances)
                .HasForeignKey(i => i.InstanceId);

            modelBuilder.Entity<Question>().HasMany(q => q.Answers)
                .WithOne(a => a.Question).HasForeignKey(a => a.QuestionId).OnDelete(DeleteBehavior.Cascade);

            Role[] roles = new Role[]
            {
                new Role(){Id = 1, Name= "Заведующий", Design = "main-lawyer"},
                new Role(){Id = 2, Name = "Адвокат", Design = "lawyer"}
            };
            modelBuilder.Entity<Role>().HasData(roles);

            KindOfCase[] kindOfCases = new KindOfCase[]
            {
                new KindOfCase(){Id = 1, Name = "Уголовное дело"},
                new KindOfCase(){Id = 2, Name = "Гражданское дело"},
                new KindOfCase(){Id = 3, Name = "Административное дело"},
            };

            Instance[] instances = new Instance[]
            {
                new Instance(){Id = 1, Name = "первая инстанция" },
                new Instance(){Id = 2, Name = "апелляционная инстанция"},
                new Instance(){Id = 3, Name = "надзорная инстанция"},
                new Instance(){Id = 4, Name = "областной суд"},
                new Instance(){Id = 5, Name = "верховный суд"},
            };

            KindOfCaseInstance[] kindOfCaseInstances = new KindOfCaseInstance[]
            {
                new KindOfCaseInstance { InstanceId = instances[0].Id, KindOfCaseId = kindOfCases[0].Id },
                new KindOfCaseInstance { InstanceId = instances[0].Id, KindOfCaseId = kindOfCases[1].Id },
                new KindOfCaseInstance { InstanceId = instances[0].Id, KindOfCaseId = kindOfCases[2].Id },
                new KindOfCaseInstance { InstanceId = instances[1].Id, KindOfCaseId = kindOfCases[0].Id },
                new KindOfCaseInstance { InstanceId = instances[1].Id, KindOfCaseId = kindOfCases[1].Id },
                new KindOfCaseInstance { InstanceId = instances[2].Id, KindOfCaseId = kindOfCases[0].Id },
                new KindOfCaseInstance { InstanceId = instances[2].Id, KindOfCaseId = kindOfCases[1].Id },
                new KindOfCaseInstance { InstanceId = instances[3].Id, KindOfCaseId = kindOfCases[2].Id },
                new KindOfCaseInstance { InstanceId = instances[4].Id, KindOfCaseId = kindOfCases[2].Id }
            };
            modelBuilder.Entity<KindOfCase>().HasData(kindOfCases);
            modelBuilder.Entity<Instance>().HasData(instances);
            modelBuilder.Entity<KindOfCaseInstance>().HasData(kindOfCaseInstances);



            RoleInTheCase[] roleInTheCases = new RoleInTheCase[]
            {
                new RoleInTheCase(){Id = 1, Name = "Обвиняемый", KindOfCaseId = 1},
                new RoleInTheCase(){Id = 2, Name = "Потерпевший", KindOfCaseId = 1},
                new RoleInTheCase(){Id = 3, Name = "Осуждённый", KindOfCaseId = 1},
                new RoleInTheCase(){Id = 4, Name = "Истец", KindOfCaseId = 2},
                new RoleInTheCase(){Id = 5, Name = "Ответчик", KindOfCaseId = 2},
                new RoleInTheCase(){Id = 6, Name = "Третье лицо", KindOfCaseId = 2},
                new RoleInTheCase(){Id = 7, Name = "Заявитель", KindOfCaseId = 2},
                new RoleInTheCase(){Id = 8, Name = "Потерпевший", KindOfCaseId = 3},
                new RoleInTheCase(){Id = 9, Name = "Лицо, в отшении которого ведётся дело", KindOfCaseId = 3},
            };

            modelBuilder.Entity<RoleInTheCase>().HasData(roleInTheCases);

            Schedule[] schedules = new Schedule[]
            {
                new Schedule()
                {
                    Id = 1,
                    DayOfWeek = DayOfWeek.Monday
                },
                new Schedule()
                {
                    Id = 2,
                    DayOfWeek = DayOfWeek.Tuesday
                },
                new Schedule()
                {
                    Id = 3,
                    DayOfWeek = DayOfWeek.Wednesday
                },
                new Schedule()
                {
                    Id = 4,
                    DayOfWeek = DayOfWeek.Thursday
                },
                new Schedule()
                {
                    Id = 5,
                    DayOfWeek = DayOfWeek.Friday
                }
            };
            modelBuilder.Entity<Schedule>().HasData(schedules);

            Price[] prices = new Price[]
            {
                new Price
                {
                    Id = 1, ForWhat = "2.1 Консультация, совет, " +
                    "не требующие ознакомления с документами, от двух до пяти базовых величин ", 
                    StartSum = 54.00, FinishSum = 135.00
                },
                new Price
                {
                    Id = 2, ForWhat = "2.2 Консультация, совет, требующие ознакомления с документами, " +
                    "либо представляющие сложность, от трех до десяти базовых величин ", 
                    StartSum = 81.00, FinishSum = 270.00
                },
                new Price
                {
                    Id = 3, ForWhat = "2.3 Составление правовых документов – от трех до пятнадцати базовых величин", 
                    StartSum = 81.00, FinishSum = 405.00
                },
                new Price
                {
                    Id = 4, ForWhat = "2.4 Составление сложных правовых документов, " +
                    "в том числе связанных с истребованием адвокатами дополнительных материалов " +
                    "и (или) ознакомлением с дополнительными материалами  - от восьми  до двадцати базовых величин",
                    StartSum = 216.00, FinishSum = 540.00
                },
                new Price
                {
                    Id = 5, ForWhat = "2.5 Один день участия адвоката в предварительном следствии, " +
                    "дознании и в судах первой инстанции при ведении уголовных, административных " +
                    "и гражданских дел, в том числе изучение материалов дела, подготовка к ведению дела, " +
                    "посещение в местах лишения свободы, изучение протокола судебного заседания,  " +
                    "составление замечаний на протокол судебного заседания, составление кассационной жалобы," +
                    " - от восьми  до пятнадцати базовых величин "
                    , StartSum = 216.00, FinishSum = 405.00
                },
                new Price
                {
                    Id = 6, ForWhat = "2.6 При сложности дела, а также по делам, связанным с выездом адвоката в командировку, " +
                    "- от десяти до двадцати  базовых величин "
                    , StartSum = 270.00, FinishSum = 540.00
                },
                new Price
                {
                    Id = 7, ForWhat = "2.7 Иные виды юридической помощи " +
                    "(участие в переговорах, подача адвокатского запроса и получение6 ответа на запрос, " +
                    "представление интересов в государственных и иных органах и организациях) – " +
                    "от пяти до двадцати базовых величин ", StartSum = 135.00, FinishSum = 540.00
                },
                new Price
                {
                    Id = 8, ForWhat = "Базовая величина исчислена на 1 января 2020 года в размере ", StartSum = 27.00, FinishSum = 27.00
                }
            };

            User[] users = new User[]
            {
                new User()
                {
                    Id = 1,
                    Login="asada",
                    Password = "77BF1AD4AA4741B8C9D35443FF8AFEAC",
                    Name = "Светлана",
                    Surname = "Асадчая",
                    Patronymic = "Яковлевна",
                    DateOfBirth = DateTime.Parse("29.08.1969"),
                    Email = "asadchaya.a.s@gmail.com",
                    Phone = "+375(44)747-88-51",
                    HomePhone = "30-16-86",
                    RoleId = 1,
                    Address = "Чечерская 17",
                    ScheduleId = 1
                },

                new User()
                {
                    Id = 2,
                    Login="law",
                    Password = "52CF164593824035FBB66861577A0C32",
                    Name = "Елена",
                    Surname = "Пивнова",
                    Patronymic = "Михайловна",
                    DateOfBirth = DateTime.Parse("09.04.1975"),
                    Email = "pivaaa@gmail.com",
                    Phone = "+375(44)695-25-44",
                    HomePhone = "30-36-86",
                    RoleId = 2,
                    Address = "Пушкина 7",
                    ScheduleId = 2
                }
            };
            modelBuilder.Entity<User>().HasData(users);

            Case[] cases = new Case[]
            {
                new Case()
                {
                    Id = 1,
                    Title = "Дело №1",
                    KindOfCaseId = 1,
                    InstanceId = 1,
                    UserId = 1,
                    Date = DateTime.Now.Date,
                    ArticleOrCategory = "122",
                    VerdictOrDecision = "не вынесен"
                },
                new Case()
                {
                    Id = 2,
                    Title = "Дело №2",
                    KindOfCaseId = 3,
                    InstanceId = 4,
                    UserId = 1,
                    Date = DateTime.Now.Date,
                    ArticleOrCategory = "255",
                    VerdictOrDecision = "не вынесен"
                },
                new Case()
                {
                    Id = 3,
                    Title = "Дело №3",
                    KindOfCaseId = 2,
                    InstanceId = 2,
                    UserId = 2,
                    Date = DateTime.Now.Date,
                    ArticleOrCategory = "жилищные",
                    VerdictOrDecision = "не вынесено"
                },
            };
            modelBuilder.Entity<Case>().HasData(cases);

            CaseUser[] caseusers = new CaseUser[]
            {
                new CaseUser()
                {
                    Id = 1,
                    Name = "Пупкин",
                    Surname = "Андрей",
                    Patronymic = "Леонидович",
                    DateOfBirth = DateTime.Parse("22.05.1987"),
                    Email = "pup@gmail.com",
                    Phone = "+375(44)695-25-44",
                    HomePhone = "10-16-86",
                    CaseId = 1,
                    RoleInTheCaseId = 2
                },
                new CaseUser()
                {
                    Id = 2,
                    Name = "Мупкин",
                    Surname = "Игорь",
                    Patronymic = "Леонидович",
                    DateOfBirth = DateTime.Parse("22.05.1987"),
                    CaseId = 1,
                    RoleInTheCaseId = 1
                },
                new CaseUser()
                {
                    Id = 3,
                    Name = "Минина",
                    Surname = "Ирина",
                    Patronymic = "Олеговна",
                    DateOfBirth = DateTime.Parse("22.05.1947"),
                    Phone = "+375(44)665-65-84",
                    CaseId = 2,
                    RoleInTheCaseId = 8
                },
                new CaseUser()
                {
                    Id = 4,
                    Name = "Кучер",
                    Surname = "Магомед",
                    Patronymic = "Алибабович",
                    DateOfBirth = DateTime.Parse("22.05.1990"),
                    CaseId = 2,
                    RoleInTheCaseId = 9
                },
                new CaseUser()
                {
                    Id = 5,
                    Name = "Морган",
                    Surname = "Фримен",
                    Patronymic = "Леонидович",
                    DateOfBirth = DateTime.Parse("22.05.1940"),
                    Email = "morgan@gmail.com",
                    CaseId = 3,
                    RoleInTheCaseId = 4
                },
                new CaseUser()
                {
                    Id = 6,
                    Name = "Джарет",
                    Surname = "Лето",
                    Patronymic = "Леонидович",
                    DateOfBirth = DateTime.Parse("22.05.1970"),
                    Email = "pup@gmail.com",
                    Phone = "+375(44)747-23-78",
                    CaseId = 3,
                    RoleInTheCaseId = 5
                },

            };
            modelBuilder.Entity<CaseUser>().HasData(caseusers);

            Client[] clients = new Client[]
            {
                new Client(){Id = 1, CaseUserId = 1, CaseId = 1, Money = 0 },
                new Client(){Id = 2, CaseUserId = 3, CaseId = 2, Money = 0 },
                new Client(){Id = 3, CaseUserId = 5, CaseId = 3, Money = 0 },
            };
            modelBuilder.Entity<Client>().HasData(clients);
        }
    }
}
