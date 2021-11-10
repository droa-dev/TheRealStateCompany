using Properties.Domain;
using Properties.Domain.Repositories;
using System;
using System.Threading.Tasks;

namespace Properties.Infrastructure.DataAccess.DataProviders.SQLServer.Repositories
{
    public sealed class PropertyImageRepository : IPropertyImageRepository
    {
        private readonly RealStateDbContext _context;

        /// <summary>
        /// </summary>
        /// <param name="context"></param>
        public PropertyImageRepository(RealStateDbContext context) => this._context = context ??
                                                                          throw new ArgumentNullException(
                                                                              nameof(context));

        public async Task Add(PropertyImage propertyImage) => await this._context
                .PropertyImages
                .AddAsync(propertyImage)
                .ConfigureAwait(false);

        public async Task Update(PropertyImage propertyImage)
        {
            _context
               .PropertyImages
               .Update(propertyImage);

            await _context
                .SaveChangesAsync()
                .ConfigureAwait(false);
        }
    }
}
