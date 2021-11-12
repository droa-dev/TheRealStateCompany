using System.ComponentModel.DataAnnotations;

namespace Properties.WebApi.UseCases.V1.Property.UpdateProperty
{
    /// <summary>
    ///     Update Property Response.
    /// </summary>
    public sealed class UpdatePropertyResponse
    {
        /// <summary>
        ///     Update Property Response constructor.
        /// </summary>
        public UpdatePropertyResponse(ViewModels.PropertyModel propertyModel) => this.Property = propertyModel;

        /// <summary>
        ///     Gets Property.
        /// </summary>
        [Required]
        public ViewModels.PropertyModel Property { get; }
    }
}
