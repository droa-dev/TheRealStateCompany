using Properties.Domain.ValueObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Properties.Domain.Repositories
{
    public interface IPropertyTraceRepository
    {
        Task Create(PropertyTrace propertyTrace);
        Task Update(PropertyTrace propertyTrace);
        Task<PropertyTrace> GetPropertyTrace(PropertyTraceId propertyTraceId);
        Task<List<PropertyTrace>> GetPropertyTraces(PropertyId propertyId);
    }
}
