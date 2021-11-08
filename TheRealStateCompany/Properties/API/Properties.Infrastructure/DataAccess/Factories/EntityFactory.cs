using Properties.Domain;
using Properties.Domain.Factories;
using Properties.Domain.ValueObjects;
using System;

namespace Properties.Infrastructure.DataAccess.Factories
{
    public sealed class EntityFactory : IPropertyFactory, IOwnerFactory, IPropertyTraceFactory
    {
        /// <inheritdoc />
        public Property NewProperty(Name name, Address address, Money price, string codeInternal, string year, OwnerId ownerId, CountryStatesId countryStatesId)
            => new Property(new PropertyGuid(Guid.NewGuid()), name, address, price, codeInternal, year, ownerId, countryStatesId);

        /// <inheritdoc />
        public Owner NewOwner(Identification identification, Name name, Address address, File photo, DateTime birthday)
            => new Owner(new OwnerGuid(Guid.NewGuid()), identification, name, address, photo, birthday);

        /// <inheritdoc />
        public PropertyImage NewPropertyImage(Name fileName, File file, PropertyId propertyId)
            => new PropertyImage(new PropertyImageGuid(Guid.NewGuid()), fileName, file, new Enabled(true), propertyId);

        /// <inheritdoc />
        public PropertyTrace NewPropertyTrace(Name name, Money value, Money tax, PropertyGuid propertyGuid)
            => new PropertyTrace(new PropertyTraceGuid(Guid.NewGuid()), DateTime.Now, name, value, tax, propertyGuid);
    }
}
