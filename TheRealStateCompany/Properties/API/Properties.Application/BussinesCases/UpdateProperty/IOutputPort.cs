using Properties.Domain;

namespace Properties.Application.BussinesCases.UpdateProperty
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
        ///     Updated Property
        /// </summary>
        void Ok(Property property);
    }
}
