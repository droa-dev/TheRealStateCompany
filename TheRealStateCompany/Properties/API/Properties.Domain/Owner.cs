﻿using Properties.Domain.ValueObjects;
using System;

namespace Properties.Domain
{
    public class Owner
    {
        public Owner(OwnerGuid ownerGuid, Identification identification, Name name, Address address, File photo, DateTime birthday) 
        {
            this.OwnerGuid = ownerGuid;
            this.IdentificationNumber = identification;
            this.Name = name;
            this.Address = address;
            this.Photo = photo;
            this.Birthday = birthday;
        }
        public OwnerId OwnerId { get; set; }
        public OwnerGuid OwnerGuid { get; set; }
        public Identification IdentificationNumber { get; set; }
        public Name Name { get; set; }
        public Address Address { get; set; }
        public File Photo { get; set; }
        public DateTime Birthday { get; set; }
    }
}
