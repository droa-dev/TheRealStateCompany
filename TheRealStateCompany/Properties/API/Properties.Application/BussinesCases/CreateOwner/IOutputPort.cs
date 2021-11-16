using Properties.Domain;

namespace Properties.Application.BussinesCases.CreateOwner
{
    public interface IOutputPort
    {
        /// <summary>
        ///     Invalid input.
        /// </summary>
        void Invalid();

        /// <summary>
        ///     Created Owner
        /// </summary>
        void Ok(Owner owner);
    }
}
