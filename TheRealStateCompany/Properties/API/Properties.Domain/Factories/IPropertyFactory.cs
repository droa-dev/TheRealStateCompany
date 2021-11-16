using Properties.Domain.ValueObjects;

namespace Properties.Domain.Factories
{
    public interface IPropertyFactory
    {
        /// <summary>
        ///     Creates a new Property instance.
        /// </summary>        
        /// <param name="name">name</param>
        /// <param name="address">address</param>
        /// <param name="price">price</param>
        /// <param name="codeInternal">code internal</param>
        /// <param name="year">year</param>        
        /// <param name="ownerGuid">ownerGuid</param>      
        /// <param name="countryStatesId">country state relation</param>
        /// <returns>New Property instance.</returns>
        Property NewProperty(
            Name name, Address address, Money price, string codeInternal, string year,
            OwnerGuid ownerGuid, CountryStatesId countryStatesId);

        /// <summary>
        ///     Updates a Property instance.
        /// </summary>
        /// <param name="propertyGuid">propertyGuid</param>
        /// <param name="name">name</param>
        /// <param name="address">address</param>
        /// <param name="price">price</param>
        /// <param name="codeInternal">code internal</param>
        /// <param name="year">year</param>
        /// <param name="ownerGuid">owner id relation</param>
        /// <param name="countryStatesId">country state relation</param>
        /// <returns>New Property instance.</returns>
        Property UpdateProperty(
            PropertyGuid propertyGuid, Name? name, Address? address, Money? price, string? codeInternal,
            string? year, OwnerGuid? ownerGuid, CountryStatesId? countryStatesId);

        /// <summary>
        ///     Creates a new PropertyFilters instance.
        /// </summary>
        /// <param name="ownerGuid">ownerGuid</param>
        /// <param name="countryStateId">countryStateId</param>
        /// <param name="initialPrice">initialPrice</param>
        /// <param name="maxPrice">maxPrice</param>
        /// <param name="year">year</param>
        /// <param name="codeInternal">code internal</param>
        /// <returns>New List Property instance.</returns>
        PropertyFilters ListProperty(
            OwnerGuid? ownerGuid, CountryStatesId? countryStateId, Money? initialPrice,
            Money? maxPrice, string year, string codeInternal);

        /// <summary>
        ///     Creates a new PropertyImage instance.
        /// </summary>
        /// <param name="fileName">fileName</param>
        /// <param name="file">file</param>
        /// /// <param name="propertyGuid">propertyId relation</param>       
        /// <returns>New PropertyImage instance.</returns>
        PropertyImage NewPropertyImage(Name fileName, File file, PropertyGuid propertyGuid);
    }
}
