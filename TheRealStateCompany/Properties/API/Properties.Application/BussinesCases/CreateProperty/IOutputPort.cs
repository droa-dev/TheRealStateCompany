using Properties.Domain;

namespace Properties.Application.BussinesCases.CreateProperty
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
        ///     Property already exist
        /// </summary>
        void Ok(Property property);

        /// <summary>
        ///     Created Property
        /// </summary>
        void Created(Property property);
    }
}
