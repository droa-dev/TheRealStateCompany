using Properties.Domain.ValueObjects;
using System.Threading.Tasks;

namespace Properties.Domain.Repositories
{
    public interface IOwnerRepository
    {
        //Task<Owner> GetOwner(OwnerId ownerId);
        Task<IOwner> GetOwner(Identification identification);
        Task Create(Owner owner);
        Task Update(Owner owner);
    }
}
