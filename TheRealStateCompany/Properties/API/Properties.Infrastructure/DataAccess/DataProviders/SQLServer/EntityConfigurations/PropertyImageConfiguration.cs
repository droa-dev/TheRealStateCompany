using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Properties.Domain;
using Properties.Domain.ValueObjects;
using System;

namespace Properties.Infrastructure.DataAccess.DataProviders.SQLServer.EntityConfiguratios
{
    public sealed class PropertyImageConfiguration : IEntityTypeConfiguration<PropertyImage>
    {
        /// <summary>
        ///     Configure PropertyImage.
        /// </summary>
        /// <param name="builder">Builder.</param>
        public void Configure(EntityTypeBuilder<PropertyImage> builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.ToTable("PropertyImage", "dbo");

            builder.HasKey(b => b.PropertyImageGuid);

            builder.Property(b => b.PropertyImageId)
                .HasConversion(
                    v => v.Id,
                    v => new PropertyImageId(v))
                .IsRequired()
                .UseIdentityColumn();

            builder.Property(b => b.PropertyImageGuid)
                .HasConversion(
                    value => value.Id,
                    value => new PropertyImageGuid(value))
                .IsRequired();

            builder.Property(b => b.FileName)
                .HasConversion(
                    value => value.TextName,
                    value => new Name(value))
                .IsRequired();

            builder.Property(b => b.File)
                .HasConversion(
                    value => value.FileBinary,
                    value => new File(value));

            builder.Property(b => b.Enabled)
                .HasConversion(
                    value => value.IsEnabled,
                    value => new Enabled(value))
                .IsRequired();

            builder.HasOne(x => x.Property)
               .WithOne(b => b.PropertyImage!)
               .HasForeignKey<PropertyImage>(b => b.PropertyGuid)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
