using Properties.Domain.Enums;
using Properties.Domain.ValueObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Properties.Domain.Repositories
{
    public interface IPropertyRepository
    {
        Task<Property> GetProperty(PropertyId propertyId);
        Task<Property> GetProperty(PropertyGuid propertyGuid);
        Task Update(Property property);
        Task Create(Property property);
        Task<IList<Property>> GetPropertiesFilter(PropertyFilter filter);
    }
}
