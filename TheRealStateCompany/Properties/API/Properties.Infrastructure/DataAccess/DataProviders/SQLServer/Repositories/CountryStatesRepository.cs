using Microsoft.EntityFrameworkCore;
using Properties.Domain;
using Properties.Domain.Repositories;
using Properties.Domain.ValueObjects;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Properties.Infrastructure.DataAccess.DataProviders.SQLServer.Repositories
{
    public sealed class CountryStatesRepository : ICountryStatesRepository
    {
        private readonly RealStateDbContext _context;

        /// <summary>
        /// </summary>
        /// <param name="context"></param>
        public CountryStatesRepository(RealStateDbContext context) => this._context = context ??
                                                                          throw new ArgumentNullException(
                                                                              nameof(context));

        public async Task<CountryStates> GetCountryState(Abbreviation abbreviation) => await this._context
                .CountryStates
                .Where(e => e.Abbrev == abbreviation).Select(e => e)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);

        public async Task<CountryStates> GetCountryState(CountryStatesId countryStatesId) => await this._context
                .CountryStates
                .Where(e => e.CountryStatesId == countryStatesId).Select(e => e)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);

        public async Task<CountryStates> GetCountryState(Name name) => await this._context
                .CountryStates
                .Where(e => e.Name == name).Select(e => e)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
    }
}
