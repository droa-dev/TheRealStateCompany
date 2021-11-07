using Properties.Domain.ValueObjects;

namespace Properties.Domain
{
    public class CountryStates
    {
        public CountryStatesId CountryStatesId { get; set; }
        public Name Name { get; set; }
        public Abbreviation Abbrev { get; set; }
    }
}
