using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace samo.Data.FE
{
    public class samoDbContextFactory : IDesignTimeDbContextFactory<samoDbContext>
    {
        public samoDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("samoDb");

            var optionsBuilder = new DbContextOptionsBuilder<samoDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new samoDbContext(optionsBuilder.Options);
        }
    }
}
