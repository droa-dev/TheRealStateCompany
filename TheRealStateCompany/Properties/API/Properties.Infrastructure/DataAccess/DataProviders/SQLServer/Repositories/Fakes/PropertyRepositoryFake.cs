using Properties.Domain;
using Properties.Domain.Repositories;
using Properties.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Properties.Infrastructure.DataAccess.DataProviders.SQLServer.Repositories.Fakes
{
    public sealed class PropertyRepositoryFake : IPropertyRepository
    {
        private readonly RealStateDbContextFake _context;

        /// <summary>
        /// </summary>
        /// <param name="context"></param>
        public PropertyRepositoryFake(RealStateDbContextFake context) => this._context = context;

        public async Task<IProperty> GetProperty(PropertyGuid propertyGuid)
        {
            Property? property = this._context
                .Properties
                .Where(e => e.PropertyGuid == propertyGuid)
                .Select(e => e)
                .SingleOrDefault();

            if (property == null)
            {
                return PropertyNull.Instance;
            }

            return await Task.FromResult(property)
               .ConfigureAwait(false);
        }

        public async Task<IProperty> GetPropertyForUpdate(PropertyGuid propertyGuid)
        {
            Property? property = this._context
                .Properties
                .Where(e => e.PropertyGuid == propertyGuid)
                .Select(e => e)
                .SingleOrDefault();

            if (property == null)
            {
                return PropertyNull.Instance;
            }

            return await Task.FromResult(property)
               .ConfigureAwait(false);
        }

        public async Task Update(Property property)
        {
            Property? propertyOld = this._context
                .Properties
                .SingleOrDefault(e => e.PropertyGuid.Equals(property.PropertyGuid));

            if (propertyOld != null)
            {
                this._context.Properties.Remove(propertyOld);
            }

            this._context
                .Properties
                .Add(property);

            await Task.CompletedTask
                .ConfigureAwait(false);
        }

        public async Task Create(Property property)
        {
            this._context
                .Properties
                .Add(property);

            await Task.CompletedTask
                .ConfigureAwait(false);
        }

        public async Task<IList<Property>> GetPropertiesFilter(PropertyFilters filters)
        {
            IList<Property> result = new List<Property>();
            Money compareInitialPrice = (Money)(filters.InitialPrice.HasValue && filters.InitialPrice.Value.Amount > 0 ? filters!.InitialPrice! : new Money(0));
            Money compareMaxlPrice = (Money)(filters.MaxPrice.HasValue && filters.MaxPrice.Value.Amount > 0 ? filters!.MaxPrice! : new Money(0));

            result = this._context
                .Properties
                .Where(e =>
                 e.OwnerGuid == ((filters.OwnerGuid.HasValue && filters.OwnerGuid.Value.Id != Guid.Empty) ? filters.OwnerGuid : e.OwnerGuid)
                 && e.CountryStatesId == (filters.CountryStatesId.HasValue && !filters.CountryStatesId.Value.IsZero() ? filters.CountryStatesId : e.CountryStatesId)
                 && e.Price >= (!compareInitialPrice.IsZero() ? compareInitialPrice : e.Price)
                 && e.Price <= (!compareMaxlPrice.IsZero() ? compareMaxlPrice : e.Price)
                 && e.Year == (!string.IsNullOrEmpty(filters.Year) ? filters.Year : e.Year)
                 && e.CodeInternal == (!string.IsNullOrEmpty(filters.CodeInternal) ? filters.CodeInternal : e.CodeInternal))
                .ToList();

            return await Task.FromResult(result)
              .ConfigureAwait(false);
        }
    }
}
