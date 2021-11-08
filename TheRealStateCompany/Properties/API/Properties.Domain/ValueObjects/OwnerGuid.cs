using System;

namespace Properties.Domain.ValueObjects
{
    public readonly struct OwnerGuid : IEquatable<OwnerGuid>
    {
        public Guid Id { get; }
        public OwnerGuid(Guid id) => this.Id = id;
        public override bool Equals(object? obj) =>
           obj is OwnerGuid o && this.Equals(o);
        public bool Equals(OwnerGuid other) => this.Id == other.Id;
        public override int GetHashCode() => HashCode.Combine(this.Id);
        public static bool operator ==(OwnerGuid left, OwnerGuid right) => left.Equals(right);
        public static bool operator !=(OwnerGuid left, OwnerGuid right) => !(left == right);
        public override string ToString() => this.Id.ToString();
    }
}
