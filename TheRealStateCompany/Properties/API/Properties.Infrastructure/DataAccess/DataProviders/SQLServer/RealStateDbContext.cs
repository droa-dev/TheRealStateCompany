using Microsoft.EntityFrameworkCore;
using Properties.Domain;
using System;

namespace Properties.Infrastructure.DataAccess.DataProviders.SQLServer
{
    public sealed class RealStateDbContext : DbContext
    {
        public RealStateDbContext(DbContextOptions options)
            : base(options)
        {
        }

        /// <summary>
        ///     Gets or sets Properties
        /// </summary>
        public DbSet<Property> Properties { get; set; }

        /// <summary>
        ///     Gets or sets Owners
        /// </summary>
        public DbSet<Owner> Owners { get; set; }

        /// <summary>
        ///     Gets or sets PropertyImages
        /// </summary>
        public DbSet<PropertyImage> PropertyImages { get; set; }

        /// <summary>
        ///     Gets or sets PropertyTraces
        /// </summary>
        public DbSet<PropertyTrace> PropertyTraces { get; set; }

        /// <summary>
        ///     Gets or sets CountryStates
        /// </summary>
        public DbSet<CountryStates> CountryStates { get; set; }

        /// <summary>
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder is null)
            {
                throw new ArgumentNullException(nameof(modelBuilder));
            }

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RealStateDbContext).Assembly);
            //SeedData.Seed(modelBuilder);
        }
    }
}
