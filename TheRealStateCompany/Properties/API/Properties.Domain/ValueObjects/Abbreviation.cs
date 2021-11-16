using System;

namespace Properties.Domain.ValueObjects
{
    public readonly struct Abbreviation : IEquatable<Abbreviation>
    {
        public Abbreviation(string textAbbreviation)
        {
            if (string.IsNullOrWhiteSpace(textAbbreviation))
                TextAbbreviation = string.Empty;

            TextAbbreviation = textAbbreviation;
        }

        public string TextAbbreviation { get; }
        public override bool Equals(object? obj) =>
           obj is Name o && this.Equals(o);

        public bool Equals(Abbreviation other) => this.TextAbbreviation == other.TextAbbreviation;

        public override string ToString() => TextAbbreviation;

        public static bool operator ==(Abbreviation left, Abbreviation right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Abbreviation left, Abbreviation right)
        {
            return !(left == right);
        }
        public override int GetHashCode() => HashCode.Combine(this.TextAbbreviation);
    }
}
