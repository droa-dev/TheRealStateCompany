using Properties.Domain.ValueObjects;

namespace Properties.Domain
{
    public interface IOwner
    {
        /// <summary>
        ///     Gets guid.
        /// </summary>
        OwnerGuid OwnerGuid { get; }
    }
}
