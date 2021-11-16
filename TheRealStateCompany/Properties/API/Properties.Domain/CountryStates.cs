using Properties.Domain.ValueObjects;

namespace Properties.Domain
{
    public class CountryStates : ICountryStates
    {
        public CountryStates(CountryStatesId countryStatesId, Name name, Abbreviation abbreviation) 
        {
            this.CountryStatesId = countryStatesId;
            this.Name = name;
            this.Abbrev = abbreviation;
        }
        public CountryStatesId CountryStatesId { get; set; }
        public Name Name { get; set; }
        public Abbreviation Abbrev { get; set; }
        public Property? Property { get; set; }
    }
}
