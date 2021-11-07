using System.Threading.Tasks;

namespace Properties.Domain.Repositories
{
    public interface IPropertyImageRepository
    {
        Task Add(PropertyImage propertyImage);
        Task Update(PropertyImage propertyImage);
    }
}
