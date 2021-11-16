using Properties.Domain.ValueObjects;
using System;

namespace Properties.Domain
{
    public sealed class OwnerNull : IOwner
    {
        public static OwnerNull Instance { get; } = new OwnerNull();
        public OwnerGuid OwnerGuid => new(Guid.Empty);
        public Identification IdentificationNumber => new(0);
        public Name Name => new(string.Empty);
        public Address Address => new(string.Empty);
        public File? Photo => new(Array.Empty<byte>());
        public DateTime? Birthday => new DateTime();
        public DateTime CreatedDate => new DateTime();
    }
}
