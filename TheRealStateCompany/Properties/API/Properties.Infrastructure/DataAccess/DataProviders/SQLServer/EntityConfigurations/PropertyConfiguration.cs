using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Properties.Domain;
using Properties.Domain.ValueObjects;
using System;

namespace Properties.Infrastructure.DataAccess.DataProviders.SQLServer.EntityConfiguratios
{
    public sealed class PropertyConfiguration : IEntityTypeConfiguration<Property>
    {
        /// <summary>
        ///     Configure Account.
        /// </summary>
        /// <param name="builder">Builder.</param>
        public void Configure(EntityTypeBuilder<Property> builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.ToTable("Property", "dbo");

            builder.HasKey(b => b.PropertyGuid);

            builder.Property(b => b.PropertyId)
                .HasConversion(
                    v => v.Id,
                    v => new PropertyId(v))
                .IsRequired()
                .UseIdentityColumn();

            builder.Property(b => b.PropertyGuid)
                .HasConversion(
                    value => value.Id,
                    value => new PropertyGuid(value))
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

            builder.Property(b => b.Price)
                .HasConversion(
                    value => value.Amount,
                    value => new Money(value))
                .IsRequired();

            builder.Property(b => b.CodeInternal)
                .IsRequired();

            builder.Property(b => b.Year)
                .IsRequired();

            builder.Property(b => b.OwnerGuid)
                .HasConversion(
                    value => value.Id,
                    value => new OwnerGuid(value))
                .IsRequired();

            builder.Property(b => b.CountryStatesId)
                .HasConversion(
                    value => value.Id,
                    value => new CountryStatesId(value))
                .IsRequired();

            builder.HasOne(x => x.CountryStates)
               .WithOne(b => b.Property!)
               .HasForeignKey<Property>(b => b.CountryStatesId);

            builder.HasMany(x => x.PropertyTraceCollection)
                .WithOne(b => b.Property!)
                .HasForeignKey(b => b.PropertyGuid)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
