using Properties.Domain;
using System;
using System.ComponentModel.DataAnnotations;

namespace Properties.WebApi.ViewModels
{
    public class OwnerModel
    {
        public OwnerModel(Owner owner)
        {
            this.IdentificationNumber = owner.IdentificationNumber.IdNumber;
            this.Name = owner.Name.TextName;
            this.Address = owner.Address.TextAddress;
            this.Photo = owner.Photo!.Value.FileBinary ?? null;
            this.Birthday = owner.Birthday ?? null;
        }

        [Required]
        public decimal IdentificationNumber { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        public byte[]? Photo { get; set; }

        public DateTime? Birthday { get; set; }
    }
}
