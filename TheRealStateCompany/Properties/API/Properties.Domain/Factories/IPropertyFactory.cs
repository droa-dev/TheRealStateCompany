﻿using Properties.Domain.ValueObjects;

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
        /// <param name="ownerId">owner id relation</param>
        /// <param name="countryStatesId">country state relation</param>
        /// <returns>New Property instance.</returns>
        Property NewProperty(Name name, Address address, Money price, string codeInternal, string year, OwnerId ownerId, CountryStatesId countryStatesId);

        /// <summary>
        ///     Updates a Property instance.
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
        Property UpdateProperty(PropertyGuid propertyGuid, Name name, Address address, Money price, string codeInternal, string year, OwnerId ownerId, CountryStatesId countryStatesId);

        /// <summary>
        ///     Creates a new PropertyFilters instance.
        /// </summary>
        /// <param name="ownerIdentification">propertyGuid</param>
        /// <param name="countryStateAbb">name</param>
        /// <param name="initialPrice">address</param>
        /// <param name="maxPrice">price</param>
        /// <param name="year">year</param>
        /// <param name="codeInternal">code internal</param>
        /// <returns>New List Property instance.</returns>
        PropertyFilters ListProperty(Identification? ownerIdentification, Abbreviation? countryStateAbb, Money? initialPrice, Money? maxPrice, string year, string codeInternal);

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