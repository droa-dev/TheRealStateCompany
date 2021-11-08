using Properties.Domain;

namespace Properties.Application.BussinesCases.AddPropertyImage
{
    public interface IOutputPort
    {
        /// <summary>
        ///     Invalid input.
        /// </summary>
        void Invalid();

        /// <summary>
        ///     Not found.
        /// </summary>
        void NotFound();

        /// <summary>
        ///     Created Property
        /// </summary>
        void Ok(PropertyImage propertyImage);
    }
}
