using Properties.Domain.ValueObjects;

namespace Properties.Domain
{
    public class PropertyFilters
    {
        public PropertyFilters(Identification? ownerIdentification, Abbreviation? countryStateAbb, Money? initialPrice, Money? maxPrice, string year, string codeInternal)
        {
            this.OwnerIdenttification = ownerIdentification;
            this.StateAbbreviation = countryStateAbb;
            this.InitialPrice = initialPrice;
            this.MaxPrice = maxPrice;
            this.Year = year;
            this.CodeInternal = codeInternal;
        }
        public Identification? OwnerIdenttification { get; }
        public Abbreviation? StateAbbreviation { get; set; }
        public Money? InitialPrice { get; set; }
        public Money? MaxPrice { get; set; }
        public string Year { get; set; }
        public string CodeInternal { get; set; }
    }
}
