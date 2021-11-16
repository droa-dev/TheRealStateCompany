using Properties.Domain.ValueObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Properties.Domain.Repositories
{
    public interface IPropertyRepository
    {
        Task<IProperty> GetProperty(PropertyGuid propertyGuid);
        Task<IProperty> GetPropertyForUpdate(PropertyGuid propertyGuid);
        Task Update(Property property);
        Task Create(Property property);
        Task<IList<Property>> GetPropertiesFilter(PropertyFilters filters);
    }
}
