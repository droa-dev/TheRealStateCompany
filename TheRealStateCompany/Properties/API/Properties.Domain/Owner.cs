using Properties.Domain.ValueObjects;
using System;

namespace Properties.Domain
{
    public class Owner : IOwner
    {
        public Owner(OwnerGuid ownerGuid, Identification identificationNumber, Name name, Address address, File? photo, DateTime? birthday)
        {
            this.OwnerGuid = ownerGuid;
            this.IdentificationNumber = identificationNumber;
            this.Name = name;
            this.Address = address;
            this.Photo = photo;
            this.Birthday = birthday;
        }
        //public OwnerId OwnerId { get; }
        public OwnerGuid OwnerGuid { get; }
        public Identification IdentificationNumber { get; }
        public Name Name { get; }
        public Address Address { get; }
        public File? Photo { get; }
        public DateTime? Birthday { get; set; }
        public DateTime CreatedDate { get; }
    }
}
