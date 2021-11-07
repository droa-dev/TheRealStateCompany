using System;

namespace Properties.Domain.ValueObjects
{
    public readonly struct Enabled : IEquatable<Enabled>
    {
        public bool IsEnabled { get; }

        public Enabled(bool isEnabled) =>
            this.IsEnabled = isEnabled;

        public override bool Equals(object? obj) =>
            obj is Enabled o && this.Equals(o);

        public bool Equals(Enabled other) =>
            this.IsEnabled == other.IsEnabled;

        public override int GetHashCode() =>
            HashCode.Combine(this.IsEnabled);

        public static bool operator ==(Enabled left, Enabled right) => left.Equals(right);

        public static bool operator !=(Enabled left, Enabled right) => !(left == right);
    }
}
