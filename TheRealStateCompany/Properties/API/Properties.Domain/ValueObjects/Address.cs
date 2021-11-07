using System;

namespace Properties.Domain.ValueObjects
{
    public readonly struct Address : IEquatable<Address>
    {
        private readonly string _textAddress;
        public Address(string textAddress)
        {
            if (string.IsNullOrWhiteSpace(textAddress))
                _textAddress = string.Empty;

            _textAddress = textAddress;
        }
        public override bool Equals(object? obj) =>
           obj is Name o && this.Equals(o);

        public bool Equals(Address other) => this._textAddress == other._textAddress;

        public override string ToString() => _textAddress;

        public static bool operator ==(Address left, Address right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Address left, Address right)
        {
            return !(left == right);
        }
        public override int GetHashCode() => HashCode.Combine(this._textAddress);
    }
}
