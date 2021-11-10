using Properties.Domain.ValueObjects;
using System;

namespace Properties.Domain.Factories
{
    public interface IOwnerFactory
    {
        /// <summary>
        ///     Creates a new Owner instance.
        /// </summary>
        /// <param name="identification">identification number</param>
        /// <param name="name">name</param>
        /// /// <param name="address">address</param>
        /// /// <param name="photo">photo</param>
        /// /// <param name="birthday">birthday</param>       
        /// <returns>New Owner instance.</returns>
        Owner NewOwner(Identification identification, Name name, Address address, File? photo, DateTime? birthday);
    }
}
