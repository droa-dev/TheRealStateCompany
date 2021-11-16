using Properties.Domain;
using Properties.Domain.Repositories;
using Properties.Domain.ValueObjects;
using System.Linq;
using System.Threading.Tasks;

namespace Properties.Infrastructure.DataAccess.DataProviders.SQLServer.Repositories.Fakes
{
    public sealed class OwnerRepositoryFake : IOwnerRepository
    {
        private readonly RealStateDbContextFake _context;

        /// <summary>
        /// </summary>
        /// <param name="context"></param>
        public OwnerRepositoryFake(RealStateDbContextFake context) => this._context = context;

        public async Task<IOwner> GetOwner(Identification identification)
        {
            Owner? owner = this._context
                .Owners
                .Where(e => e.IdentificationNumber == identification)
                .Select(e => e)
                .SingleOrDefault();

            if (owner == null)
            {
                return OwnerNull.Instance;
            }

            return await Task.FromResult(owner)
               .ConfigureAwait(false);
        }

        public async Task Create(Owner owner)
        {
            this._context
                .Owners
                .Add(owner);

            await Task.CompletedTask
                .ConfigureAwait(false);
        }

        public async Task Update(Owner owner)
        {
            Owner? ownerOld = this._context
                .Owners
                .SingleOrDefault(e => e.OwnerGuid.Equals(owner.OwnerGuid));

            if (ownerOld != null)
            {
                this._context.Owners.Remove(ownerOld);
            }

            this._context
                .Owners
                .Add(owner);

            await Task.CompletedTask
                .ConfigureAwait(false);
        }
    }
}
