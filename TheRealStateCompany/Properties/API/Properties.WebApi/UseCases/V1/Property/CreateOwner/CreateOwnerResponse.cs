using System.ComponentModel.DataAnnotations;

namespace Properties.WebApi.UseCases.V1.Property.CreateOwner
{
    /// <summary>
    ///     Create Owner Response.
    /// </summary>
    public sealed class CreateOwnerResponse
    {
        /// <summary>
        ///     CreateOwnerResponse Constructor.
        /// </summary>
        public CreateOwnerResponse(ViewModels.OwnerModel ownerModel) => this.Owner = ownerModel;

        /// <summary>
        ///     Gets Owner.
        /// </summary>
        [Required]
        public ViewModels.OwnerModel Owner { get; }
    }
}
