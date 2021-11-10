using Microsoft.EntityFrameworkCore;
using Properties.Domain;
using Properties.Domain.Repositories;
using Properties.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Properties.Infrastructure.DataAccess.DataProviders.SQLServer.Repositories
{
    public sealed class PropertyTraceRepository : IPropertyTraceRepository
    {
        private readonly RealStateDbContext _context;

        /// <summary>
        /// </summary>
        /// <param name="context"></param>
        public PropertyTraceRepository(RealStateDbContext context) => this._context = context ??
                                                                          throw new ArgumentNullException(
                                                                              nameof(context));

        public async Task Create(PropertyTrace propertyTrace) => await this._context
                .PropertyTraces
                .AddAsync(propertyTrace)
                .ConfigureAwait(false);

        public async Task Update(PropertyTrace propertyTrace)
        {
            _context
               .PropertyTraces
               .Update(propertyTrace);

            await _context
                .SaveChangesAsync()
                .ConfigureAwait(false);
        }

        public async Task<PropertyTrace> GetPropertyTrace(PropertyTraceId propertyTraceId) => await this._context
                .PropertyTraces
                .Where(e => e.PropertyTraceId == propertyTraceId).Select(e => e)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);

        public async Task<IList<PropertyTrace>> GetPropertyTraces(PropertyGuid propertyGuid) => await this._context
                .PropertyTraces
                .Where(e => e.PropertyGuid == propertyGuid)
                .ToListAsync()
                .ConfigureAwait(false);
    }
}
