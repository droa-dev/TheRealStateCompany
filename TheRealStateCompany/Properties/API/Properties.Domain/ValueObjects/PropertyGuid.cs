using System;

namespace Properties.Domain.ValueObjects
{
    /// <summary>
    ///     PropertyId
    ///     <see>    
    ///     </see>    
    /// </summary>
    public readonly struct PropertyGuid : IEquatable<PropertyGuid>
    {
        public Guid Id { get; }
        public PropertyGuid(Guid id) => this.Id = id;

        public override bool Equals(object? obj) =>
           obj is PropertyGuid o && this.Equals(o);
        public bool Equals(PropertyGuid other) => this.Id == other.Id;
        public override int GetHashCode() => HashCode.Combine(this.Id);
        public static bool operator ==(PropertyGuid left, PropertyGuid right) => left.Equals(right);
        public static bool operator !=(PropertyGuid left, PropertyGuid right) => !(left == right);

        public override string ToString() => this.Id.ToString();
    }
}
