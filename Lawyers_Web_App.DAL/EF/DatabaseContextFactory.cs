using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lawyers_Web_App.DAL.EF
{
    public class DatabaseContextFactory:IDesignTimeDbContextFactory<LowyersContext>
    {
        public LowyersContext CreateDbContext(string[] args)
        {
            AppConfiguration appConfig = new AppConfiguration();
            var opsBuilder = new DbContextOptionsBuilder<LowyersContext>();
            opsBuilder.UseSqlServer(appConfig.sqlConnectionString);
            return new LowyersContext(opsBuilder.Options);
        }
    }
}
