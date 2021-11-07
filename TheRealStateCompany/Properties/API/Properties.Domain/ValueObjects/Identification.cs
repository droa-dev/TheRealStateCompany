using System;

namespace Properties.Domain.ValueObjects
{
    public readonly struct Identification : IEquatable<Identification>
    {
        public decimal IdNumber { get; }

        public Identification(decimal id) =>
            this.IdNumber = id;

        public override bool Equals(object? obj) =>
            obj is Identification o && this.Equals(o);

        public bool Equals(Identification other) =>
            this.IdNumber == other.IdNumber;

        public override int GetHashCode() =>
            HashCode.Combine(this.IdNumber);

        public static bool operator ==(Identification left, Identification right) => left.Equals(right);

        public static bool operator !=(Identification left, Identification right) => !(left == right);

        public bool IsZero() => this.IdNumber == 0;

        public override string ToString() => string.Format($"{this.IdNumber:N0}");
    }
}
