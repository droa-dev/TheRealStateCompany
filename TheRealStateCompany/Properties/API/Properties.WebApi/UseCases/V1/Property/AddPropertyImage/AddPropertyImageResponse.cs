using System.ComponentModel.DataAnnotations;

namespace Properties.WebApi.UseCases.V1.Property.AddPropertyImage
{
    /// <summary>
    ///     The response for Add an image for the Property.
    /// </summary>
    public sealed class AddPropertyImageResponse
    {
        /// <summary>
        ///     The Response AddPropertyImage Constructor.
        /// </summary>
        public AddPropertyImageResponse(ViewModels.PropertyImageModel imageModel) => this.PropertyImage = imageModel;

        /// <summary>
        ///     Gets Property Image.
        /// </summary>
        [Required]
        public ViewModels.PropertyImageModel PropertyImage { get; }
    }
}
