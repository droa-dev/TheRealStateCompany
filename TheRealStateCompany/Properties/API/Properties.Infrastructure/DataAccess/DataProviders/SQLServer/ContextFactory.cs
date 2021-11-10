using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Properties.Infrastructure.DataAccess.DataProviders.SQLServer
{
    public sealed class ContextFactory : IDesignTimeDbContextFactory<RealStateDbContext>
    {
        /// <summary>
        ///     Instantiate a RealStateDbContext.
        /// </summary>
        /// <param name="args">Command line args.</param>
        /// <returns>RealState DbContext.</returns>
        public RealStateDbContext CreateDbContext(string[] args)
        {
            string connectionString = ReadDefaultConnectionStringFromAppSettings();

            DbContextOptionsBuilder<RealStateDbContext> builder = new DbContextOptionsBuilder<RealStateDbContext>();
            Console.WriteLine(connectionString);
            builder.UseSqlServer(connectionString);
            builder.EnableSensitiveDataLogging();
            return new RealStateDbContext(builder.Options);
        }

        private static string ReadDefaultConnectionStringFromAppSettings()
        {
            string? envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
                .AddJsonFile("appsettings.json", false)
                .AddJsonFile($"appsettings.{envName}.json", false)
                .AddEnvironmentVariables()
                .Build();

            string connectionString = configuration.GetValue<string>("PersistenceModule:DefaultConnection");
            return connectionString;
        }
    }
}
