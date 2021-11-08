using System;

namespace Properties.Domain.ValueObjects
{
    public readonly struct PropertyImageGuid : IEquatable<PropertyImageGuid>
    {
        public Guid Id { get; }
        public PropertyImageGuid(Guid id) => this.Id = id;

        public override bool Equals(object? obj) =>
           obj is PropertyImageGuid o && this.Equals(o);
        public bool Equals(PropertyImageGuid other) => this.Id == other.Id;
        public override int GetHashCode() => HashCode.Combine(this.Id);
        public static bool operator ==(PropertyImageGuid left, PropertyImageGuid right) => left.Equals(right);
        public static bool operator !=(PropertyImageGuid left, PropertyImageGuid right) => !(left == right);

        public override string ToString() => this.Id.ToString();
    }
}
