using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Properties.WebApi.UseCases.V1.Property.ListProperty
{
    /// <summary>
    ///     List Property Response.
    /// </summary>
    public sealed class ListPropertyResponse
    {

        /// <summary>
        ///     ListPropertyResponse Constructor.
        /// </summary>
        public ListPropertyResponse(IEnumerable<Domain.Property> properties)
        {
            foreach (Domain.Property property in properties)
            {
                ViewModels.PropertyModel propertyModel = new ViewModels.PropertyModel(property);
                this.Properties.Add(propertyModel);
            }
        }

        /// <summary>
        ///     Gets Properties.
        /// </summary>
        [Required]
        public List<ViewModels.PropertyModel> Properties { get; } = new List<ViewModels.PropertyModel>();
    }
}
