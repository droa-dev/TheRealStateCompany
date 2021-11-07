using System;

namespace Properties.Domain.ValueObjects
{
    public readonly struct Name : IEquatable<Name>
    {
        private readonly string _textName;
        public Name(string textName)
        {
            if (string.IsNullOrWhiteSpace(textName))
                _textName = string.Empty;

            _textName = textName;
        }
        public override bool Equals(object? obj) =>
           obj is Name o && this.Equals(o);

        public bool Equals(Name other) => this._textName == other._textName;

        public override string ToString() => _textName;

        public static bool operator ==(Name left, Name right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Name left, Name right)
        {
            return !(left == right);
        }
        public override int GetHashCode() => HashCode.Combine(this._textName);
    }
}
