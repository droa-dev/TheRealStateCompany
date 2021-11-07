using Properties.Domain.ValueObjects;

namespace Properties.Domain
{
    public class Property : IProperty
    {
        public PropertyId PropertyId { get; }
        public PropertyGuid PropertyGuid { get; }
        public Name Name { get; }
        public Address Address { get; }
        public Money Price { get; }
        public string CodeInternal { get; set; }
        public string Year { get; set; }
        public OwnerId OwnerId { get; }
        public Owner? Owner { get; set; }
        public CountryStatesId CountryStatesId { get; }
        public CountryStates? CountryStates { get; set; }
    }
}
