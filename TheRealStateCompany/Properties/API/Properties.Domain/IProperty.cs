using Properties.Domain.ValueObjects;

namespace Properties.Domain
{
    public interface IProperty
    {
        /// <summary>
        ///     Gets guid.
        /// </summary>
        PropertyGuid PropertyGuid { get; }
    }
}
