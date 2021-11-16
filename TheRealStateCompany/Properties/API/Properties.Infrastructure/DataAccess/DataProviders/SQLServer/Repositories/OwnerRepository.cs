using Microsoft.EntityFrameworkCore;
using Properties.Domain;
using Properties.Domain.Repositories;
using Properties.Domain.ValueObjects;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Properties.Infrastructure.DataAccess.DataProviders.SQLServer.Repositories
{
    public sealed class OwnerRepository : IOwnerRepository
    {
        private readonly RealStateDbContext _context;

        /// <summary>
        /// </summary>
        /// <param name="context"></param>
        public OwnerRepository(RealStateDbContext context) => this._context = context ??
                                                                          throw new ArgumentNullException(
                                                                              nameof(context));

        public async Task<IOwner> GetOwner(Identification identification) => await this._context
                .Owners
                .Where(e => e.IdentificationNumber == identification).Select(e => e)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);

        public async Task Create(Owner owner) => await this._context
                .Owners
                .AddAsync(owner)
                .ConfigureAwait(false);

        public async Task Update(Owner owner)
        {
            _context
                .Owners
                .Update(owner);

            await _context
                .SaveChangesAsync()
                .ConfigureAwait(false);
        }
    }
}
