using Properties.Domain.Collections;
using Properties.Domain.ValueObjects;

namespace Properties.Domain
{
    public class Property : IProperty
    {
        public Property(PropertyGuid propertyGuid, Name name, Address address, Money price, string codeInternal, string year, OwnerGuid ownerGuid, CountryStatesId countryStatesId) 
        {
            this.PropertyGuid = propertyGuid;
            this.Name = name;
            this.Address = address;
            this.Price = price;
            this.CodeInternal = codeInternal;
            this.Year = year;
            this.OwnerGuid = ownerGuid;
            this.CountryStatesId = countryStatesId;
        }
        public PropertyId PropertyId { get; }
        public PropertyGuid PropertyGuid { get; }
        public Name Name { get; }
        public Address Address { get; }
        public Money Price { get; }
        public string CodeInternal { get; set; }
        public string Year { get; set; }
        public OwnerGuid OwnerGuid { get; set; }
        public Owner? Owner { get; set; }
        public CountryStatesId CountryStatesId { get; }
        public CountryStates CountryStates { get; set; }        
        public PropertyImage? PropertyImage { get; set; }
        public PropertyTraceCollection PropertyTraceCollection { get; } = new PropertyTraceCollection();
    }
}
