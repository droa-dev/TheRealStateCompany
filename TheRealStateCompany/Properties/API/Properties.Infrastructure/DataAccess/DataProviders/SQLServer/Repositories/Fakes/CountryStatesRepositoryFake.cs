using Properties.Domain;
using Properties.Domain.Repositories;
using Properties.Domain.ValueObjects;
using System.Linq;
using System.Threading.Tasks;

namespace Properties.Infrastructure.DataAccess.DataProviders.SQLServer.Repositories.Fakes
{
    public sealed class CountryStatesRepositoryFake : ICountryStatesRepository
    {
        private readonly RealStateDbContextFake _context;

        /// <summary>
        /// </summary>
        /// <param name="context"></param>
        public CountryStatesRepositoryFake(RealStateDbContextFake context) => this._context = context;

        public async Task<ICountryStates> GetCountryState(Abbreviation abbreviation)
        {
            CountryStates? state = this._context
                .CountryStates
                .Where(e => e.Abbrev == abbreviation)
                .Select(e => e)
                .SingleOrDefault();

            if (state == null)
            {
                return CountryStatesNull.Instance;
            }

            return await Task.FromResult(state)
               .ConfigureAwait(false);
        }

        public async Task<ICountryStates> GetCountryState(CountryStatesId countryStatesId)
        {
            CountryStates? state = this._context
                .CountryStates
                .Where(e => e.CountryStatesId == countryStatesId)
                .Select(e => e)
                .SingleOrDefault();

            if (state == null)
            {
                return CountryStatesNull.Instance;
            }

            return await Task.FromResult(state)
               .ConfigureAwait(false);
        }

        public async Task<ICountryStates> GetCountryState(Name name)
        {
            CountryStates? state = this._context
                .CountryStates
                .Where(e => e.Name == name)
                .Select(e => e)
                .SingleOrDefault();

            if (state == null)
            {
                return CountryStatesNull.Instance;
            }

            return await Task.FromResult(state)
               .ConfigureAwait(false);
        }
    }
}
