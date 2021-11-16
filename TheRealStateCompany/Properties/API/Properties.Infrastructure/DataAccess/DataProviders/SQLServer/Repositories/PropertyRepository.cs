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

        public async Task<IProperty> GetProperty(PropertyGuid propertyGuid)
        {
            Property property = await this._context
                .Properties
                .Where(e => e.PropertyGuid == propertyGuid).Select(e => e)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);

            return property != null ? property : PropertyNull.Instance;
        }

        public async Task<IProperty> GetPropertyForUpdate(PropertyGuid propertyGuid)
        {
            Property property = await this._context
                .Properties
                .AsNoTracking()
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

        public async Task<IList<Property>> GetPropertiesFilter(PropertyFilters filters)
        {
            IList<Property> result = new List<Property>();

            Money compareInitialPrice = (Money)(filters.InitialPrice.HasValue && filters.InitialPrice.Value.Amount > 0 ? filters!.InitialPrice! : new Money(0));
            Money compareMaxlPrice = (Money)(filters.MaxPrice.HasValue && filters.MaxPrice.Value.Amount > 0 ? filters!.MaxPrice! : new Money(0));

            result = await this._context
                .Properties
                .Where(e =>
                 e.OwnerGuid == ((filters.OwnerGuid.HasValue && filters.OwnerGuid.Value.Id != Guid.Empty) ? filters.OwnerGuid : e.OwnerGuid)
                 && e.CountryStatesId == (filters.CountryStatesId.HasValue && !filters.CountryStatesId.Value.IsZero() ? filters.CountryStatesId : e.CountryStatesId)
                 && e.Price >= (!compareInitialPrice.IsZero() ? compareInitialPrice : e.Price)
                 && e.Price <= (!compareMaxlPrice.IsZero() ? compareMaxlPrice : e.Price)
                 && e.Year == (!string.IsNullOrEmpty(filters.Year) ? filters.Year : e.Year)
                 && e.CodeInternal == (!string.IsNullOrEmpty(filters.CodeInternal) ? filters.CodeInternal : e.CodeInternal))
                .ToListAsync()
                .ConfigureAwait(false);

            return result;
        }


    }
}
