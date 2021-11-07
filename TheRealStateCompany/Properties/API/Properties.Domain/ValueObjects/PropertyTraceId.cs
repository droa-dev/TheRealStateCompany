using System;

namespace Properties.Domain.ValueObjects
{
    public readonly struct PropertyTraceId : IEquatable<PropertyTraceId>
    {
        public int Id { get; }
        public PropertyTraceId(int id) => this.Id = id;

        public override bool Equals(object? obj) =>
           obj is PropertyTraceId o && this.Equals(o);
        public bool Equals(PropertyTraceId other) => this.Id == other.Id;
        public override int GetHashCode() => HashCode.Combine(this.Id);
        public static bool operator ==(PropertyTraceId left, PropertyTraceId right) => left.Equals(right);
        public static bool operator !=(PropertyTraceId left, PropertyTraceId right) => !(left == right);

        public override string ToString() => this.Id.ToString();
    }
}
