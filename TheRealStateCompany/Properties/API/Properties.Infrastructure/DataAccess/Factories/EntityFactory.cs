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
            PropertyGuid propertyGuid, Name name, Address address, Money price, string codeInternal, 
            string year, OwnerGuid ownerguid, CountryStatesId countryStatesId)
            => new Property(propertyGuid, name, address, price, codeInternal, year, ownerguid, countryStatesId);

        /// <inheritdoc />
        public PropertyFilters ListProperty(Identification? ownerIdentification, Abbreviation? countryStateAbb,
            Money? initialPrice, Money? maxPrice, string year, string codeInternal)
            => new PropertyFilters(ownerIdentification, countryStateAbb, initialPrice, maxPrice, year, codeInternal);

        /// <inheritdoc />
        public PropertyTrace NewPropertyTrace(Name name, Money value, Money tax, PropertyGuid propertyGuid)
            => new PropertyTrace(new PropertyTraceGuid(Guid.NewGuid()), DateTime.Now, name, value, tax, propertyGuid);        
    }
}
