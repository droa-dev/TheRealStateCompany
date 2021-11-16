using Properties.Domain;
using Properties.Domain.Repositories;
using Properties.Domain.ValueObjects;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Properties.Infrastructure.DataAccess.DataProviders.SQLServer.Repositories.Fakes
{
    public sealed class PropertyTraceRepositoryFake : IPropertyTraceRepository
    {
        private readonly RealStateDbContextFake _context;

        /// <summary>
        /// </summary>
        /// <param name="context"></param>
        public PropertyTraceRepositoryFake(RealStateDbContextFake context) => this._context = context;

        public async Task Create(PropertyTrace propertyTrace)
        {
            this._context
                .PropertyTraces
                .Add(propertyTrace);

            await Task.CompletedTask
                .ConfigureAwait(false);
        }

        public async Task Update(PropertyTrace propertyTrace)
        {
            PropertyTrace? propertyTraceOld = this._context
                .PropertyTraces
                .SingleOrDefault(e => e.PropertyTraceGuid.Equals(propertyTrace.PropertyTraceGuid));

            if (propertyTraceOld != null)
            {
                this._context.PropertyTraces.Remove(propertyTraceOld);
            }

            this._context
                .PropertyTraces
                .Add(propertyTrace);

            await Task.CompletedTask
                .ConfigureAwait(false);
        }

        public async Task<IList<PropertyTrace>> GetPropertyTraces(PropertyGuid propertyGuid)
        {
            IList<PropertyTrace> resultList = this._context
                .PropertyTraces
                .Where(e => e.PropertyGuid == propertyGuid).Select(e => e)
                .ToList();

            return await Task.FromResult(resultList)
               .ConfigureAwait(false);
        }
    }
}
