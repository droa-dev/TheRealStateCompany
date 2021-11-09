using Properties.Domain;
using System.Collections.Generic;

namespace Properties.Application.BussinesCases.ListProperty
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
        ///     List Property by filters
        /// </summary>
        void Ok(IList<Property> properties);
    }
}
