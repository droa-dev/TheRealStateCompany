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
    /// <inheritdoc />
    public sealed class PropertyRepository : IPropertyRepository
    {
        private readonly RealStateDbContext _context;

        /// <summary>
        /// </summary>
        /// <param name="context"></param>
        public PropertyRepository(RealStateDbContext context) => this._context = context ??
                                                                          throw new ArgumentNullException(
                                                                              nameof(context));

        public async Task<IProperty> GetProperty(PropertyId propertyId)
        {
            Property property = await this._context
                .Properties
                .Where(e => e.PropertyId == propertyId).Select(e => e)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);

            return property != null ? property : PropertyNull.Instance;
        }

        public async Task<IProperty> GetProperty(PropertyGuid propertyGuid)
        {
            Property property = await this._context
                .Properties
                .Where(e => e.PropertyGuid == propertyGuid).Select(e => e)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);

            return property != null ? property : PropertyNull.Instance;
        }

        public async Task Update(Property property)
        {
            _context
                .Properties
                .Update(property);

            await _context
                .SaveChangesAsync()
                .ConfigureAwait(false);
        }

        public async Task Create(Property property) => await this._context
                .Properties
                .AddAsync(property);

        public async Task<IList<Property>> GetPropertiesFilter(PropertyFilters filters) => await this._context
                .Properties
                .Where(e =>
                 e.Owner!.IdentificationNumber == (filters.OwnerIdenttification ?? e.Owner.IdentificationNumber)
                 && e.CountryStates!.Abbrev == (filters.StateAbbreviation ?? e.CountryStates.Abbrev)
                 && e.Price.Amount >= (filters.InitialPrice.HasValue ? filters.InitialPrice.Value.Amount : e.Price.Amount)
                 && e.Price.Amount <= (filters.MaxPrice.HasValue ? filters.MaxPrice.Value.Amount : e.Price.Amount)
                 && e.Year == (!string.IsNullOrEmpty(filters.Year) ? filters.Year : e.Year)
                 && e.CodeInternal == (!string.IsNullOrEmpty(filters.CodeInternal) ? filters.CodeInternal : e.CodeInternal))
                .ToListAsync()
                .ConfigureAwait(false);
    }
}
