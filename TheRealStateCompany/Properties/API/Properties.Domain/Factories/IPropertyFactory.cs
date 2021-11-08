using Properties.Domain.ValueObjects;

namespace Properties.Domain.Factories
{
    public interface IPropertyFactory
    {
        /// <summary>
        ///     Creates a new Property instance.
        /// </summary>
        /// <param name="propertyGuid">propertyGuid</param>
        /// <param name="name">name</param>
        /// <param name="address">address</param>
        /// <param name="price">price</param>
        /// <param name="codeInternal">code internal</param>
        /// <param name="year">year</param>
        /// <param name="ownerId">owner id relation</param>
        /// <param name="countryStatesId">country state relation</param>
        /// <returns>New Property instance.</returns>
        Property NewProperty(Name name, Address address, Money price, string codeInternal, string year, OwnerId ownerId, CountryStatesId countryStatesId);

        /// <summary>
        ///     Creates a new PropertyImage instance.
        /// </summary>
        /// <param name="fileName">fileName</param>
        /// <param name="file">file</param>
        /// /// <param name="propertyId">propertyId relation</param>       
        /// <returns>New PropertyImage instance.</returns>
        PropertyImage NewPropertyImage(Name fileName, File file, PropertyId propertyId);
    }
}
