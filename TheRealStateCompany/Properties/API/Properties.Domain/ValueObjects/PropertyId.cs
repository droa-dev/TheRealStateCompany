using System;

namespace Properties.Domain.ValueObjects
{
    public readonly struct PropertyId : IEquatable<PropertyId>
    {
        public int Id { get; }
        public PropertyId(int id) => this.Id = id;

        public override bool Equals(object? obj) =>
           obj is PropertyId o && this.Equals(o);
        public bool Equals(PropertyId other) => this.Id == other.Id;
        public override int GetHashCode() => HashCode.Combine(this.Id);
        public static bool operator ==(PropertyId left, PropertyId right) => left.Equals(right);
        public static bool operator !=(PropertyId left, PropertyId right) => !(left == right);

        public override string ToString() => this.Id.ToString();
    }
}
