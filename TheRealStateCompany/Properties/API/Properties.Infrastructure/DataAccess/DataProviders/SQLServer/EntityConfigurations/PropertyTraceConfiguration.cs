using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Properties.Domain;
using Properties.Domain.ValueObjects;
using System;

namespace Properties.Infrastructure.DataAccess.DataProviders.SQLServer.EntityConfigurations
{
    public sealed class PropertyTraceConfiguration : IEntityTypeConfiguration<PropertyTrace>
    {
        /// <summary>
        ///     Configure PropertyImage.
        /// </summary>
        /// <param name="builder">Builder.</param>
        public void Configure(EntityTypeBuilder<PropertyTrace> builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.ToTable("PropertyTrace", "dbo");

            builder.HasKey(b => b.PropertyTraceGuid);

            builder.Property(b => b.PropertyTraceGuid)
                .HasConversion(
                    v => v.Id,
                    v => new PropertyTraceGuid(v))
                .IsRequired()
                .UseIdentityColumn();

            builder.Property(b => b.PropertyTraceId)
                .HasConversion(
                    value => value.Id,
                    value => new PropertyTraceId(value))
                .IsRequired();

            builder.Property(b => b.DateSale)
                .IsRequired();

            builder.Property(b => b.Name)
                .HasConversion(
                    value => value.TextName,
                    value => new Name(value))
                .IsRequired();

            builder.Property(b => b.Value)
                .HasConversion(
                    value => value.Amount,
                    value => new Money(value));

            builder.Property(b => b.Tax)
                .HasConversion(
                    value => value.Amount,
                    value => new Money(value))
                .IsRequired();

            builder.HasOne(x => x.Property)
               .WithMany(b => b.PropertyTraceCollection!)
               .HasForeignKey(b => b.PropertyGuid)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
