using Properties.Domain.ValueObjects;
using System;

namespace Properties.Domain
{
    public sealed class PropertyNull : IProperty
    {
        public static PropertyNull Instance { get; } = new PropertyNull();
        public PropertyId PropertyId => new PropertyId(0);
        public PropertyGuid PropertyGuid => new PropertyGuid(Guid.Empty);
        public Name Name => new Name(string.Empty);
        public Address Address => new Address(string.Empty);
        public Money Price => new Money(decimal.Zero);
        public OwnerId OwnerId => new OwnerId(0);
        public CountryStatesId CountryStatesId => new CountryStatesId(0);
    }
}
