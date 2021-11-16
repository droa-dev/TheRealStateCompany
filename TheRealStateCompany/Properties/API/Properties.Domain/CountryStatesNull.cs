using Properties.Domain.ValueObjects;

namespace Properties.Domain
{
    public sealed class CountryStatesNull : ICountryStates
    {
        public static CountryStatesNull Instance { get; } = new CountryStatesNull();
        public CountryStatesId CountryStatesId => new CountryStatesId(0);
        public Name Name => new Name(string.Empty);
        public Abbreviation Abbrev => new Abbreviation(string.Empty);
    }
}
