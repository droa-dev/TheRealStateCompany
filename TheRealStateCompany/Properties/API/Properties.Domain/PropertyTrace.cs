using Properties.Domain.ValueObjects;
using System;

namespace Properties.Domain
{
    public class PropertyTrace
    {
        public PropertyTraceId PropertyTraceId { get; }
        public DateTime DateSale { get; }
        public Name Name { get; }
        public Money Value { get; }
        public Money Tax { get; }
        public PropertyId PropertyId { get; }
        public Property? Property { get; set; }
    }
}
