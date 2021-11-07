using System;

namespace Properties.Domain.ValueObjects
{
    public readonly struct CountryStatesId : IEquatable<CountryStatesId>
    {
        public int Id { get; }
        public CountryStatesId(int id) => this.Id = id;

        public override bool Equals(object? obj) =>
           obj is OwnerId o && this.Equals(o);
        public bool Equals(CountryStatesId other) => this.Id == other.Id;
        public override int GetHashCode() => HashCode.Combine(this.Id);
        public static bool operator ==(CountryStatesId left, CountryStatesId right) => left.Equals(right);
        public static bool operator !=(CountryStatesId left, CountryStatesId right) => !(left == right);

        public override string ToString() => this.Id.ToString();
    }
}
