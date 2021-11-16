using Properties.Domain.ValueObjects;

namespace Properties.Domain
{
    public interface ICountryStates
    {
        /// <summary>
        ///     Gets CountryStatesId.
        /// </summary>
        CountryStatesId CountryStatesId { get; }
        Abbreviation Abbrev { get; }
    }
}
