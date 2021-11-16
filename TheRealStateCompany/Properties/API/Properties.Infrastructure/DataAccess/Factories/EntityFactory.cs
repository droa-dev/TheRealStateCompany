using Properties.Domain;
using Properties.Domain.Factories;
using Properties.Domain.ValueObjects;
using System;

namespace Properties.Infrastructure.DataAccess.Factories
{
    public sealed class EntityFactory : IPropertyFactory, IOwnerFactory, IPropertyTraceFactory
    {
        /// <inheritdoc />
        public Property NewProperty(Name name, Address address, Money price, string codeInternal, string year, OwnerGuid ownerGuid, CountryStatesId countryStatesId)
            => new Property(new PropertyGuid(Guid.NewGuid()), name, address, price, codeInternal, year, ownerGuid, countryStatesId);

        /// <inheritdoc />
        public Owner NewOwner(Identification identification, Name name, Address address, File? photo, DateTime? birthday)
            => new Owner(new OwnerGuid(Guid.NewGuid()), identification, name, address, photo, birthday);

        /// <inheritdoc />
        public PropertyImage NewPropertyImage(Name fileName, File file, PropertyGuid propertyGuid)
            => new PropertyImage(new PropertyImageGuid(Guid.NewGuid()), fileName, file, new Enabled(true), propertyGuid);

        /// <inheritdoc />
        public Property UpdateProperty(
            PropertyGuid propertyGuid, Name? name, Address? address, Money? price, string? codeInternal,
            string? year, OwnerGuid? ownerguid, CountryStatesId? countryStatesId)
            => new Property(propertyGuid, new Name(name.HasValue ? name.Value.TextName : string.Empty),
                new Address(address.HasValue ? address.Value.TextAddress : string.Empty),
                new Money(price.HasValue ? price.Value.Amount : decimal.Zero), codeInternal ?? string.Empty, year ?? string.Empty,
                new OwnerGuid(ownerguid.HasValue ? ownerguid.Value.Id : Guid.Empty),
                new CountryStatesId(countryStatesId.HasValue ? countryStatesId.Value.Id : 0));

        /// <inheritdoc />
        public PropertyFilters ListProperty(OwnerGuid? ownerGuid, CountryStatesId? countryStateId,
            Money? initialPrice, Money? maxPrice, string year, string codeInternal)
            => new PropertyFilters(ownerGuid, countryStateId, initialPrice, maxPrice, year, codeInternal);

        /// <inheritdoc />
        public PropertyTrace NewPropertyTrace(Name name, Money value, Money tax, PropertyGuid propertyGuid)
            => new PropertyTrace(new PropertyTraceGuid(Guid.NewGuid()), DateTime.Now, name, value, tax, propertyGuid);
    }
}
