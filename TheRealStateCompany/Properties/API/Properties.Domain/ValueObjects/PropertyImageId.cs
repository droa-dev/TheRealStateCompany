using System;

namespace Properties.Domain.ValueObjects
{
    public readonly struct PropertyImageId : IEquatable<PropertyImageId>
    {
        public int Id { get; }
        public PropertyImageId(int id) => this.Id = id;

        public override bool Equals(object? obj) =>
           obj is PropertyImageId o && this.Equals(o);
        public bool Equals(PropertyImageId other) => this.Id == other.Id;
        public override int GetHashCode() => HashCode.Combine(this.Id);
        public static bool operator ==(PropertyImageId left, PropertyImageId right) => left.Equals(right);
        public static bool operator !=(PropertyImageId left, PropertyImageId right) => !(left == right);

        public override string ToString() => this.Id.ToString();
    }
}
