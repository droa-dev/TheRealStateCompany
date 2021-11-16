using Properties.Domain;
using Properties.Domain.ValueObjects;
using System;
using System.Collections.ObjectModel;

namespace Properties.Infrastructure.DataAccess.DataProviders.SQLServer
{
    public sealed class RealStateDbContextFake
    {
        public RealStateDbContextFake()
        {
            Owner owner = new(
                SeedDataTest.DefaultOwnerId,
                new Identification(SeedDataTest.DefaultOwnerIdentification),
                new Name(SeedDataTest.DefaultOwnerName),
                new Address(SeedDataTest.DefaultOwnerAddress),
                new File(Array.Empty<byte>()),
                SeedDataTest.DefaultOwnerBirthday
                );

            Property property = new(
                SeedDataTest.DefaultPropertyId,
                new Name(SeedDataTest.DefaultPropertyName),
                new Address(SeedDataTest.DefaultPropertyAddress),
                new Money(SeedDataTest.DefaultPropertyPrice),
                SeedDataTest.DefaultPropertyCodeInternal,
                SeedDataTest.DefaultPropertyYear,
                SeedDataTest.DefaultOwnerId,
                SeedDataTest.DefaultPropertyCountryStatesId
                );

            PropertyTrace trace = new(
                SeedDataTest.DefaultPropertyTraceId,
                SeedDataTest.DefaultPropertyTraceDateSale,
                new Name(SeedDataTest.DefaultPropertyName),
                new Money(SeedDataTest.DefaultPropertyPrice),
                new Money(SeedDataTest.DefaultPropertyTax),
                SeedDataTest.DefaultPropertyId
                );

            this.Owners.Add(owner);
            this.Properties.Add(property);
            this.PropertyTraces.Add(trace);
        }

        /// <summary>
        ///     Gets or sets Properties.
        /// </summary>
        public Collection<Property> Properties { get; } = new Collection<Property>();

        /// <summary>
        ///     Gets or sets Owners.
        /// </summary>
        public Collection<Owner> Owners { get; } = new Collection<Owner>();

        /// <summary>
        ///     Gets or sets PropertyImages.
        /// </summary>
        public Collection<PropertyImage> PropertyImages { get; } = new Collection<PropertyImage>();

        /// <summary>
        ///     Gets or sets PropertyTraces.
        /// </summary>
        public Collection<PropertyTrace> PropertyTraces { get; } = new Collection<PropertyTrace>();

        /// <summary>
        ///     Gets or sets CountryStates.
        /// </summary>
        public Collection<CountryStates> CountryStates { get; } = new Collection<CountryStates>();
    }
}
