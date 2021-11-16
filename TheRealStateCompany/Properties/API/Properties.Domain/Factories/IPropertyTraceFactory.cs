using Properties.Domain.ValueObjects;

namespace Properties.Domain.Factories
{
    public interface IPropertyTraceFactory
    {
        /// <summary>
        ///     Creates a new PropertyTrace instance.
        /// </summary>        
        /// <param name="name">name</param>
        /// <param name="value">value</param>
        /// <param name="tax">tax</param>
        /// <param name="propertyGuid">property guid relation</param>       
        /// <returns>New PropertyTrace instance.</returns>
        PropertyTrace NewPropertyTrace(Name name, Money value, Money tax, PropertyGuid propertyGuid);
    }
}
