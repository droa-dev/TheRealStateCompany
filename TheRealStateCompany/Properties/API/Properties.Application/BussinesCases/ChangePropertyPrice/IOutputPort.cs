using Properties.Domain;

namespace Properties.Application.BussinesCases.ChangePropertyPrice
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
        ///     Updated Property price
        /// </summary>
        void Ok(Property property);
    }
}
