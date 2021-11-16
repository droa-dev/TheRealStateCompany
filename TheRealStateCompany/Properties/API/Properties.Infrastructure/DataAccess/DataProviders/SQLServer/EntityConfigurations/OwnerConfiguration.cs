using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Properties.Domain;
using Properties.Domain.ValueObjects;
using System;

namespace Properties.Infrastructure.DataAccess.DataProviders.SQLServer.EntityConfiguratios
{
    public sealed class OwnerConfiguration : IEntityTypeConfiguration<Owner>
    {
        /// <summary>
        ///     Configure Owner.
        /// </summary>
        /// <param name="builder">Builder.</param>
        public void Configure(EntityTypeBuilder<Owner> builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.ToTable("Owner", "dbo");

            builder.HasKey(b => b.OwnerGuid);

            builder.Property(b => b.OwnerGuid)
                .HasConversion(
                    value => value.Id,
                    value => new OwnerGuid(value))
                .IsRequired();

            builder.Property(b => b.IdentificationNumber)
                .HasConversion(
                    value => value.IdNumber,
                    value => new Identification(value))
                .IsRequired();

            builder.Property(b => b.Name)
                .HasConversion(
                    value => value.TextName,
                    value => new Name(value))
                .IsRequired();

            builder.Property(b => b.Address)
                .HasConversion(
                    value => value.TextAddress,
                    value => new Address(value))
                .IsRequired();

            builder.Property(b => b.Photo)
                .HasConversion(
                    value => value.HasValue ? value.Value.FileBinary : Array.Empty<byte>(),
                    value => new File(value));

            builder.Property(b => b.CreatedDate)
                .HasDefaultValueSql("getdate()")
                .IsRequired();

        }
    }
}
