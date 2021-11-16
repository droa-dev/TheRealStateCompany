using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Properties.Domain;
using Properties.Domain.ValueObjects;
using System;

namespace Properties.Infrastructure.DataAccess.DataProviders.SQLServer.EntityConfiguratios
{
    public sealed class CountryStatesConfiguration : IEntityTypeConfiguration<CountryStates>
    {
        /// <summary>
        ///     Configure CountryStates.
        /// </summary>
        /// <param name="builder">Builder.</param>
        public void Configure(EntityTypeBuilder<CountryStates> builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.ToTable("CountryStates", "dbo");

            builder.HasKey(b => b.CountryStatesId);

            builder.Property(b => b.CountryStatesId)
                .HasConversion(
                    v => v.Id,
                    v => new CountryStatesId(v))
                .IsRequired();
            //.UseIdentityColumn();

            builder.Property(b => b.Name)
                .HasConversion(
                    value => value.TextName,
                    value => new Name(value))
                .IsRequired();

            builder.Property(b => b.Abbrev)
                .HasConversion(
                    value => value.TextAbbreviation,
                    value => new Abbreviation(value))
                .IsRequired();
        }
    }
}
