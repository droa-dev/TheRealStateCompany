using System.ComponentModel.DataAnnotations;

namespace Properties.WebApi.UseCases.V1.Property.ChangePropertyPrice
{
    /// <summary>
    ///     Change Property Price Response.
    /// </summary>
    public sealed class ChangePropertyPriceResponse
    {
        /// <summary>
        ///     Change Property Price Constructor.
        /// </summary>
        public ChangePropertyPriceResponse(ViewModels.PropertyModel propertyModel) => this.Property = propertyModel;

        /// <summary>
        ///     Gets Response.
        /// </summary>
        [Required]
        public ViewModels.PropertyModel Property { get; }
    }
}
