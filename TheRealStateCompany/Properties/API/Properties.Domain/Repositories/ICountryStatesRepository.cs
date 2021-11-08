using Properties.Domain.ValueObjects;
using System.Threading.Tasks;

namespace Properties.Domain.Repositories
{
    public interface ICountryStatesRepository
    {
        Task<CountryStates> GetCountryState(Abbreviation abbreviation);
        Task<CountryStates> GetCountryState(CountryStatesId countryStatesId);
        Task<CountryStates> GetCountryState(Name name);
    }
}
