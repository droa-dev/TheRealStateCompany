using System;

namespace Properties.Domain.ValueObjects
{
    public readonly struct Name : IEquatable<Name>
    {
        public Name(string textName)
        {
            if (string.IsNullOrWhiteSpace(textName))
                TextName = string.Empty;

            TextName = textName;
        }

        public string TextName { get; }
        public override bool Equals(object? obj) =>
           obj is Name o && this.Equals(o);

        public bool Equals(Name other) => this.TextName == other.TextName;

        public override string ToString() => TextName;

        public static bool operator ==(Name left, Name right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Name left, Name right)
        {
            return !(left == right);
        }
        public override int GetHashCode() => HashCode.Combine(this.TextName);
    }
}
