using Properties.Domain.ValueObjects;

namespace Properties.Domain
{
    public class PropertyFilters
    {
        public PropertyFilters(OwnerGuid? ownerGuid, CountryStatesId? countryStatesId, Money? initialPrice, Money? maxPrice, string year, string codeInternal)
        {
            this.OwnerGuid = ownerGuid;
            this.CountryStatesId = countryStatesId;
            this.InitialPrice = initialPrice;
            this.MaxPrice = maxPrice;
            this.Year = year;
            this.CodeInternal = codeInternal;
        }
        public OwnerGuid? OwnerGuid { get; }
        public CountryStatesId? CountryStatesId { get; set; }
        public Money? InitialPrice { get; set; }
        public Money? MaxPrice { get; set; }
        public string Year { get; set; }
        public string CodeInternal { get; set; }
    }
}
