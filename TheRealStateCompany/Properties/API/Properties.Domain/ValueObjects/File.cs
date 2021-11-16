using System;

namespace Properties.Domain.ValueObjects
{
    public readonly struct File : IEquatable<File>
    {
        public byte[] FileBinary { get; }

        public File(byte[] filebinary) =>
            this.FileBinary = filebinary;

        public override bool Equals(object? obj) =>
           obj is File o && this.Equals(o);

        public bool Equals(File other) =>
            this.FileBinary == other.FileBinary;

        public override int GetHashCode() =>
            HashCode.Combine(this.FileBinary);

        public static bool operator ==(File left, File right) => left.Equals(right);

        public static bool operator !=(File left, File right) => !(left == right);
    }
}
