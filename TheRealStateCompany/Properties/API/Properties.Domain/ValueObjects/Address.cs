using System;

namespace Properties.Domain.ValueObjects
{
    public readonly struct Address : IEquatable<Address>
    {        
        public Address(string textAddress)
        {
            if (string.IsNullOrWhiteSpace(textAddress))
                TextAddress = string.Empty;

            TextAddress = textAddress;
        }
        public string TextAddress { get; }
        public override bool Equals(object? obj) =>
           obj is Name o && this.Equals(o);

        public bool Equals(Address other) => this.TextAddress == other.TextAddress;

        public override string ToString() => TextAddress;

        public static bool operator ==(Address left, Address right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Address left, Address right)
        {
            return !(left == right);
        }
        public override int GetHashCode() => HashCode.Combine(this.TextAddress);
    }
}
