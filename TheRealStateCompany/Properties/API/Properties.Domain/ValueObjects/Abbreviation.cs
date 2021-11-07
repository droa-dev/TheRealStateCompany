using System;

namespace Properties.Domain.ValueObjects
{
    public readonly struct Abbreviation : IEquatable<Abbreviation>
    {
        private readonly string _textAbbreviation;
        public Abbreviation(string textAbbreviation)
        {
            if (string.IsNullOrWhiteSpace(textAbbreviation))
                _textAbbreviation = string.Empty;

            _textAbbreviation = textAbbreviation;
        }
        public override bool Equals(object? obj) =>
           obj is Name o && this.Equals(o);

        public bool Equals(Abbreviation other) => this._textAbbreviation == other._textAbbreviation;

        public override string ToString() => _textAbbreviation;

        public static bool operator ==(Abbreviation left, Abbreviation right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Abbreviation left, Abbreviation right)
        {
            return !(left == right);
        }
        public override int GetHashCode() => HashCode.Combine(this._textAbbreviation);
    }
}
