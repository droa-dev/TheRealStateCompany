using System.ComponentModel.DataAnnotations;

namespace Properties.WebApi.UseCases.V1.Property.CreateProperty
{
    /// <summary>
    ///     Create Property Response.
    /// </summary>
    public sealed class CreatePropertyResponse
    {
        /// <summary>
        ///     CreatePropertyResponse Constructor.
        /// </summary>
        public CreatePropertyResponse(ViewModels.PropertyModel propertyModel) => this.Property = propertyModel;

        /// <summary>
        ///     Gets Property.
        /// </summary>
        [Required]
        public ViewModels.PropertyModel Property { get; }
    }
}
