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
        ///     Created Property
        /// </summary>
        void Ok(Property property);        
    }
}
