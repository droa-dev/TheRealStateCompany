using Properties.Domain.ValueObjects;
using System;

namespace Properties.Domain
{
    public class PropertyTrace
    {
        public PropertyTrace(PropertyTraceGuid propertyTraceGuid, DateTime dateSale, Name name, Money value, Money tax, PropertyGuid propertyGuid) 
        {
            this.PropertyTraceGuid = propertyTraceGuid;
            this.DateSale = dateSale;
            this.Name = name;
            this.Value = value;
            this.Tax = tax;
            this.PropertyGuid = propertyGuid;
        }
        public PropertyTraceGuid PropertyTraceGuid { get; set; }
        public PropertyTraceId PropertyTraceId { get; }
        public DateTime DateSale { get; }
        public Name Name { get; }
        public Money Value { get; }
        public Money Tax { get; }
        public PropertyId PropertyId { get; }
        public PropertyGuid PropertyGuid { get; }
        public Property? Property { get; set; }
    }
}
