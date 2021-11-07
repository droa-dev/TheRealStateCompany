using System;

namespace Properties.Domain.ValueObjects
{
    public readonly struct OwnerId : IEquatable<OwnerId>
    {
        public int Id { get; }
        public OwnerId(int id) => this.Id = id;

        public override bool Equals(object? obj) =>
           obj is OwnerId o && this.Equals(o);
        public bool Equals(OwnerId other) => this.Id == other.Id;
        public override int GetHashCode() => HashCode.Combine(this.Id);
        public static bool operator ==(OwnerId left, OwnerId right) => left.Equals(right);
        public static bool operator !=(OwnerId left, OwnerId right) => !(left == right);

        public override string ToString() => this.Id.ToString();
    }
}
