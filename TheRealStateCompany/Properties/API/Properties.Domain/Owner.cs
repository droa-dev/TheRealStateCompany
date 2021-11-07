using Properties.Domain.ValueObjects;
using System;

namespace Properties.Domain
{
    public class Owner
    {
        public OwnerId OwnerId { get; set; }
        public Identification IdentificationNumber { get; set; }
        public Name Name { get; set; }
        public Address Address { get; set; }
        public File Photo { get; set; }
        public DateTime Birthday { get; set; }
    }
}
