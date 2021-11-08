using System;

namespace Properties.Domain.ValueObjects
{
    public readonly struct PropertyTraceGuid : IEquatable<PropertyTraceGuid>
    {
        public Guid Id { get; }
        public PropertyTraceGuid(Guid id) => this.Id = id;

        public override bool Equals(object? obj) =>
           obj is PropertyTraceGuid o && this.Equals(o);
        public bool Equals(PropertyTraceGuid other) => this.Id == other.Id;
        public override int GetHashCode() => HashCode.Combine(this.Id);
        public static bool operator ==(PropertyTraceGuid left, PropertyTraceGuid right) => left.Equals(right);
        public static bool operator !=(PropertyTraceGuid left, PropertyTraceGuid right) => !(left == right);

        public override string ToString() => this.Id.ToString();
    }
}
