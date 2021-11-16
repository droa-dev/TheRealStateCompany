using Properties.Domain;
using System;
using System.ComponentModel.DataAnnotations;

namespace Properties.WebApi.ViewModels
{
    public sealed class PropertyModel
    {
        public PropertyModel(Property property)
        {
            this.PropertyId = property.PropertyGuid.Id;
            this.Name = property.Name.TextName;
            this.Address = property.Address.TextAddress;
            this.Price = property.Price.Amount;
            this.CodeInternal = property.CodeInternal;
            this.Year = property.Year;
            this.StateAbbr = property.CountryStateAbb.TextAbbreviation;
        }

        [Required]
        public Guid PropertyId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string CodeInternal { get; set; }
        [Required]
        public string Year { get; set; }
        [Required]
        public string StateAbbr { get; set; }
    }
}
