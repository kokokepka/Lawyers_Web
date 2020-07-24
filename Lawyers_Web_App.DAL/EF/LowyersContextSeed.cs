using Lawyers_Web_App.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Lawyers_Web_App.DAL.EF
{
    public class LowyersContextSeed
    {
        public static async Task SeedAsync(LowyersContext lowyerContext)
        {
            //try
            //{
            //    // TODO: Only run this if using a real database
            //    // aspnetrunContext.Database.Migrate();
            //    // aspnetrunContext.Database.EnsureCreated();

            //    //if (!lowyerContext.Users.Any() && !lowyerContext.Documents.Any())
            //    //{
            //    //    IList<User> users = new List<User>()
            //    //    {
            //    //        new User { Name="User1", LastName = "Last1", Patronymic = "Patronymic1", Address = "Address1"},
            //    //        new User { Name="User2", LastName = "Last2", Patronymic = "Patronymic1", Address = "Address1"},
            //    //        new User { Name="User3", LastName = "Last3", Patronymic = "Patronymic1", Address = "Address1"}
            //    //    };
            //    //    lowyerContext.Users.AddRange(users);
            //    //    await lowyerContext.SaveChangesAsync();

            //    //    IList<Document> documents = new List<Document>()
            //    //    {
            //    //        new Document { Name="Doc1", Path = "//", User = users[0], Date = DateTime.Now.Date },
            //    //        new Document { Name="Doc2", Path = "//", User = users[1], Date = DateTime.Now.Date },
            //    //        new Document { Name="Doc3", Path = "//", User = users[2], Date = DateTime.Now.Date }
            //    //    };
            //    //    lowyerContext.Documents.AddRange(documents);
            //    //    await lowyerContext.SaveChangesAsync();
            //    }
            //}
            ////catch (Exception exception)
            ////{
            ////    throw new Exception(exception.Message+", there is problem in LowyersContextSeed.");
            ////}
        }
    }
}
